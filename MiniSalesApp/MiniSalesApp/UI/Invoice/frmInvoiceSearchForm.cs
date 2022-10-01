using DevExpress.XtraBars;
using MediatR;
using MiniSalesApp.Application.Customers.Queries.GetCustomer;
using MiniSalesApp.Application.Invoice.Dtos;
using MiniSalesApp.Application.Invoice.Queries.GetInvoiceById;
using MiniSalesApp.Application.Invoice.Queries.SearchInvoice;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiniSalesApp.UI.Invoice
{
    public partial class frmInvoiceSearchForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private readonly IMediator _mediator;
        public InvoiceDto Invoice { get; private set; } = new InvoiceDto();
        public DateTime FromDate => dtFromDate.DateTime.Date;
        public DateTime ToDate => new DateTime(dtToDate.DateTime.Year, dtToDate.DateTime.Month, dtToDate.DateTime.Day, 23, 59, 59);

        public frmInvoiceSearchForm(IMediator mediator)
        {
            InitializeComponent();
            _mediator = mediator;
        }

        private async void RpsBtnSelect_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var row = grdVwInvoice.GetFocusedRow() as InvoiceForListDto;

            if (row == null)
                return;

            var InvoiceResult = await _mediator.Send(new GetInvoiceByIdQuery()
            {
                InvoiceId = row.InvoiceId
            });

            if (InvoiceResult.IsFailure)
            {
                Program.DisplayMessage(InvoiceResult.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Invoice = InvoiceResult.Value;
            DialogResult = DialogResult.OK;
        }

        private async void frmInvoiceSearchForm_Load(object sender, EventArgs e)
        {
            await GetCustomers();
            btnClear.PerformClick();
        }

        private async Task GetCustomers()
        {
            var getResult = await _mediator.Send(new GetCustomerQuery());

            lkUpCustomer.Properties.DataSource = getResult;
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            if (dtFromDate.DateTime == DateTime.MinValue || dtToDate.DateTime.Date < dtFromDate.DateTime.Date)
            {
                Program.DisplayMessage(Messages.InvalidDate, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                var result = await _mediator.Send(new SearchInvoiceQuery()
                {
                    Serial = (txtSerial.EditValue == null || string.IsNullOrEmpty(txtSerial.EditValue.ToString())) ? null : Convert.ToInt32(txtSerial.EditValue),
                    CustomerId = (int?)lkUpCustomer.EditValue,
                    FromDate = FromDate,
                    ToDate = ToDate
                });

                grdCtrInvoice.DataSource = result;
            }
            catch (Exception ex)
            {
                Program.DisplayMessage(ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtSerial.EditValue = null;
            lkUpCustomer.EditValue = null;
            dtFromDate.DateTime = DateTime.Now;
            dtToDate.DateTime = DateTime.Now;
        }

        private void grdVwInvoice_DoubleClick(object sender, EventArgs e)
        {
            RpsBtnSelect_ButtonClick(null, null);
        }

        private void txtSerial_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void lkUpCustomer_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void dtFromDate_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void dtToDate_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}