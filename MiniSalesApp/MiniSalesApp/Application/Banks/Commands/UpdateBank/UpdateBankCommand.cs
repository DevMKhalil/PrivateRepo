using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MiniSalesApp.Application.Banks.Dtos;
using MiniSalesApp.Application.InterFaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MiniSalesApp.Application.Banks.Commands.UpdateBank
{
    public class UpdateBankCommand : IRequest<Result<int>>
    {
        public BankDto Bank { get; set; }
    }

    public class UpdateBankCommandHandler : IRequestHandler<UpdateBankCommand, Result<int>>
    {
        private readonly IMiniSalesAppContext _context;

        public UpdateBankCommandHandler(IMiniSalesAppContext miniSalesAppContext)
        {
            _context = miniSalesAppContext;
        }
        public async Task<Result<int>> Handle(UpdateBankCommand request, CancellationToken cancellationToken)
        {
            Maybe<Logic.BankAgreget.Bank> BankResult = await _context.Banks
               .Include(x => x.BankPaymentList)
               .Include(x => x.BankRecivementList)
               .FirstOrDefaultAsync(x => x.BankId == request.Bank.BankId);

            if (BankResult.HasNoValue)
                return Result.Failure<int>(Messages.BankNotFound);

            Logic.BankAgreget.Bank Bank = BankResult.Value;

            var updateResult = Bank.UpdateBank(request.Bank);

            if (updateResult.IsFailure)
                return Result.Failure<int>(updateResult.Error);

            var savingResult = await _context.SaveChangesWithValidation();

            if (savingResult.IsFailure)
                return Result.Failure<int>(savingResult.Error);

            return Result.Success(updateResult.Value.BankId);
        }
    }
}
