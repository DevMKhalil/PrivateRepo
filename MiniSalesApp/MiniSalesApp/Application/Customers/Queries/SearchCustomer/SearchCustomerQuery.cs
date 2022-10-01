using MediatR;
using Microsoft.EntityFrameworkCore;
using MiniSalesApp.Application.Customers.Dtos;
using MiniSalesApp.Logic.CustomerAgreget;
using MiniSalesApp.Application.InterFaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MiniSalesApp.Application.Customers.Queries.SearchCustomer
{
    public class SearchCustomerQuery : IRequest<List<CustomerDto>>
    {
        public string Name { get; set; }
        public int? Serial { get; set; }
        public string Phone { get; set; }
    }

    public class SearchCustomerQueryHandler : IRequestHandler<SearchCustomerQuery, List<CustomerDto>>
    {
        private readonly IMiniSalesAppContext _context;
        public SearchCustomerQueryHandler(IMiniSalesAppContext miniSalesAppContext)
        {
            _context = miniSalesAppContext;
        }
        public async Task<List<CustomerDto>> Handle(SearchCustomerQuery request, CancellationToken cancellationToken)
        {
            List<CustomerDto> result = new List<CustomerDto>();

            IQueryable<Customer> Customers = (from Customer in _context.Customers select Customer);

            if (!string.IsNullOrEmpty(request.Name))
                Customers = Customers.Where(x => x.Name.Contains(request.Name));

            if (request.Serial != null && request.Serial > default(int))
                Customers = Customers.Where(x => x.Serial == request.Serial);

            if (!string.IsNullOrEmpty(request.Phone))
                Customers = Customers.Where(x => x.Phone.Contains(request.Phone));

            result = await (from Customer in Customers
                            select new CustomerDto
                            {
                                CustomerId = Customer.CustomerId,
                                Name = Customer.Name,
                                Phone = Customer.Phone,
                                Serial = Customer.Serial,
                                Address = Customer.Address,
                                Balance = Customer.Balance
                            }).ToListAsync();

            return result;
        }
    }
}
