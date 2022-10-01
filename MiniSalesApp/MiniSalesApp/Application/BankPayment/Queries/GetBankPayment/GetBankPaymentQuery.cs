using MediatR;
using Microsoft.EntityFrameworkCore;
using MiniSalesApp.Application.BankPayment.Dtos;
using MiniSalesApp.Application.InterFaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MiniSalesApp.Application.StoreRecivement.Queries.GetStoreRecivement
{
    public class GetBankPaymentQuery : IRequest<List<BankPaymentDto>>
    {
    }

    public class GetBankPaymentQueryHandler : IRequestHandler<GetBankPaymentQuery, List<BankPaymentDto>>
    {
        private readonly IMiniSalesAppContext _context;
        public GetBankPaymentQueryHandler(IMiniSalesAppContext miniSalesAppContext)
        {
            _context = miniSalesAppContext;
        }
        public async Task<List<BankPaymentDto>> Handle(GetBankPaymentQuery request, CancellationToken cancellationToken)
        {
            List<BankPaymentDto> result = new List<BankPaymentDto>();

            var resultList = await _context.Banks.SelectMany(x => x.BankPaymentList).ToListAsync();

            var custIds = resultList.Select(x => x.SupplierId ?? 0);

            var custList = await _context.Suppliers
                .Where(x => custIds.Contains(x.SupplierId))
                .Select(z => new { supId = z.SupplierId, supName = z.Name }).ToListAsync();

            result = resultList.Select(x => new BankPaymentDto
            {
                BankId = x.BankId,
                BankPaymentId = x.BankPaymentId,
                IsSupplierPayment = x.IsSupplierPayment,
                SupplierId = x.SupplierId,
                SupplierName = x.SupplierId == null ? string.Empty : custList.FirstOrDefault(t => t.supId == x.SupplierId).supName,
                Serial = x.Serial,
                Amount = x.Amount,
                Date = x.Date,
                Description = x.Description
            }).ToList();

            return result;
        }
    }
}
