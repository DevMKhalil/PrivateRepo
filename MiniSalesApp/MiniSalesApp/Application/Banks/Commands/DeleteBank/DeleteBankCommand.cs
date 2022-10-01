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

namespace MiniSalesApp.Application.Banks.Commands.DeleteBank
{
    public class DeleteBankCommand : IRequest<Result>
    {
        public int BankId { get; set; }
    }

    public class DeleteBankCommandHandler : IRequestHandler<DeleteBankCommand, Result>
    {
        private readonly IMiniSalesAppContext _context;

        public DeleteBankCommandHandler(IMiniSalesAppContext miniSalesAppContext)
        {
            _context = miniSalesAppContext;
        }
        public async Task<Result> Handle(DeleteBankCommand request, CancellationToken cancellationToken)
        {
            Maybe<Logic.BankAgreget.Bank> BankResult = await _context.Banks
                .Include(x => x.BankPaymentList)
                .Include(x => x.BankRecivementList)
                .FirstOrDefaultAsync(x => x.BankId == request.BankId);

            if (BankResult.HasNoValue)
                return Result.Failure(Messages.BankNotFound);

            Logic.BankAgreget.Bank Bank = BankResult.Value;

            int lastDailyId = await _context.Banks.MaxAsync(x => (int?)x.BankId) ?? 0;

            var validateForDeleteResult = Bank.VaildateForDelete();

            if (validateForDeleteResult.IsFailure)
                return Result.Failure(validateForDeleteResult.Error);

            _context.Banks.Remove(Bank);

            var savingResult = await _context.SaveChangesWithValidation();

            if (savingResult.IsFailure)
                return Result.Failure<int>(savingResult.Error);

            return Result.Success();
        }
    }
}
