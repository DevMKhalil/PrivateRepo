using MediatR;
using Microsoft.EntityFrameworkCore;
using MiniSalesApp.Application.Banks.Dtos;
using MiniSalesApp.Application.InterFaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MiniSalesApp.Application.Banks.Queries.GetBank
{
    public class GetBankQuery : IRequest<List<BankDto>>
    {
    }

    public class GetBankQueryHandler : IRequestHandler<GetBankQuery, List<BankDto>>
    {
        private readonly IMiniSalesAppContext _context;
        public GetBankQueryHandler(IMiniSalesAppContext miniSalesAppContext)
        {
            _context = miniSalesAppContext;
        }
        public async Task<List<BankDto>> Handle(GetBankQuery request, CancellationToken cancellationToken)
        {
            List<BankDto> result = new List<BankDto>();

            result = await _context.Banks.Select(x => new BankDto
            {
                BankId = x.BankId,
                Serial = x.Serial,
                Date = x.Date,
                StartAmount = x.StartAmount,
                Name = x.Name
            }).ToListAsync();

            return result;
        }
    }
}
