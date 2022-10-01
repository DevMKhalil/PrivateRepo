using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MiniSalesApp.Application.StoreDaily.Dtos;
using MiniSalesApp.Application.InterFaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MiniSalesApp.Application.StoreDaily.Commands.UpdateStoreDaily
{
    public class UpdateStoreDailyCommand : IRequest<Result<int>>
    {
        public StoreDailyDto StoreDaily { get; set; }
    }

    public class UpdateStoreDailyCommandHandler : IRequestHandler<UpdateStoreDailyCommand, Result<int>>
    {
        private readonly IMiniSalesAppContext _context;

        public UpdateStoreDailyCommandHandler(IMiniSalesAppContext miniSalesAppContext)
        {
            _context = miniSalesAppContext;
        }
        public async Task<Result<int>> Handle(UpdateStoreDailyCommand request, CancellationToken cancellationToken)
        {
            Maybe<Logic.StoreDailyAgreget.StoreDaily> StoreDailyResult = await _context.StoreDailies
               .Include(x => x.StoreRecivementList)
               .Include(x => x.StorePaymentList)
               .FirstOrDefaultAsync(x => x.StoreDailyId == request.StoreDaily.StoreDailyId);

            if (StoreDailyResult.HasNoValue)
                return Result.Failure<int>(Messages.StoreDailyNotFound);

            Logic.StoreDailyAgreget.StoreDaily storeDaily = StoreDailyResult.Value;

            int lastDailyId = await _context.StoreDailies.MaxAsync(x => (int?)x.StoreDailyId) ?? 0;

            var validateForUpdateResult = storeDaily.VaildateForUpdate(lastDailyId);

            if (validateForUpdateResult.IsFailure)
                return Result.Failure<int>(validateForUpdateResult.Error);

            Maybe<Logic.StoreDailyAgreget.StoreDaily> lastDaily = await _context.StoreDailies.OrderByDescending(x => x.StoreDailyId)
                .Include(x => x.StorePaymentList)
                .Include(x => x.StoreRecivementList)
                .FirstOrDefaultAsync();

            DateTime? lastTransactionInLastDailyDate;
            bool isTheFirst;
            decimal lastDailyEndAmount;

            if (lastDaily.HasNoValue || StoreDailyResult.Value.Serial == 1)
            {
                lastTransactionInLastDailyDate = null;
                isTheFirst = true;
                lastDailyEndAmount = default(decimal);
            }
            else
            {
                var maxPaymentDate = lastDaily.Value.StorePaymentList.Max(x => (DateTime?)x.Date) ?? DateTime.MinValue;
                var maxRecivementDate = lastDaily.Value.StoreRecivementList.Max(x => (DateTime?)x.Date) ?? DateTime.MinValue;

                if (maxPaymentDate == DateTime.MinValue && maxRecivementDate == DateTime.MinValue)
                    lastTransactionInLastDailyDate = lastDaily.Value.StartDate;
                else
                    lastTransactionInLastDailyDate = maxPaymentDate >= maxRecivementDate ? maxPaymentDate : maxRecivementDate;

                isTheFirst = false;
                lastDailyEndAmount = lastDaily.Value.EndAmount;
            }

            var updateResult = storeDaily.UpdateStoreDaily(
                request.StoreDaily,
                lastTransactionInLastDailyDate,
                isTheFirst,
                lastDailyEndAmount);

            if (updateResult.IsFailure)
                return Result.Failure<int>(updateResult.Error);

            var savingResult = await _context.SaveChangesWithValidation();

            if (savingResult.IsFailure)
                return Result.Failure<int>(savingResult.Error);

            return Result.Success(updateResult.Value.StoreDailyId);
        }
    }
}
