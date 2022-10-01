using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using MediatR;
using MiniSalesApp.Application.Customers.Commands.CreateCustomer;
using MiniSalesApp.Application.Invoice.Dtos;
using MiniSalesApp.Application.InterFaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MiniSalesApp.Application.Invoice.Commands.CreateInvoice
{
    public class CreateInvoiceCommand : IRequest<Result<int>>
    {
        public InvoiceDto invoice { get; set; }
    }

    public class CreateInvoiceCommandHandler : IRequestHandler<CreateInvoiceCommand, Result<int>>
    {
        private readonly IMiniSalesAppContext _context;

        public CreateInvoiceCommandHandler(IMiniSalesAppContext miniSalesAppContext)
        {
            _context = miniSalesAppContext;
        }
        public async Task<Result<int>> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
        {
            var maxSerial = await _context.Invoices.MaxAsync(x => (int?)x.Serial) ?? 0;

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

            Logic.InvoiceAgreget.Dtos.InvoiceDto invoice = new Logic.InvoiceAgreget.Dtos.InvoiceDto
            {
                Date = request.invoice.Date,
                Discount = request.invoice.Discount,
                Discription = request.invoice.Discription,
                maybeCustomer = await _context.Customers.FirstOrDefaultAsync(x => x.CustomerId == request.invoice.CustomerId),
                invoiceDetailList = invoiceDetailList
            };

            var createResult = MiniSalesApp.Logic.InvoiceAgreget.Invoice.CreateInvoice(invoice, maxSerial);

            if (createResult.IsFailure)
                return Result.Failure<int>(createResult.Error);

            _context.Invoices.Add(createResult.Value);

            var savingResult = await _context.SaveChangesWithValidation();

            if (savingResult.IsFailure)
                return Result.Failure<int>(savingResult.Error);

            return Result.Success(createResult.Value.InvoiceId);
        }
    }
}
