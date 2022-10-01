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

namespace MiniSalesApp.Application.StoreRecivement.Queries.SearchStoreRecivement
{
    public class SearchStoreRecivementQuery : IRequest<List<Dtos.StoreRecivementDto>>
    {
        public int? Serial { get; set; }
        public int? CustomerId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public bool IsCustomerRecivement { get; set; }
    }

    public class SearchStoreRecivementQueryHandler : IRequestHandler<SearchStoreRecivementQuery, List<Dtos.StoreRecivementDto>>
    {
        private readonly IMiniSalesAppContext _context;
        public SearchStoreRecivementQueryHandler(IMiniSalesAppContext miniSalesAppContext)
        {
            _context = miniSalesAppContext;
        }
        public async Task<List<Dtos.StoreRecivementDto>> Handle(SearchStoreRecivementQuery request, CancellationToken cancellationToken)
        {
            List<Dtos.StoreRecivementDto> result = new List<Dtos.StoreRecivementDto>();

            IQueryable<Logic.StoreDailyAgreget.StoreDaily> storeDailies = _context.StoreDailies.AsQueryable();

            if (request.Serial != null && request.Serial > default(int))
                storeDailies = storeDailies.Where(x => x.StoreRecivementList.Any(z => z.Serial == request.Serial));
            else
            {
                storeDailies = storeDailies.Where(x => x.StoreRecivementList.Any(z => z.IsCustomerRecivement == request.IsCustomerRecivement));

                if (request.IsCustomerRecivement && request.CustomerId != null && request.CustomerId > default(int))
                    storeDailies = storeDailies.Where(x => x.StoreRecivementList.Any(z => z.CustomerId == request.CustomerId));

                if (request.FromDate != DateTime.MinValue && request.ToDate != DateTime.MinValue)
                    storeDailies = storeDailies.Where(z => z.StoreRecivementList.Any( x => x.Date >= request.FromDate && x.Date <= request.ToDate));
            }

            var resultList = await storeDailies.SelectMany(x => x.StoreRecivementList).ToListAsync();

            result = resultList.Select(x => new StoreRecivementDto
            {
                StoreDailyId = x.StoreDailyId,
                StoreRecivementId = x.StoreRecivementId,
                IsCustomerRecivement = x.IsCustomerRecivement,
                CustomerId = x.CustomerId,
                Serial = x.Serial,
                Amount = x.Amount,
                Date = x.Date,
                Description = x.Description
            }).ToList();

            var customersIds = result.Select(x => x.CustomerId ?? 0);

            var customersNames = await _context.Customers
                .Where(z => customersIds.Contains(z.CustomerId))
                .Select(x => new { Id = x.CustomerId, Name = x.Name })
                .ToListAsync();

            result.ForEach(x => x.CustomerName = customersNames.FirstOrDefault(y => y.Id == (x.CustomerId ?? 0)).Name);

            return result;
        }
    }
}
