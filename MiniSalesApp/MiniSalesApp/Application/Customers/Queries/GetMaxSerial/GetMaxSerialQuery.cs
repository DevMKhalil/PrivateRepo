using MediatR;
using Microsoft.EntityFrameworkCore;
using MiniSalesApp.Application.InterFaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MiniSalesApp.Application.Customers.Queries.GetMaxSerial
{
    public class GetMaxSerialQuery : IRequest<int>
    {
    }

    public class GetMaxSerialQueryHandler : IRequestHandler<GetMaxSerialQuery, int>
    {
        private readonly IMiniSalesAppContext _context;
        public GetMaxSerialQueryHandler(IMiniSalesAppContext miniSalesAppContext)
        {
            _context = miniSalesAppContext;
        }
        public async Task<int> Handle(GetMaxSerialQuery request, CancellationToken cancellationToken)
        {
            return await _context.Customers.MaxAsync(x => (int?)x.Serial) ?? 0;
        }
    }
}
