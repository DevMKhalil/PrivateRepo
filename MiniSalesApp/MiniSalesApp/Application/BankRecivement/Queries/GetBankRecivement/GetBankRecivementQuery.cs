using MediatR;
using Microsoft.EntityFrameworkCore;
using MiniSalesApp.Application.BankRecivement.Dtos;
using MiniSalesApp.Application.InterFaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MiniSalesApp.Application.BankRecivement.Queries.GetBankRecivement
{
    public class GetBankRecivementQuery : IRequest<List<BankRecivementDto>>
    {
    }

    public class GetBankRecivementQueryHandler : IRequestHandler<GetBankRecivementQuery, List<BankRecivementDto>>
    {
        private readonly IMiniSalesAppContext _context;
        public GetBankRecivementQueryHandler(IMiniSalesAppContext miniSalesAppContext)
        {
            _context = miniSalesAppContext;
        }
        public async Task<List<BankRecivementDto>> Handle(GetBankRecivementQuery request, CancellationToken cancellationToken)
        {
            List<BankRecivementDto> result = new List<BankRecivementDto>();

            var resultList = await _context.Banks.SelectMany(x => x.BankRecivementList).ToListAsync();

            var custIds = resultList.Select(x => x.CustomerId ?? 0);

            var custList = await _context.Customers
                .Where(x => custIds.Contains(x.CustomerId))
                .Select(z => new { supId = z.CustomerId, supName = z.Name }).ToListAsync();

            result = resultList.Select(x => new BankRecivementDto
            {
                BankId = x.BankId,
                BankRecivementId = x.BankRecivementId,
                IsCustomerRecivement = x.IsCustomerRecivement,
                CustomerId = x.CustomerId,
                CustomerName = x.CustomerId == null ? string.Empty : custList.FirstOrDefault(t => t.supId == x.CustomerId).supName,
                Serial = x.Serial,
                Amount = x.Amount,
                Date = x.Date,
                Description = x.Description
            }).ToList();

            return result;
        }
    }
}
