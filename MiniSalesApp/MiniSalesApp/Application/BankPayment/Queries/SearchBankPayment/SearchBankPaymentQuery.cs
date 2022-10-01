using DevExpress.Xpo;
using MediatR;
using MiniSalesApp.Application.BankPayment.Dtos;
using MiniSalesApp.Application.StoreRecivement.Dtos;
using MiniSalesApp.Application.InterFaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MiniSalesApp.Application.BankPayment.Queries.SearchBankPayment
{
    public class SearchBankPaymentQuery : IRequest<List<BankPaymentDto>>
    {
        public int? Serial { get; set; }
        public int? SupplierId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public bool IsSupplierPayment { get; set; }
        public int? BankId { get; set; }
    }

    public class SearchBankPaymentQueryHandler : IRequestHandler<SearchBankPaymentQuery, List<BankPaymentDto>>
    {
        private readonly IMiniSalesAppContext _context;
        public SearchBankPaymentQueryHandler(IMiniSalesAppContext miniSalesAppContext)
        {
            _context = miniSalesAppContext;
        }
        public async Task<List<BankPaymentDto>> Handle(SearchBankPaymentQuery request, CancellationToken cancellationToken)
        {
            List<BankPaymentDto> result = new List<BankPaymentDto>();

            IQueryable<Logic.BankAgreget.Bank> banks = _context.Banks.AsQueryable();

            if (request.Serial != null && request.Serial > default(int))
                banks = banks.Where(x => x.BankPaymentList.Any(z => z.Serial == request.Serial));
            else
            {
                banks = banks.Where(x => x.BankPaymentList.Any(z => z.IsSupplierPayment == request.IsSupplierPayment));

                if (request.BankId != null && request.BankId > default(int))
                    banks = banks.Where(x => x.BankPaymentList.Any(z => z.BankId == request.BankId));

                if (request.IsSupplierPayment && request.SupplierId != null && request.SupplierId > default(int))
                    banks = banks.Where(x => x.BankPaymentList.Any(z => z.SupplierId == request.SupplierId));

                if (request.FromDate != DateTime.MinValue && request.ToDate != DateTime.MinValue)
                    banks = banks.Where(z => z.BankPaymentList.Any( x => x.Date >= request.FromDate && x.Date <= request.ToDate));
            }

            var resultList = await banks.SelectMany(x => x.BankPaymentList).ToListAsync();

            result = resultList.Select(x => new BankPaymentDto
            {
                BankId = x.BankId,
                BankPaymentId = x.BankPaymentId,
                IsSupplierPayment = x.IsSupplierPayment,
                SupplierId = x.SupplierId,
                Serial = x.Serial,
                Amount = x.Amount,
                Date = x.Date,
                Description = x.Description
            }).ToList();

            var suppliersIds = result.Select(x => x.SupplierId ?? 0);

            var suppliersNames = await _context.Suppliers
                .Where(z => suppliersIds.Contains(z.SupplierId))
                .Select(x => new { Id = x.SupplierId, Name = x.Name })
                .ToListAsync();

            result.ForEach(x => x.SupplierName = suppliersNames.FirstOrDefault(y => y.Id == (x.SupplierId ?? 0)).Name);

            return result;
        }
    }
}
