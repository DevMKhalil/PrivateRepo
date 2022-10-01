using MediatR;
using Microsoft.EntityFrameworkCore;
using MiniSalesApp.Application.Customers.Dtos;
using MiniSalesApp.Application.InterFaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MiniSalesApp.Application.Customers.Queries.GetCustomer
{
    public class GetCustomerQuery : IRequest<List<CustomerDto>>
    {
    }

    public class GetCustomerQueryHandler : IRequestHandler<GetCustomerQuery, List<CustomerDto>>
    {
        private readonly IMiniSalesAppContext _context;
        public GetCustomerQueryHandler(IMiniSalesAppContext miniSalesAppContext)
        {
            _context = miniSalesAppContext;
        }
        public async Task<List<CustomerDto>> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
        {
            List<CustomerDto> result = new List<CustomerDto>();

            result = await _context.Customers.Select(x => new CustomerDto
            {
                CustomerId = x.CustomerId,
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
