using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSalesApp.Application.BankPayment.Dtos
{
    public class BankPaymentDto
    {
        public BankPaymentDto()
        {

        }

        public int BankPaymentId { get; set; }
        public int Serial { get; set; }
        public bool IsSupplierPayment { get; set; }
        public int? SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public int BankId { get; set; }
        public DateTime Date { get; set; }
    }
}
