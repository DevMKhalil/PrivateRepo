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

namespace MiniSalesApp.Application.Invoice.Queries.SearchInvoice
{
    public class SearchInvoiceQuery : IRequest<List<InvoiceForListDto>>
    {
        public int? Serial { get; set; }
        public int? CustomerId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }

    public class SearchInvoiceQueryHandler : IRequestHandler<SearchInvoiceQuery, List<InvoiceForListDto>>
    {
        private readonly IMiniSalesAppContext _context;
        public SearchInvoiceQueryHandler(IMiniSalesAppContext miniSalesAppContext)
        {
            _context = miniSalesAppContext;
        }
        public async Task<List<InvoiceForListDto>> Handle(SearchInvoiceQuery request, CancellationToken cancellationToken)
        {
            List<InvoiceForListDto> result = new List<InvoiceForListDto>();

            IQueryable<Logic.InvoiceAgreget.Invoice> invoices = _context.Invoices.AsQueryable();

            if (request.Serial != null && request.Serial > default(int))
                invoices = invoices.Where(x => x.Serial == request.Serial);
            else
            {
                if (request.CustomerId != null && request.CustomerId > default(int))
                    invoices = invoices.Where(x => x.CustomerId == request.CustomerId);

                if (request.FromDate != DateTime.MinValue && request.ToDate != DateTime.MinValue)
                    invoices = invoices.Where(x => x.Date >= request.FromDate && x.Date <= request.ToDate); 
            }

            result = await (from invoice in invoices
                            select new InvoiceForListDto
                            {
                                InvoiceId = invoice.InvoiceId,
                                Serial = invoice.Serial,
                                Date = invoice.Date,
                                Discount = invoice.Discount,
                                Total = invoice.Total,
                                TotalAfterDiscount = invoice.TotalAfterDiscount,
                                Discription = invoice.Discription,
                                CustomerId = invoice.CustomerId
                            }).ToListAsync();

            var customersIds = result.Select(x => x.CustomerId);

            var customersNames = await _context.Customers
                .Where(z => customersIds.Contains(z.CustomerId))
                .Select(x => new { Id = x.CustomerId, Name = x.Name })
                .ToListAsync();

            result.ForEach(x => x.CustomerName = customersNames.FirstOrDefault(y => y.Id == x.CustomerId).Name);

            return result;
        }
    }
}
