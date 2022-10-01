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

namespace MiniSalesApp.Application.Invoice.Commands.DeleteInvoice
{
    public class DeleteInvoiceCommand : IRequest<Result>
    {
        public int InvoiceId { get; set; }
    }

    public class DeleteInvoiceCommandHandler : IRequestHandler<DeleteInvoiceCommand, Result>
    {
        private readonly IMiniSalesAppContext _context;

        public DeleteInvoiceCommandHandler(IMiniSalesAppContext miniSalesAppContext)
        {
            _context = miniSalesAppContext;
        }
        public async Task<Result> Handle(DeleteInvoiceCommand request, CancellationToken cancellationToken)
        {
            Maybe<Logic.InvoiceAgreget.Invoice> maybeInvoice =
                await _context.Invoices
                .Include(z => z.InvoiceDetailList)
                .FirstOrDefaultAsync(x => x.InvoiceId == request.InvoiceId);

            if (maybeInvoice.HasNoValue)
                return Result.Failure<int>(Messages.InvoiceNotFound);

            Logic.InvoiceAgreget.Invoice invoice = maybeInvoice.Value;

            _context.Invoices.Remove(invoice);

            var savingResult = await _context.SaveChangesWithValidation();

            if (savingResult.IsFailure)
                return Result.Failure<int>(savingResult.Error);

            return Result.Success();
        }
    }
}
