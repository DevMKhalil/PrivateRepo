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

namespace MiniSalesApp.Application.StoreDaily.Queries.GetStoreDaily
{
    public class GetStoreDailyQuery : IRequest<List<StoreDailyDto>>
    {
    }

    public class GetStoreDailyQueryHandler : IRequestHandler<GetStoreDailyQuery, List<StoreDailyDto>>
    {
        private readonly IMiniSalesAppContext _context;
        public GetStoreDailyQueryHandler(IMiniSalesAppContext miniSalesAppContext)
        {
            _context = miniSalesAppContext;
        }
        public async Task<List<StoreDailyDto>> Handle(GetStoreDailyQuery request, CancellationToken cancellationToken)
        {
            List<StoreDailyDto> result = new List<StoreDailyDto>();

            result = await _context.StoreDailies.Select(x => new StoreDailyDto
            {
                StoreDailyId = x.StoreDailyId,
                Serial = x.Serial,
                StartDate = x.StartDate,
                StartAmount = x.StartAmount,
                EndAmount = x.EndAmount
            }).ToListAsync();

            return result;
        }
    }
}
