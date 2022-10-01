using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MiniSalesApp.Application.BankPayment.Dtos;
using MiniSalesApp.Application.InterFaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MiniSalesApp.Application.BankPayment.Commands.UpdateBankPayment
{
    public class UpdateBankPaymentCommand : IRequest<Result<int>>
    {
        public BankPaymentDto bankPayment { get; set; }
    }

    public class UpdateBankPaymentCommandHandler : IRequestHandler<UpdateBankPaymentCommand, Result<int>>
    {
        private readonly IMiniSalesAppContext _context;

        public UpdateBankPaymentCommandHandler(IMiniSalesAppContext miniSalesAppContext)
        {
            _context = miniSalesAppContext;
        }
        public async Task<Result<int>> Handle(UpdateBankPaymentCommand request, CancellationToken cancellationToken)
        {
            Maybe<Logic.BankAgreget.Bank> BankPaymentResult =
               await _context.Banks
                .Include(x => x.BankRecivementList)
                .Include(x => x.BankPaymentList)
               .FirstOrDefaultAsync(z => z.BankPaymentList.Any(t => t.BankPaymentId == request.bankPayment.BankPaymentId));

            if (BankPaymentResult.HasNoValue)
                return Result.Failure<int>(Messages.BankNotFound);

            Logic.BankAgreget.Bank bank = BankPaymentResult.Value;

            Maybe<Logic.SupplierAgreget.Supplier> maybeSupplier =
               await _context.Suppliers.FirstOrDefaultAsync(x => x.SupplierId == request.bankPayment.SupplierId);

            var dto = new Logic.BankPaymentAgreget.Dto.BankPaymentDto
            {
                Amount = request.bankPayment.Amount,
                Date = request.bankPayment.Date,
                Description = request.bankPayment.Description,
                IsSupplierPayment = request.bankPayment.IsSupplierPayment,
                MaybeBank = BankPaymentResult,
                MaybeSupplier = maybeSupplier,
                BankPaymentId = request.bankPayment.BankPaymentId
            };

            var payment = bank.BankPaymentList.FirstOrDefault(x => x.BankPaymentId == request.bankPayment.BankPaymentId);

            var updateResult = payment.UpdateBankPayment(dto);

            if (updateResult.IsFailure)
                return Result.Failure<int>(updateResult.Error);

            var savingResult = await _context.SaveChangesWithValidation();

            if (savingResult.IsFailure)
                return Result.Failure<int>(savingResult.Error);

            return Result.Success(updateResult.Value.BankPaymentId);
        }
    }
}
