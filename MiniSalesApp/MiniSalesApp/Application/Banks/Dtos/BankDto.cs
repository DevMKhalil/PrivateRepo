using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSalesApp.Application.Banks.Dtos
{
    public class BankDto
    {
        public int BankId { get; set; }
        public string Name { get; set; }
        public int Serial { get; set; }
        public decimal StartAmount { get; set; }
        public DateTime Date { get; set; }
    }
}
