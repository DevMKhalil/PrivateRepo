using MediatR;
using Microsoft.EntityFrameworkCore;
using MiniSalesApp.Application.StoreRecivement.Dtos;
using MiniSalesApp.Application.InterFaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MiniSalesApp.Application.StoreRecivement.Queries.GetStoreRecivement
{
    public class GetStoreRecivementQuery : IRequest<List<StoreRecivementDto>>
    {
    }

    public class GetStoreRecivementQueryHandler : IRequestHandler<GetStoreRecivementQuery, List<StoreRecivementDto>>
    {
        private readonly IMiniSalesAppContext _context;
        public GetStoreRecivementQueryHandler(IMiniSalesAppContext miniSalesAppContext)
        {
            _context = miniSalesAppContext;
        }
        public async Task<List<StoreRecivementDto>> Handle(GetStoreRecivementQuery request, CancellationToken cancellationToken)
        {
            List<StoreRecivementDto> result = new List<StoreRecivementDto>();

            var resultList = await _context.StoreDailies.SelectMany(x => x.StoreRecivementList).ToListAsync();

            var custIds = resultList.Select(x => x.CustomerId ?? 0);

            var custList = await _context.Customers
                .Where(x => custIds.Contains(x.CustomerId))
                .Select(z => new { custId = z.CustomerId, custName = z.Name }).ToListAsync();

            result = resultList.Select(x => new StoreRecivementDto
            {
                StoreDailyId = x.StoreDailyId,
                StoreRecivementId = x.StoreRecivementId,
                IsCustomerRecivement = x.IsCustomerRecivement,
                CustomerId = x.CustomerId,
                CustomerName = x.CustomerId == null ? string.Empty : custList.FirstOrDefault(t => t.custId == x.CustomerId).custName,
                Serial = x.Serial,
                Amount = x.Amount,
                Date = x.Date,
                Description = x.Description
            }).ToList();

            return result;
        }
    }
}
