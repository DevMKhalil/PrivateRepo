using MediatR;
using Microsoft.EntityFrameworkCore;
using MiniSalesApp.Application.StoreDaily.Dtos;
using MiniSalesApp.Application.InterFaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MiniSalesApp.Application.StoreDaily.Queries.SearchStoreDaily
{
    public class SearchStoreDailyQuery : IRequest<List<StoreDailyDto>>
    {
        public int? Serial { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }

    public class SearchStoreDailyQueryHandler : IRequestHandler<SearchStoreDailyQuery, List<StoreDailyDto>>
    {
        private readonly IMiniSalesAppContext _context;
        public SearchStoreDailyQueryHandler(IMiniSalesAppContext miniSalesAppContext)
        {
            _context = miniSalesAppContext;
        }
        public async Task<List<StoreDailyDto>> Handle(SearchStoreDailyQuery request, CancellationToken cancellationToken)
        {
            List<StoreDailyDto> result = new List<StoreDailyDto>();

            IQueryable<Logic.StoreDailyAgreget.StoreDaily> storeDailies = _context.StoreDailies.AsQueryable();

            if (request.Serial != null && request.Serial > default(int))
                storeDailies = storeDailies.Where(x => x.Serial == request.Serial);
            else
            {
                if (request.FromDate != DateTime.MinValue && request.ToDate != DateTime.MinValue)
                    storeDailies = storeDailies.Where(x => x.StartDate >= request.FromDate && x.StartDate <= request.ToDate);
            }

            result = await (from storeDaily in storeDailies
                            select new StoreDailyDto
                            {
                                StoreDailyId = storeDaily.StoreDailyId,
                                Serial = storeDaily.Serial,
                                StartDate = storeDaily.StartDate,
                                StartAmount = storeDaily.StartAmount,
                                EndAmount = storeDaily.EndAmount
                            }).ToListAsync();

            return result;
        }
    }
}
