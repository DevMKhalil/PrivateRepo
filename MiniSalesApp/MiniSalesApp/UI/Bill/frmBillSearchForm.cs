using DevExpress.XtraBars;
using MediatR;
using MiniSalesApp.Application.Bill.Dtos;
using MiniSalesApp.Application.Bill.Queries.GetBillById;
using MiniSalesApp.Application.Bill.Queries.SearchBill;
using MiniSalesApp.Application.Suppliers.Queries.GetSupplier;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiniSalesApp.UI.Bill
{
    public partial class frmBillSearchForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private readonly IMediator _mediator;
        public BillDto Bill { get; private set; } = new BillDto();
        public DateTime FromDate => dtFromDate.DateTime.Date;
        public DateTime ToDate => new DateTime(dtToDate.DateTime.Year, dtToDate.DateTime.Month, dtToDate.DateTime.Day, 23, 59, 59);

        public frmBillSearchForm(IMediator mediator)
        {
            InitializeComponent();
            _mediator = mediator;
        }

        private async void RpsBtnSelect_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var row = grdVwBill.GetFocusedRow() as BillForListDto;

            if (row == null)
                return;

            var BillResult = await _mediator.Send(new GetBillByIdQuery()
            {
                BillId = row.BillId
            });

            if (BillResult.IsFailure)
            {
                Program.DisplayMessage(BillResult.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Bill = BillResult.Value;
            DialogResult = DialogResult.OK;
        }

        private async void frmBillSearchForm_Load(object sender, EventArgs e)
        {
            await GetSuppliers();
            btnClear.PerformClick();
        }

        private async Task GetSuppliers()
        {
            var getResult = await _mediator.Send(new GetSupplierQuery());

            lkUpSupplier.Properties.DataSource = getResult;
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
                var result = await _mediator.Send(new SearchBillQuery()
                {
                    Serial = (txtSerial.EditValue == null || string.IsNullOrEmpty(txtSerial.EditValue.ToString())) ? null : Convert.ToInt32(txtSerial.EditValue),
                    SupplierId = (int?)lkUpSupplier.EditValue,
                    FromDate = FromDate,
                    ToDate = ToDate
                });

                grdCtrBill.DataSource = result;
            }
            catch (Exception ex)
            {
                Program.DisplayMessage(ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtSerial.EditValue = null;
            lkUpSupplier.EditValue = null;
            dtFromDate.DateTime = DateTime.Now;
            dtToDate.DateTime = DateTime.Now;
        }

        private void grdVwBill_DoubleClick(object sender, EventArgs e)
        {
            RpsBtnSelect_ButtonClick(null, null);
        }
    }
}