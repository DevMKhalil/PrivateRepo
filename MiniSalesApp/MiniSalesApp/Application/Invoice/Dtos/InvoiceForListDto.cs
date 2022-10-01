using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSalesApp.Application.Invoice.Dtos
{
    public class InvoiceForListDto
    {
        public int InvoiceId { get; set; }
        public int Serial { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public decimal Total { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalAfterDiscount { get; set; }
        public string Discription { get; set; }
        public DateTime Date { get; set; }
    }
}
