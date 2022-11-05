using DevExpress.XtraBars;
using DevExpress.XtraReports.UI;
using MediatR;
using MiniSalesApp.Application.Materials.Queries.GetMaterial;
using MiniSalesApp.Reports.Helper;
using MiniSalesApp.Reports.Items.ItemReport;
using MiniSalesApp.UI.Custom;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MiniSalesApp.Helper;

namespace MiniSalesApp.UI.Material.Reports
{
    public partial class frmMaterialReports : DevExpress.XtraBars.Ribbon.RibbonForm, IReportQuery
    {
        public readonly IMediator _mediator;
        XtraReport _report;
        public XtraReport Report => _report;

        public Form QueryForm => this;

        public frmMaterialReports(IMediator mediator)
        {
            InitializeComponent();
            _mediator = mediator;
            this.InitializeReportForm();
        }

        private async Task GetMaterials()
        {
            var getResult = await _mediator.Send(new GetMaterialQuery());

            chkLstMaterial.Properties.DataSource = getResult;
        }

        private void frmMaterialReports_Load(object sender, EventArgs e)
        {
            new frmProgressForm(GetMaterials).ShowDialog();
        }

        private void chkLstMaterial_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)
            {
                chkLstMaterial.EditValue = string.Empty;
                chkLstMaterial.RefreshEditValue();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            chkLstMaterial.EditValue = string.Empty;
            chkLstMaterial.RefreshEditValue();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var materialsIdList = string.IsNullOrEmpty(chkLstMaterial.EditValue.ToString()) ? null : chkLstMaterial.EditValue.ToString();
            _report = new rptItemsReport(materialsIdList);
            DialogResult = DialogResult.OK;
        }

        private void frmMaterialReports_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_report is null)
                _report = new rptItemsReport();
        }
    }
}