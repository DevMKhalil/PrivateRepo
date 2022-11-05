using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSalesApp.Reports.Helper
{
    public interface IDrillDownReport
    {
        public bool IsPrinted { get; set; } 

        void SetPrinttedFlagValue(bool isPrinted);
    }
}
