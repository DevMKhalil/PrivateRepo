using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MiniSalesApp.Application.BankRecivement.Dtos;
using MiniSalesApp.Application.StoreRecivement.Dtos;
using MiniSalesApp.Application.InterFaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MiniSalesApp.Application.BankRecivement.Commands.CreateBankRecivement
{
    public class CreateBankRecivementCommand : IRequest<Result<int>>
    {
        public BankRecivementDto BankRecivement { get; set; }
    }

    public class CreateBankRecivementCommandHandler : IRequestHandler<CreateBankRecivementCommand, Result<int>>
    {
        private readonly IMiniSalesAppContext _context;

        public CreateBankRecivementCommandHandler(IMiniSalesAppContext miniSalesAppContext)
        {
            _context = miniSalesAppContext;
        }

        public async Task<Result<int>> Handle(CreateBankRecivementCommand request, CancellationToken cancellationToken)
        {
            var maxSerial = await (from recivement in _context.Banks
                                   select recivement.BankRecivementList.Max(x => (int?)x.Serial) ?? 0).FirstAsync();

            Maybe<Logic.BankAgreget.Bank> bankResult = await _context.Banks
                .Include(x => x.BankRecivementList)
                .Include(x => x.BankPaymentList)
                .FirstOrDefaultAsync(x => x.BankId == request.BankRecivement.BankId);

            if (bankResult.HasNoValue)
                return Result.Failure<int>(Messages.BankNotFound);

            Logic.BankAgreget.Bank bank = bankResult.Value;

            Maybe<Logic.CustomerAgreget.Customer> maybeCustomer =
                await _context.Customers.FirstOrDefaultAsync(x => x.CustomerId == request.BankRecivement.CustomerId);

            var dto = new Logic.BankRecivementAgreget.Dto.BankRecivementDto
            {
                Amount = request.BankRecivement.Amount,
                Date = request.BankRecivement.Date,
                Description = request.BankRecivement.Description,
                IsCustomerRecivement = request.BankRecivement.IsCustomerRecivement,
                MaybeBank = bank,
                MaybeCustomer = maybeCustomer
            };

            var createResult = Logic.BankRecivementAgreget.BankRecivement.CreateBankRecivement(dto, maxSerial);

            if (createResult.IsFailure)
                return Result.Failure<int>(createResult.Error);

            bank.BankRecivementList.Add(createResult.Value);

            var savingResult = await _context.SaveChangesWithValidation();

            if (savingResult.IsFailure)
                return Result.Failure<int>(savingResult.Error);

            return Result.Success(createResult.Value.BankRecivementId);
        }
    }
}
