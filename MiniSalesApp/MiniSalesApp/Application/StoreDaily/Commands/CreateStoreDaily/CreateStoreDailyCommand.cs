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

namespace MiniSalesApp.Application.StoreDaily.Commands.CreateStoreDaily
{
    public class CreateStoreDailyCommand : IRequest<Result<int>>
    {
        public StoreDailyDto StoreDaily { get; set; }
    }

    public class CreateStoreDailyCommandHandler : IRequestHandler<CreateStoreDailyCommand, Result<int>>
    {
        private readonly IMiniSalesAppContext _context;

        public CreateStoreDailyCommandHandler(IMiniSalesAppContext miniSalesAppContext)
        {
            _context = miniSalesAppContext;
        }
        public async Task<Result<int>> Handle(CreateStoreDailyCommand request, CancellationToken cancellationToken)
        {
            var maxSerial = await _context.StoreDailies.MaxAsync(x => (int?)x.Serial) ?? 0;

            Maybe<Logic.StoreDailyAgreget.StoreDaily> lastDaily = await _context.StoreDailies.OrderByDescending(x => x.StoreDailyId)
                .Include(x => x.StorePaymentList)
                .Include(x => x.StoreRecivementList)
                .FirstOrDefaultAsync();

            DateTime? lastTransactionInLastDailyDate;
            bool isTheFirst;
            decimal lastDailyEndAmount;

            if (lastDaily.HasNoValue)
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

            var createResult = Logic.StoreDailyAgreget.StoreDaily.CreateStoreDaily(
                request.StoreDaily,
                lastTransactionInLastDailyDate,
                isTheFirst,
                lastDailyEndAmount,
                maxSerial);

            if (createResult.IsFailure)
                return Result.Failure<int>(createResult.Error);

            _context.StoreDailies.Add(createResult.Value);

            var savingResult = await _context.SaveChangesWithValidation();

            if (savingResult.IsFailure)
                return Result.Failure<int>(savingResult.Error);

            return Result.Success(createResult.Value.StoreDailyId);
        }
    }
}
