using MediatR;
using Microsoft.EntityFrameworkCore;
using MiniSalesApp.Application.Banks.Dtos;
using MiniSalesApp.Logic.BankAgreget;
using MiniSalesApp.Application.InterFaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MiniSalesApp.Application.Banks.Queries.SearchBank
{
    public class SearchBankQuery : IRequest<List<BankDto>>
    {
        public int? Serial { get; set; }
        public string Name { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }

    public class SearchBankQueryHandler : IRequestHandler<SearchBankQuery, List<BankDto>>
    {
        private readonly IMiniSalesAppContext _context;
        public SearchBankQueryHandler(IMiniSalesAppContext miniSalesAppContext)
        {
            _context = miniSalesAppContext;
        }
        public async Task<List<BankDto>> Handle(SearchBankQuery request, CancellationToken cancellationToken)
        {
            List<BankDto> result = new List<BankDto>();

            IQueryable<Bank> banks = _context.Banks.AsQueryable();

            if (request.Serial != null && request.Serial > default(int))
                banks = banks.Where(x => x.Serial == request.Serial);
            else
            {
                if (!string.IsNullOrEmpty(request.Name))
                    banks = banks.Where(x => x.Name.Contains(request.Name));

                if (request.FromDate != DateTime.MinValue && request.ToDate != DateTime.MinValue)
                    banks = banks.Where(x => x.Date >= request.FromDate && x.Date <= request.ToDate);
            }

            result = await (from bank in banks
                            select new BankDto
                            {
                                BankId = bank.BankId,
                                Serial = bank.Serial,
                                Date = bank.Date,
                                StartAmount = bank.StartAmount,
                                Name = bank.Name
                            }).ToListAsync();

            return result;
        }
    }
}
