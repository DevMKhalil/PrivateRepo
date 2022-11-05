using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSalesApp.Reports.Items.ItemPrint
{
    public partial class rptItemReport : rptMasterReport
    {
        private readonly int _materialId;

        public rptItemReport()
        {
            InitializeComponent();
            ReportTitle = Messages.Material;
            Name = "rptItem";
        }

        public rptItemReport(int materialId):this()
        {
            _materialId = materialId;
        }

        private void rptItemReport_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            materialTableAdapter.Connection.ConnectionString = Program.ConnectionString;

            materialTableAdapter.Fill(dsItem1.Material, _materialId);
        }
    }
}
