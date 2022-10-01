using MediatR;
using Microsoft.EntityFrameworkCore;
using MiniSalesApp.Application.Suppliers.Dtos;
using MiniSalesApp.Application.InterFaces;
using MiniSalesApp.Logic.SupplierAgreget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MiniSalesApp.Application.Suppliers.Queries.SearchSupplier
{
    public class SearchSupplierQuery : IRequest<List<SupplierDto>>
    {
        public string Name { get; set; }
        public int? Serial { get; set; }
        public string Phone { get; set; }
    }

    public class SearchSupplierQueryHandler : IRequestHandler<SearchSupplierQuery, List<SupplierDto>>
    {
        private readonly IMiniSalesAppContext _context;
        public SearchSupplierQueryHandler(IMiniSalesAppContext miniSalesAppContext)
        {
            _context = miniSalesAppContext;
        }
        public async Task<List<SupplierDto>> Handle(SearchSupplierQuery request, CancellationToken cancellationToken)
        {
            List<SupplierDto> result = new List<SupplierDto>();

            IQueryable<Supplier> Suppliers = (from Supplier in _context.Suppliers select Supplier);

            if (!string.IsNullOrEmpty(request.Name))
                Suppliers = Suppliers.Where(x => x.Name.Contains(request.Name));

            if (request.Serial != null && request.Serial > default(int))
                Suppliers = Suppliers.Where(x => x.Serial == request.Serial);

            if (!string.IsNullOrEmpty(request.Phone))
                Suppliers = Suppliers.Where(x => x.Phone.Contains(request.Phone));

            result = await (from Supplier in Suppliers
                            select new SupplierDto
                            {
                                SupplierId = Supplier.SupplierId,
                                Name = Supplier.Name,
                                Phone = Supplier.Phone,
                                Serial = Supplier.Serial,
                                Address = Supplier.Address,
                                Balance = Supplier.Balance
                            }).ToListAsync();

            return result;
        }
    }
}
