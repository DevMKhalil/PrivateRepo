using DevExpress.Xpo;
using MediatR;
using MiniSalesApp.Application.StoreRecivement.Dtos;
using MiniSalesApp.Application.InterFaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MiniSalesApp.Application.StorePayment.Queries.SearchStorePayment
{
    public class SearchStorePaymentQuery : IRequest<List<Dtos.StorePaymentDto>>
    {
        public int? Serial { get; set; }
        public int? SupplierId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public bool IsSupplierPayment { get; set; }
    }

    public class SearchStorePaymentQueryHandler : IRequestHandler<SearchStorePaymentQuery, List<Dtos.StorePaymentDto>>
    {
        private readonly IMiniSalesAppContext _context;
        public SearchStorePaymentQueryHandler(IMiniSalesAppContext miniSalesAppContext)
        {
            _context = miniSalesAppContext;
        }
        public async Task<List<Dtos.StorePaymentDto>> Handle(SearchStorePaymentQuery request, CancellationToken cancellationToken)
        {
            List<Dtos.StorePaymentDto> result = new List<Dtos.StorePaymentDto>();

            IQueryable<Logic.StoreDailyAgreget.StoreDaily> storeDailies = _context.StoreDailies.AsQueryable();

            if (request.Serial != null && request.Serial > default(int))
                storeDailies = storeDailies.Where(x => x.StorePaymentList.Any(z => z.Serial == request.Serial));
            else
            {
                storeDailies = storeDailies.Where(x => x.StorePaymentList.Any(z => z.IsSupplierPayment == request.IsSupplierPayment));

                if (request.IsSupplierPayment && request.SupplierId != null && request.SupplierId > default(int))
                    storeDailies = storeDailies.Where(x => x.StorePaymentList.Any(z => z.SupplierId == request.SupplierId));

                if (request.FromDate != DateTime.MinValue && request.ToDate != DateTime.MinValue)
                    storeDailies = storeDailies.Where(z => z.StorePaymentList.Any( x => x.Date >= request.FromDate && x.Date <= request.ToDate));
            }

            var resultList = await storeDailies.SelectMany(x => x.StorePaymentList).ToListAsync();

            result = resultList.Select(x => new Dtos.StorePaymentDto
            {
                StoreDailyId = x.StoreDailyId,
                StorePaymentId = x.StorePaymentId,
                IsSupplierPayment = x.IsSupplierPayment,
                SupplierId = x.SupplierId,
                Serial = x.Serial,
                Amount = x.Amount,
                Date = x.Date,
                Description = x.Description
            }).ToList();

            var suppllierIds = result.Select(x => x.SupplierId ?? 0);

            var supplierNames = await _context.Suppliers
                .Where(z => suppllierIds.Contains(z.SupplierId))
                .Select(x => new { Id = x.SupplierId, Name = x.Name })
                .ToListAsync();

            result.ForEach(x => x.SupplierName = supplierNames.FirstOrDefault(y => y.Id == (x.SupplierId ?? 0)).Name);

            return result;
        }
    }
}
