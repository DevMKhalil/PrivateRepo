using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSalesApp.Application.Bill.Dtos
{
    public class BillForListDto
    {
        public int BillId { get; set; }
        public int Serial { get; set; }
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public decimal Total { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalAfterDiscount { get; set; }
        public string Discription { get; set; }
        public DateTime Date { get; set; }
    }
}
