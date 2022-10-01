using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MiniSalesApp.Application.BankRecivement.Dtos;
using MiniSalesApp.Application.InterFaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MiniSalesApp.Application.BankRecivement.Commands.UpdateBankRecivement
{
    public class UpdateBankRecivementCommand : IRequest<Result<int>>
    {
        public BankRecivementDto BankRecivement { get; set; }
    }

    public class UpdateBankRecivementCommandHandler : IRequestHandler<UpdateBankRecivementCommand, Result<int>>
    {
        private readonly IMiniSalesAppContext _context;

        public UpdateBankRecivementCommandHandler(IMiniSalesAppContext miniSalesAppContext)
        {
            _context = miniSalesAppContext;
        }
        public async Task<Result<int>> Handle(UpdateBankRecivementCommand request, CancellationToken cancellationToken)
        {
            Maybe<Logic.BankAgreget.Bank> BankPaymentResult =
               await _context.Banks
                .Include(x => x.BankRecivementList)
                .Include(x => x.BankPaymentList)
               .FirstOrDefaultAsync(z => z.BankRecivementList.Any(t => t.BankRecivementId == request.BankRecivement.BankRecivementId));

            if (BankPaymentResult.HasNoValue)
                return Result.Failure<int>(Messages.BankNotFound);

            Logic.BankAgreget.Bank bank = BankPaymentResult.Value;

            Maybe<Logic.CustomerAgreget.Customer> maybeCustomer =
               await _context.Customers.FirstOrDefaultAsync(x => x.CustomerId == request.BankRecivement.CustomerId);

            var dto = new Logic.BankRecivementAgreget.Dto.BankRecivementDto
            {
                Amount = request.BankRecivement.Amount,
                Date = request.BankRecivement.Date,
                Description = request.BankRecivement.Description,
                IsCustomerRecivement = request.BankRecivement.IsCustomerRecivement,
                MaybeBank = BankPaymentResult,
                MaybeCustomer = maybeCustomer,
                BankRecivementId = request.BankRecivement.BankRecivementId
            };

            var recivement = bank.BankRecivementList.FirstOrDefault(x => x.BankRecivementId == request.BankRecivement.BankRecivementId);

            var updateResult = recivement.UpdateBankRecivement(dto);

            if (updateResult.IsFailure)
                return Result.Failure<int>(updateResult.Error);

            var savingResult = await _context.SaveChangesWithValidation();

            if (savingResult.IsFailure)
                return Result.Failure<int>(savingResult.Error);

            return Result.Success(updateResult.Value.BankRecivementId);
        }
    }
}
