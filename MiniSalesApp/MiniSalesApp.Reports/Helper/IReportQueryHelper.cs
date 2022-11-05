using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiniSalesApp.Reports.Helper
{
    public interface IReportQuery
    {
        public DevExpress.XtraReports.UI.XtraReport Report { get;}
        public Form QueryForm { get;}
    }
}
