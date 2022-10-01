using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MiniSalesApp.Application.BankPayment.Dtos;
using MiniSalesApp.Application.StoreRecivement.Dtos;
using MiniSalesApp.Application.InterFaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MiniSalesApp.Application.BankPayment.Commands.CreateBankPayment
{
    public class CreateBankPaymentCommand : IRequest<Result<int>>
    {
        public BankPaymentDto BankPayment { get; set; }
    }

    public class CreateStoreRecivementCommandHandler : IRequestHandler<CreateBankPaymentCommand, Result<int>>
    {
        private readonly IMiniSalesAppContext _context;

        public CreateStoreRecivementCommandHandler(IMiniSalesAppContext miniSalesAppContext)
        {
            _context = miniSalesAppContext;
        }

        public async Task<Result<int>> Handle(CreateBankPaymentCommand request, CancellationToken cancellationToken)
        {
            var maxSerial = await (from payment in _context.Banks
                                   select payment.BankPaymentList.Max(x => (int?)x.Serial) ?? 0).FirstAsync();

            Maybe<Logic.BankAgreget.Bank> bankResult = await _context.Banks
                .Include(x => x.BankRecivementList)
                .Include(x => x.BankPaymentList)
                .FirstOrDefaultAsync(x => x.BankId == request.BankPayment.BankId);

            if (bankResult.HasNoValue)
                return Result.Failure<int>(Messages.BankNotFound);

            Logic.BankAgreget.Bank bank = bankResult.Value;

            Maybe<Logic.SupplierAgreget.Supplier> maybeSupplier =
                await _context.Suppliers.FirstOrDefaultAsync(x => x.SupplierId == request.BankPayment.SupplierId);

            var dto = new Logic.BankPaymentAgreget.Dto.BankPaymentDto
            {
                Amount = request.BankPayment.Amount,
                Date = request.BankPayment.Date,
                Description = request.BankPayment.Description,
                IsSupplierPayment = request.BankPayment.IsSupplierPayment,
                MaybeBank = bank,
                MaybeSupplier = maybeSupplier
            };

            var createResult = Logic.BankPaymentAgreget.BankPayment.CreateBankPayment(dto, maxSerial);

            if (createResult.IsFailure)
                return Result.Failure<int>(createResult.Error);

            bank.BankPaymentList.Add(createResult.Value);

            var savingResult = await _context.SaveChangesWithValidation();

            if (savingResult.IsFailure)
                return Result.Failure<int>(savingResult.Error);

            return Result.Success(createResult.Value.BankPaymentId);
        }
    }
}
