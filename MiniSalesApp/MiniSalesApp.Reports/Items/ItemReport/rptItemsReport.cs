using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSalesApp.Reports.Items.ItemReport
{
    public partial class rptItemsReport : rptMasterReport
    {
        private readonly string _itemsIds;

        public rptItemsReport()
        {
            InitializeComponent();
            ReportTitle = Messages.Materials;
            Name = "rptItems";
        }

        public rptItemsReport(string itemsIds) : this()
        {
            _itemsIds = itemsIds;
        }

        private void rptItemsReport_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            materialTableAdapter.Connection.ConnectionString = Program.ConnectionString;

            materialTableAdapter.Fill(dsItems1.Material, _itemsIds);
        }
    }
}
