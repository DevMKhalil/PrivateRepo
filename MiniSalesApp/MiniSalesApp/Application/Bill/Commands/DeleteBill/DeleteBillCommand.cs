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

namespace MiniSalesApp.Application.Bill.Commands.DeleteBill
{
    public class DeleteBillCommand : IRequest<Result>
    {
        public int BillId { get; set; }
    }

    public class DeleteBillCommandHandler : IRequestHandler<DeleteBillCommand, Result>
    {
        private readonly IMiniSalesAppContext _context;

        public DeleteBillCommandHandler(IMiniSalesAppContext miniSalesAppContext)
        {
            _context = miniSalesAppContext;
        }
        public async Task<Result> Handle(DeleteBillCommand request, CancellationToken cancellationToken)
        {
            Maybe<Logic.BillAgreget.Bill> maybeBill =
                await _context.Bills
                .Include(z => z.BillDetailList)
                .FirstOrDefaultAsync(x => x.BillId == request.BillId);

            if (maybeBill.HasNoValue)
                return Result.Failure<int>(Messages.InvoiceNotFound);

            Logic.BillAgreget.Bill bill = maybeBill.Value;

            _context.Bills.Remove(bill);

            var savingResult = await _context.SaveChangesWithValidation();

            if (savingResult.IsFailure)
                return Result.Failure<int>(savingResult.Error);

            return Result.Success();
        }
    }
}
