using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSalesApp.Application.StoreRecivement.Dtos
{
    public class StoreRecivementDto
    {
        public StoreRecivementDto()
        {

        }

        public int StoreRecivementId { get; set; }
        public int Serial { get; set; }
        public bool IsCustomerRecivement { get; set; }
        public int? CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public int StoreDailyId { get; set; }
        public DateTime Date { get; set; }
    }
}
