using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MiniSalesApp.Application.StorePayment.Dtos;
using MiniSalesApp.Application.InterFaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MiniSalesApp.Application.StorePayment.Commands.UpdateStorePayment
{
    public class UpdateStorePaymentCommand : IRequest<Result<int>>
    {
        public Dtos.StorePaymentDto StorePayment { get; set; }
    }

    public class UpdateStorePaymentCommandHandler : IRequestHandler<UpdateStorePaymentCommand, Result<int>>
    {
        private readonly IMiniSalesAppContext _context;

        public UpdateStorePaymentCommandHandler(IMiniSalesAppContext miniSalesAppContext)
        {
            _context = miniSalesAppContext;
        }
        public async Task<Result<int>> Handle(UpdateStorePaymentCommand request, CancellationToken cancellationToken)
        {
            Maybe<Logic.StoreDailyAgreget.StoreDaily> StoreDailyPaymentResult =
               await _context.StoreDailies
                .Include(x => x.StoreRecivementList)
               .Include(x => x.StorePaymentList)
               .FirstOrDefaultAsync(z => z.StorePaymentList.Any(t => t.StorePaymentId == request.StorePayment.StorePaymentId));

            if (StoreDailyPaymentResult.HasNoValue)
                return Result.Failure<int>(Messages.StoreDailyNotFound);

            Logic.StoreDailyAgreget.StoreDaily storeDaily = StoreDailyPaymentResult.Value;

            int lastDailyId = await _context.StoreDailies.MaxAsync(x => (int?)x.StoreDailyId) ?? 0;

            var VaildateIsLastDailyResult = storeDaily.VaildateIsLastDaily(lastDailyId);

            if (VaildateIsLastDailyResult.IsFailure)
                return Result.Failure<int>(VaildateIsLastDailyResult.Error);

            Maybe<Logic.SupplierAgreget.Supplier> maybeSupplier =
               await _context.Suppliers.FirstOrDefaultAsync(x => x.SupplierId == request.StorePayment.SupplierId);

            var dto = new Logic.StorePaymentAgreget.Dto.StorePaymentDto
            {
                Amount = request.StorePayment.Amount,
                Date = request.StorePayment.Date,
                Description = request.StorePayment.Description,
                IsSupplierPayment = request.StorePayment.IsSupplierPayment,
                MaybeStoreDaily = StoreDailyPaymentResult,
                MaybeSupplier = maybeSupplier,
                StorePaymentId = request.StorePayment.StorePaymentId
            };

            var rescivement = storeDaily.StorePaymentList.FirstOrDefault(x => x.StorePaymentId == request.StorePayment.StorePaymentId);

            var updateResult = rescivement.UpdateStorePayment(dto);

            if (updateResult.IsFailure)
                return Result.Failure<int>(updateResult.Error);

            storeDaily.UpdateEndAmount();

            var savingResult = await _context.SaveChangesWithValidation();

            if (savingResult.IsFailure)
                return Result.Failure<int>(savingResult.Error);

            return Result.Success(updateResult.Value.StorePaymentId);
        }
    }
}
