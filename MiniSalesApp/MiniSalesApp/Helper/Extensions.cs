using DevExpress.XtraBars.Ribbon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSalesApp.Helper
{
    public static class Extensions
    {
        public static void InitializeReportForm(this RibbonForm ribbonForm)
        {
            ribbonForm.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ribbonForm.IconOptions.ShowIcon = false;
            ribbonForm.MaximizeBox = false;
            ribbonForm.MinimizeBox = false;
            ribbonForm.Ribbon.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.False;
        }
    }
}
