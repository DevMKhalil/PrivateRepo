using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraReports.UI;
using MiniSalesApp.Reports;
using MiniSalesApp.Reports.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiniSalesApp.UI.Reports
{
    public partial class frmReportViewer : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private XtraReport _report;
        private readonly IReportQuery _reportQuery;
       
        public XtraReport Report 
        {   get 
            {
                if (_reportQuery is not null && _reportQuery.Report is not null)
                {
                    this.Text = (_reportQuery.Report as rptMasterReport).ReportTitle;
                    return _reportQuery.Report;
                }
                else
                {
                    this.Text = (_report as rptMasterReport).ReportTitle;
                    return _report;
                }
            }
        }

        public Form Form => _reportQuery.QueryForm;

        public frmReportViewer()
        {
            InitializeComponent();
            Disposed += FrmReportViewer_Disposed;
        }

        public frmReportViewer(Form container) : this()
        {
            if (container is not null)
                MdiParent = container;
        }
      
        public frmReportViewer(Form container,XtraReport report) : this(container)
        {
            _report = report;
        }

        public frmReportViewer(Form container, IReportQuery reportQuery) : this(container)
        {
            _reportQuery = reportQuery;
            _reportQuery.QueryForm.KeyDown += QueryForm_KeyDown;
            _reportQuery.QueryForm.KeyPreview = true;
            (_reportQuery.QueryForm as RibbonForm).Ribbon.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.False;
            (_reportQuery.QueryForm as RibbonForm).ShowIcon = false;
        }

        private void QueryForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                (sender as Form).DialogResult = DialogResult.Cancel;
        }

        private void frmReportViewer_Shown(object sender, EventArgs e)
        {
            if (_reportQuery is not null)
                Form.ShowDialog(this);

            showReport(Report);
        }

        private void showReport(XtraReport report)
        {
            report.RightToLeftLayout = DevExpress.XtraReports.UI.RightToLeftLayout.Yes;
            report.RightToLeft = DevExpress.XtraReports.UI.RightToLeft.Yes;
            Invalidate();
            Update();
            documentViewer1.PrintingSystem = report.PrintingSystem;

            if ((Report as IDrillDownReport) is not null)
                (Report as IDrillDownReport).SetPrinttedFlagValue(false);

            report.CreateDocument(true);
        }

        private void FrmReportViewer_Disposed(object sender, EventArgs e)
        {
            if (Report is not null)
            {
                try
                {
                    btnStop.PerformClick();
                    Report.StopPageBuilding();
                    Report.Dispose();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + ex.InnerException);
                }
            }
        }

        private void frmReportViewer_FormClosing(object sender, FormClosingEventArgs e)
        {
            Report?.StopPageBuilding();
        }

        private void btnQueryPobUp_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (_reportQuery is not null)
            {
                if (Form.ShowDialog(this) == DialogResult.OK)
                    showReport(Report);
            }
        }
    }
}