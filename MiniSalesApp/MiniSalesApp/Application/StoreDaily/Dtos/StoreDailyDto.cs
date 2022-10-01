using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSalesApp.Application.StoreDaily.Dtos
{
    public class StoreDailyDto
    {
        public int StoreDailyId { get; set; }
        public int Serial { get; set; }
        public decimal StartAmount { get; set; }
        public decimal EndAmount { get; set; }
        public DateTime StartDate { get; set; }
    }
}
