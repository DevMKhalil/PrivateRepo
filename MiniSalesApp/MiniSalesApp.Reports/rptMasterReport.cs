using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace MiniSalesApp.Reports
{
    public partial class rptMasterReport : DevExpress.XtraReports.UI.XtraReport
    {
        public string ReportTitle { get; set; }
        public rptMasterReport()
        {
            InitializeComponent();
        }
    }
}
