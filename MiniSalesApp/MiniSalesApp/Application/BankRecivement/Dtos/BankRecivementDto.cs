using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSalesApp.Application.BankRecivement.Dtos
{
    public class BankRecivementDto
    {
        public int BankRecivementId { get; set; }
        public int Serial { get; set; }
        public bool IsCustomerRecivement { get; set; }
        public int? CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public int BankId { get; set; }
        public DateTime Date { get; set; }
    }
}
