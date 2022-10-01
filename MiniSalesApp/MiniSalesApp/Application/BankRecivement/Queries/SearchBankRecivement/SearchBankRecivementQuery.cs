using DevExpress.Xpo;
using MediatR;
using MiniSalesApp.Application.BankRecivement.Dtos;
using MiniSalesApp.Application.InterFaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MiniSalesApp.Application.BankRecivement.Queries.SearchBankRecivement
{
    public class SearchBankRecivementQuery : IRequest<List<BankRecivementDto>>
    {
        public int? Serial { get; set; }
        public int? CustomerId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public bool IsCustomerRecivement { get; set; }
        public int? BankId { get; set; }
    }

    public class SearchBankRecivementQueryHandler : IRequestHandler<SearchBankRecivementQuery, List<BankRecivementDto>>
    {
        private readonly IMiniSalesAppContext _context;
        public SearchBankRecivementQueryHandler(IMiniSalesAppContext miniSalesAppContext)
        {
            _context = miniSalesAppContext;
        }
        public async Task<List<BankRecivementDto>> Handle(SearchBankRecivementQuery request, CancellationToken cancellationToken)
        {
            List<BankRecivementDto> result = new List<BankRecivementDto>();

            IQueryable<Logic.BankAgreget.Bank> banks = _context.Banks.AsQueryable();

            if (request.Serial != null && request.Serial > default(int))
                banks = banks.Where(x => x.BankRecivementList.Any(z => z.Serial == request.Serial));
            else
            {
                banks = banks.Where(x => x.BankRecivementList.Any(z => z.IsCustomerRecivement == request.IsCustomerRecivement));

                if (request.BankId != null && request.BankId > default(int))
                    banks = banks.Where(x => x.BankRecivementList.Any(z => z.BankId == request.BankId));

                if (request.IsCustomerRecivement && request.CustomerId != null && request.CustomerId > default(int))
                    banks = banks.Where(x => x.BankRecivementList.Any(z => z.CustomerId == request.CustomerId));

                if (request.FromDate != DateTime.MinValue && request.ToDate != DateTime.MinValue)
                    banks = banks.Where(z => z.BankRecivementList.Any( x => x.Date >= request.FromDate && x.Date <= request.ToDate));
            }

            var resultList = await banks.SelectMany(x => x.BankRecivementList).ToListAsync();

            result = resultList.Select(x => new BankRecivementDto
            {
                BankId = x.BankId,
                BankRecivementId = x.BankRecivementId,
                IsCustomerRecivement = x.IsCustomerRecivement,
                CustomerId = x.CustomerId,
                Serial = x.Serial,
                Amount = x.Amount,
                Date = x.Date,
                Description = x.Description
            }).ToList();

            var customerIds = result.Select(x => x.CustomerId ?? 0);

            var suppliersNames = await _context.Customers
                .Where(z => customerIds.Contains(z.CustomerId))
                .Select(x => new { Id = x.CustomerId, Name = x.Name })
                .ToListAsync();

            result.ForEach(x => x.CustomerName = suppliersNames.FirstOrDefault(y => y.Id == (x.CustomerId ?? 0)).Name);

            return result;
        }
    }
}
