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

namespace MiniSalesApp.Application.BankPayment.Commands.DeleteBankPayment
{
    public class DeleteBankPaymentCommand : IRequest<Result>
    {
        public int BankPaymentId { get; set; }
    }

    public class DeleteBankPaymentCommandHandler : IRequestHandler<DeleteBankPaymentCommand, Result>
    {
        private readonly IMiniSalesAppContext _context;

        public DeleteBankPaymentCommandHandler(IMiniSalesAppContext miniSalesAppContext)
        {
            _context = miniSalesAppContext;
        }
        public async Task<Result> Handle(DeleteBankPaymentCommand request, CancellationToken cancellationToken)
        {
            Maybe<Logic.BankAgreget.Bank> bankPaymentResult =
                await _context.Banks
                .Include(x => x.BankRecivementList)
                .Include(x => x.BankPaymentList)
                .FirstOrDefaultAsync(z => z.BankPaymentList.Any(t => t.BankPaymentId == request.BankPaymentId));

            if (bankPaymentResult.HasNoValue)
                return Result.Failure(Messages.StoreDailyNotFound);

            Logic.BankAgreget.Bank BankPayment = bankPaymentResult.Value;

            var payment = BankPayment.BankPaymentList.FirstOrDefault(x => x.BankPaymentId == request.BankPaymentId);

            BankPayment.BankPaymentList.Remove(payment);

            var savingResult = await _context.SaveChangesWithValidation();

            if (savingResult.IsFailure)
                return Result.Failure<int>(savingResult.Error);

            return Result.Success();
        }
    }
}
