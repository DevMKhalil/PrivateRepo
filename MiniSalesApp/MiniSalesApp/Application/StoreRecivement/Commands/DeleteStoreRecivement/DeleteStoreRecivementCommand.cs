using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MiniSalesApp.Application.InterFaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MiniSalesApp.Application.StoreRecivement.Commands.DeleteStoreRecivement
{
    public class DeleteStoreRecivementCommand : IRequest<Result>
    {
        public int StoreRecivementId { get; set; }
    }

    public class DeleteStoreRecivementCommandHandler : IRequestHandler<DeleteStoreRecivementCommand, Result>
    {
        private readonly IMiniSalesAppContext _context;

        public DeleteStoreRecivementCommandHandler(IMiniSalesAppContext miniSalesAppContext)
        {
            _context = miniSalesAppContext;
        }
        public async Task<Result> Handle(DeleteStoreRecivementCommand request, CancellationToken cancellationToken)
        {
            Maybe<Logic.StoreDailyAgreget.StoreDaily> StoreDailyRecivementResult =
                await _context.StoreDailies
                .Include(x => x.StorePaymentList)
                .Include(x => x.StoreRecivementList)
                .FirstOrDefaultAsync(z => z.StoreRecivementList.Any(t => t.StoreRecivementId == request.StoreRecivementId));


            if (StoreDailyRecivementResult.HasNoValue)
                return Result.Failure(Messages.StoreDailyNotFound);

            Logic.StoreDailyAgreget.StoreDaily storeDaily = StoreDailyRecivementResult.Value;

            int lastDailyId = await _context.StoreDailies.MaxAsync(x => (int?)x.StoreDailyId) ?? 0;

            var VaildateIsLastDailyResult = storeDaily.VaildateIsLastDaily(lastDailyId);

            if (VaildateIsLastDailyResult.IsFailure)
                return Result.Failure(VaildateIsLastDailyResult.Error);

            var rescivement = storeDaily.StoreRecivementList.FirstOrDefault(x => x.StoreRecivementId == request.StoreRecivementId);

            storeDaily.StoreRecivementList.Remove(rescivement);

            storeDaily.UpdateEndAmount();

            var savingResult = await _context.SaveChangesWithValidation();

            if (savingResult.IsFailure)
                return Result.Failure<int>(savingResult.Error);

            return Result.Success();
        }
    }
}
