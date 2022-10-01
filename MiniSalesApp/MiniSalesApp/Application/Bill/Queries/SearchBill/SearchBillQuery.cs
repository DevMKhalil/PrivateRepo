using MediatR;
using Microsoft.EntityFrameworkCore;
using MiniSalesApp.Application.Bill.Dtos;
using MiniSalesApp.Application.InterFaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MiniSalesApp.Application.Bill.Queries.SearchBill
{
    public class SearchBillQuery : IRequest<List<BillForListDto>>
    {
        public int? Serial { get; set; }
        public int? SupplierId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }

    public class SearchBillQueryHandler : IRequestHandler<SearchBillQuery, List<BillForListDto>>
    {
        private readonly IMiniSalesAppContext _context;
        public SearchBillQueryHandler(IMiniSalesAppContext miniSalesAppContext)
        {
            _context = miniSalesAppContext;
        }
        public async Task<List<BillForListDto>> Handle(SearchBillQuery request, CancellationToken cancellationToken)
        {
            List<BillForListDto> result = new List<BillForListDto>();

            IQueryable<Logic.BillAgreget.Bill> bills = _context.Bills.AsQueryable();

            if (request.Serial != null && request.Serial > default(int))
                bills = bills.Where(x => x.Serial == request.Serial);
            else
            {
                if (request.SupplierId != null && request.SupplierId > default(int))
                    bills = bills.Where(x => x.SupplierId == request.SupplierId);

                if (request.FromDate != DateTime.MinValue && request.ToDate != DateTime.MinValue)
                    bills = bills.Where(x => x.Date >= request.FromDate && x.Date <= request.ToDate); 
            }

            result = await (from bill in bills
                            select new BillForListDto
                            {
                                BillId = bill.BillId,
                                Serial = bill.Serial,
                                Date = bill.Date,
                                Discount = bill.Discount,
                                Total = bill.Total,
                                TotalAfterDiscount = bill.TotalAfterDiscount,
                                Discription = bill.Discription,
                                SupplierId = bill.SupplierId
                            }).ToListAsync();

            var supplierIds = result.Select(x => x.SupplierId);

            var customersNames = await _context.Suppliers
                .Where(z => supplierIds.Contains(z.SupplierId))
                .Select(x => new { Id = x.SupplierId, Name = x.Name })
                .ToListAsync();

            result.ForEach(x => x.SupplierName = customersNames.FirstOrDefault(y => y.Id == x.SupplierId).Name);

            return result;
        }
    }
}
