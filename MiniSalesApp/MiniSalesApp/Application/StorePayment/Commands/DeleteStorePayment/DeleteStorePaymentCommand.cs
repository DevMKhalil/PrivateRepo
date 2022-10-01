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

namespace MiniSalesApp.Application.StorePayment.Commands.DeleteStorePayment
{
    public class DeleteStorePaymentCommand : IRequest<Result>
    {
        public int StorePaymentId { get; set; }
    }

    public class DeleteStorePaymentCommandHandler : IRequestHandler<DeleteStorePaymentCommand, Result>
    {
        private readonly IMiniSalesAppContext _context;

        public DeleteStorePaymentCommandHandler(IMiniSalesAppContext miniSalesAppContext)
        {
            _context = miniSalesAppContext;
        }
        public async Task<Result> Handle(DeleteStorePaymentCommand request, CancellationToken cancellationToken)
        {
            Maybe<Logic.StoreDailyAgreget.StoreDaily> StoreDailyPaymentResult =
                await _context.StoreDailies
                .Include(x => x.StoreRecivementList)
                .Include(x => x.StorePaymentList)
                .FirstOrDefaultAsync(z => z.StorePaymentList.Any(t => t.StorePaymentId == request.StorePaymentId));


            if (StoreDailyPaymentResult.HasNoValue)
                return Result.Failure(Messages.StoreDailyNotFound);

            Logic.StoreDailyAgreget.StoreDaily storeDaily = StoreDailyPaymentResult.Value;

            int lastDailyId = await _context.StoreDailies.MaxAsync(x => (int?)x.StoreDailyId) ?? 0;

            var VaildateIsLastDailyResult = storeDaily.VaildateIsLastDaily(lastDailyId);

            if (VaildateIsLastDailyResult.IsFailure)
                return Result.Failure(VaildateIsLastDailyResult.Error);

            var payment = storeDaily.StorePaymentList.FirstOrDefault(x => x.StorePaymentId == request.StorePaymentId);

            storeDaily.StorePaymentList.Remove(payment);

            storeDaily.UpdateEndAmount();

            var savingResult = await _context.SaveChangesWithValidation();

            if (savingResult.IsFailure)
                return Result.Failure<int>(savingResult.Error);

            return Result.Success();
        }
    }
}
