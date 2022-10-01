using MediatR;
using Microsoft.EntityFrameworkCore;
using MiniSalesApp.Application.StorePayment.Dtos;
using MiniSalesApp.Application.InterFaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MiniSalesApp.Application.StorePayment.Queries.GetStorePayment
{
    public class GetStorePaymentQuery : IRequest<List<StorePaymentDto>>
    {
    }

    public class GetStorePaymentQueryHandler : IRequestHandler<GetStorePaymentQuery, List<StorePaymentDto>>
    {
        private readonly IMiniSalesAppContext _context;
        public GetStorePaymentQueryHandler(IMiniSalesAppContext miniSalesAppContext)
        {
            _context = miniSalesAppContext;
        }
        public async Task<List<StorePaymentDto>> Handle(GetStorePaymentQuery request, CancellationToken cancellationToken)
        {
            List<StorePaymentDto> result = new List<StorePaymentDto>();

            var resultList = await _context.StoreDailies.SelectMany(x => x.StorePaymentList).ToListAsync();

            var supIds = resultList.Select(x => x.SupplierId ?? 0);

            var custList = await _context.Suppliers
                .Where(x => supIds.Contains(x.SupplierId))
                .Select(z => new { supId = z.SupplierId, supName = z.Name }).ToListAsync();

            result = resultList.Select(x => new StorePaymentDto
            {
                StoreDailyId = x.StoreDailyId,
                StorePaymentId = x.StorePaymentId,
                IsSupplierPayment = x.IsSupplierPayment,
                SupplierId = x.SupplierId,
                SupplierName = x.SupplierId == null ? string.Empty : custList.FirstOrDefault(t => t.supId == x.SupplierId).supName,
                Serial = x.Serial,
                Amount = x.Amount,
                Date = x.Date,
                Description = x.Description
            }).ToList();

            return result;
        }
    }
}
