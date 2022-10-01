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

namespace MiniSalesApp.Application.BankRecivement.Commands.DeleteBankRecivement
{
    public class DeleteBankRecivementCommand : IRequest<Result>
    {
        public int BankRecivementId { get; set; }
    }

    public class DeleteBankRecivementCommandHandler : IRequestHandler<DeleteBankRecivementCommand, Result>
    {
        private readonly IMiniSalesAppContext _context;

        public DeleteBankRecivementCommandHandler(IMiniSalesAppContext miniSalesAppContext)
        {
            _context = miniSalesAppContext;
        }
        public async Task<Result> Handle(DeleteBankRecivementCommand request, CancellationToken cancellationToken)
        {
            Maybe<Logic.BankAgreget.Bank> bankRecivementResult =
                await _context.Banks
                .Include(x => x.BankRecivementList)
                .Include(x => x.BankPaymentList)
                .FirstOrDefaultAsync(z => z.BankRecivementList.Any(t => t.BankRecivementId == request.BankRecivementId));

            if (bankRecivementResult.HasNoValue)
                return Result.Failure(Messages.BankNotFound);

            Logic.BankAgreget.Bank bankRecivement = bankRecivementResult.Value;

            var payment = bankRecivement.BankRecivementList.FirstOrDefault(x => x.BankRecivementId == request.BankRecivementId);

            bankRecivement.BankRecivementList.Remove(payment);

            var savingResult = await _context.SaveChangesWithValidation();

            if (savingResult.IsFailure)
                return Result.Failure<int>(savingResult.Error);

            return Result.Success();
        }
    }
}
