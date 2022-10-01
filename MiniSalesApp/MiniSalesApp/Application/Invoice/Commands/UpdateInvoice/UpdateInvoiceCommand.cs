using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MiniSalesApp.Application.Invoice.Dtos;
using MiniSalesApp.Application.InterFaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MiniSalesApp.Application.Invoice.Commands.UpdateInvoice
{
    public class UpdateInvoiceCommand : IRequest<Result<int>>
    {
        public InvoiceDto invoice { get; set; }
    }

    public class UpdateInvoiceCommandHandler : IRequestHandler<UpdateInvoiceCommand, Result<int>>
    {
        private readonly IMiniSalesAppContext _context;

        public UpdateInvoiceCommandHandler(IMiniSalesAppContext miniSalesAppContext)
        {
            _context = miniSalesAppContext;
        }
        public async Task<Result<int>> Handle(UpdateInvoiceCommand request, CancellationToken cancellationToken)
        {
            Maybe<Logic.InvoiceAgreget.Invoice> maybeInvoice = 
                await _context.Invoices
                .Include(z => z.InvoiceDetailList)
                .FirstOrDefaultAsync(x => x.InvoiceId == request.invoice.InvoiceId);

            if (maybeInvoice.HasNoValue)
                return Result.Failure<int>(Messages.InvoiceNotFound);

            Logic.InvoiceAgreget.Invoice invoice = maybeInvoice.Value;

            var materialsIds = request.invoice.InvoiceDetailList.Select(x => x.MaterialId).ToList();

            List<Logic.MaterialAgreget.Material> materials = await _context.Materials
                .Where(z => materialsIds.Contains(z.MaterialId))
                .Select(x => x)
                .ToListAsync();

            var invoiceDetailList = request.invoice.InvoiceDetailList.Select(y => new Logic.InvoiceAgreget.Dtos.InvoiceDetailDto
            {
                Quantity = y.Quantity,
                SellPrice = y.SellPrice,
                maybeMaterial = materials.FirstOrDefault(x => x.MaterialId == y.MaterialId)
            }).ToList();

            Logic.InvoiceAgreget.Dtos.InvoiceDto invoiceDto = new Logic.InvoiceAgreget.Dtos.InvoiceDto
            {
                Date = request.invoice.Date,
                Discount = request.invoice.Discount,
                Discription = request.invoice.Discription,
                maybeCustomer = await _context.Customers.FirstOrDefaultAsync(x => x.CustomerId == request.invoice.CustomerId),
                invoiceDetailList = invoiceDetailList
            };

            var updateResult = invoice.UpdateInvoice(invoiceDto);

            if (updateResult.IsFailure)
                return Result.Failure<int>(updateResult.Error);

            var savingResult = await _context.SaveChangesWithValidation();

            if (savingResult.IsFailure)
                return Result.Failure<int>(savingResult.Error);

            return Result.Success(updateResult.Value.InvoiceId);
        }
    }
}
