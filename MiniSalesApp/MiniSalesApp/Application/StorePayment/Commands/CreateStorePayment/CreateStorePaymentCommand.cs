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

namespace MiniSalesApp.Application.StorePayment.Commands.CreateStorePayment
{
    public class CreateStorePaymentCommand : IRequest<Result<int>>
    {
        public Dtos.StorePaymentDto StorePayment { get; set; }
    }

    public class CreateStorePaymentCommandHandler : IRequestHandler<CreateStorePaymentCommand, Result<int>>
    {
        private readonly IMiniSalesAppContext _context;

        public CreateStorePaymentCommandHandler(IMiniSalesAppContext miniSalesAppContext)
        {
            _context = miniSalesAppContext;
        }

        public async Task<Result<int>> Handle(CreateStorePaymentCommand request, CancellationToken cancellationToken)
        {
            var maxSerial = await (from pay in _context.StoreDailies
                                   select pay.StorePaymentList.Max(x => (int?)x.Serial) ?? 0).FirstAsync();

            Maybe<Logic.StoreDailyAgreget.StoreDaily> lastDailyResult = await _context.StoreDailies
                .Include(x => x.StoreRecivementList)
                .Include(x => x.StorePaymentList).OrderByDescending(x => x.StoreDailyId)
                .FirstOrDefaultAsync();

            if (lastDailyResult.HasNoValue)
                return Result.Failure<int>(Messages.StoreDailyNotFound);

            Logic.StoreDailyAgreget.StoreDaily lastDaily = lastDailyResult.Value;

            Maybe<Logic.SupplierAgreget.Supplier> maybeSupplier =
                await _context.Suppliers.FirstOrDefaultAsync(x => x.SupplierId == request.StorePayment.SupplierId);

            var dto = new Logic.StorePaymentAgreget.Dto.StorePaymentDto
            {
                Amount = request.StorePayment.Amount,
                Date = request.StorePayment.Date,
                Description = request.StorePayment.Description,
                IsSupplierPayment = request.StorePayment.IsSupplierPayment,
                MaybeStoreDaily = lastDaily,
                MaybeSupplier = maybeSupplier
            };

            var createResult = Logic.StorePaymentAgreget.StorePayment.CreateStorePayment(dto, maxSerial);

            if (createResult.IsFailure)
                return Result.Failure<int>(createResult.Error);

            lastDaily.StorePaymentList.Add(createResult.Value);

            lastDaily.UpdateEndAmount();

            var savingResult = await _context.SaveChangesWithValidation();

            if (savingResult.IsFailure)
                return Result.Failure<int>(savingResult.Error);

            return Result.Success(createResult.Value.StorePaymentId);
        }
    }
}
