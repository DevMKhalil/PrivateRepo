using MediatR;
using Microsoft.EntityFrameworkCore;
using MiniSalesApp.Application.Suppliers.Dtos;
using MiniSalesApp.Application.InterFaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MiniSalesApp.Application.Suppliers.Queries.GetSupplier
{
    public class GetSupplierQuery : IRequest<List<SupplierDto>>
    {
    }

    public class GetSupplierQueryHandler : IRequestHandler<GetSupplierQuery, List<SupplierDto>>
    {
        private readonly IMiniSalesAppContext _context;
        public GetSupplierQueryHandler(IMiniSalesAppContext miniSalesAppContext)
        {
            _context = miniSalesAppContext;
        }
        public async Task<List<SupplierDto>> Handle(GetSupplierQuery request, CancellationToken cancellationToken)
        {
            List<SupplierDto> result = new List<SupplierDto>();

            result = await _context.Suppliers.Select(x => new SupplierDto
            {
                SupplierId = x.SupplierId,
                Serial = x.Serial,
                Name = x.Name,
                Address = x.Address,
                Phone = x.Phone,
                Balance = x.Balance
            }).ToListAsync();

            return result;
        }
    }
}
