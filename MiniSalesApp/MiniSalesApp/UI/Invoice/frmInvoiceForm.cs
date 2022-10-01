using DevExpress.XtraBars;
using MediatR;
using MiniSalesApp.Application.Invoice.Dtos;
using MiniSalesApp.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MiniSalesApp.Application.Invoice.Commands.DeleteInvoice;
using MiniSalesApp.Application.Customers.Queries.GetCustomer;
using MiniSalesApp.Application.Materials.Queries.GetMaterial;
using MiniSalesApp.Application.Invoice.Commands.CreateInvoice;
using MiniSalesApp.Application.Invoice.Commands.UpdateInvoice;
using MiniSalesApp.Application.Invoice.Queries.GetMaxSerial;
using MiniSalesApp.UI.Custom;
using CSharpFunctionalExtensions;

namespace MiniSalesApp.UI.Invoice
{
    public partial class frmInvoiceForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private List<ModuleRibbonButton> ribbonButtons;
        private FormStates _currentState;
        InvoiceDto invoice;
        public readonly IMediator _mediator;

        private FormStates currentState
        {
            get
            {
                return _currentState;
            }
            set
            {
                _currentState = value;
                ModuleRibbonButton.SetButtonsState(ribbonButtons, value);
            }
        }

        public frmInvoiceForm(IMediator mediator)
        {
            InitializeComponent();

            _mediator = mediator;
            ribbonButtons = new List<ModuleRibbonButton>();
            ribbonButtons.Add(new ModuleRibbonButton(brBtnNew, BtnType.New, "", "", new FormStates[] { FormStates.NormalFirstoaded }));
            ribbonButtons.Add(new ModuleRibbonButton(brBtnReset, BtnType.Reset, "", "", new FormStates[] { FormStates.AddingNew, FormStates.Editing }));
            ribbonButtons.Add(new ModuleRibbonButton(brBtnSave, BtnType.Save, "", "", new FormStates[] { FormStates.AddingNew, FormStates.Editing }));
            ribbonButtons.Add(new ModuleRibbonButton(brBtnDelete, BtnType.Delete, "", "", new FormStates[] { FormStates.Editing }));
            ribbonButtons.Add(new ModuleRibbonButton(brBtnOpen, BtnType.Reset, "", "", new FormStates[] { FormStates.NormalFirstoaded }));
            currentState = FormStates.NormalFirstoaded;
            SetControlStatus(false);
            ClearControls();
            
        }

        private void RpsBtnDelete_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var row = grdVwMaterial.GetFocusedRow() as InvoiceDetailDto;

            if (row == null)
                return;

            invoice.InvoiceDetailList.Remove(row);
            grdCtrMaterial.RefreshDataSource();
            RefreshTotal();
        }

        private void RpsSearchLkUpMaterial_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            var materials = grdCtrMaterial.DataSource as BindingList<InvoiceDetailDto>;

            if (e.NewValue != null && !string.IsNullOrEmpty(e.NewValue.ToString()) && Convert.ToInt32(e.NewValue) > 0)
            {
                if (materials.Where(x => x.MaterialId == Convert.ToInt32(e.NewValue)).Count() > 0)
                {
                    e.Cancel = true;
                    Program.DisplayMessage(Messages.MaterialIsAlreadyExist, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void GrdVwMaterial_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            var row = grdVwMaterial.GetFocusedRow() as InvoiceDetailDto;

            if (row == null)
                return;

            if (row.Quantity <= 0)
            {
                e.Valid = false;
                e.ErrorText = Messages.EnterQuantity;
            }
            else if (row.SellPrice <= 0)
            {
                e.Valid = false;
                e.ErrorText = Messages.EnterPrice;
            }
            else if (row.MaterialId <= 0)
            {
                e.Valid = false;
                e.ErrorText = Messages.EnterMaterial;
            }

            if (e.Valid)
                RefreshTotal();
        }

        private void RefreshTotal()
        {
            if (invoice == null)
                return;

            txttotal.EditValue = invoice.InvoiceDetailList.Sum(x => x.SellPrice * x.Quantity);
            txtTotalAfterDiscount.EditValue = Convert.ToDecimal(txttotal.EditValue) - Convert.ToDecimal(txtDiscount.EditValue);
        }

        private void SetControlStatus(bool status)
        {
            lkUpCustomer.ReadOnly = !status;
            txtDiscount.ReadOnly = !status;
            dtDate.ReadOnly = !status;
            memoDiscription.ReadOnly = !status;
            grdVwMaterial.OptionsBehavior.Editable = status;
        }

        private void ClearControls()
        {
            txtSerial.EditValue = default(int);
            txttotal.EditValue = default(decimal);
            txtTotalAfterDiscount.EditValue = default(decimal);
            lkUpCustomer.EditValue = default(int);
            txtDiscount.EditValue = default(decimal);
            dtDate.EditValue = DateTime.Now;
            memoDiscription.EditValue = string.Empty;
            grdCtrMaterial.DataSource = null;
        }

        private void FillControls()
        {
            txtSerial.EditValue = invoice.Serial;
            txttotal.EditValue = invoice.Total;
            txtTotalAfterDiscount.EditValue = invoice.TotalAfterDiscount;
            lkUpCustomer.EditValue = invoice.CustomerId;
            txtDiscount.EditValue = invoice.Discount;
            dtDate.EditValue = invoice.Date;
            memoDiscription.EditValue = invoice.Discription;
            grdCtrMaterial.DataSource = new BindingList<InvoiceDetailDto>(invoice.InvoiceDetailList);
        }

        private void FillData()
        {
            grdVwMaterial.PostEditor();
            invoice.CustomerId = Convert.ToInt32(lkUpCustomer.EditValue);
            invoice.Discount = Convert.ToDecimal(txtDiscount.EditValue);
            invoice.Date = dtDate.DateTime;
            invoice.Discription = memoDiscription.Text;
            invoice.InvoiceDetailList = (grdCtrMaterial.DataSource as BindingList<InvoiceDetailDto>).ToList();
        }

        private bool ValidateIncoice()
        {
            StringBuilder msg = new StringBuilder();

            if (invoice.CustomerId <= 0)
                msg.AppendLine(Messages.SellectCustomer);

            if (invoice.Discount < 0)
                msg.AppendLine(Messages.DiscountCantBeNegative);

            if (invoice.Date == DateTime.MinValue)
                msg.AppendLine(Messages.InvalidDate);

            if (invoice.InvoiceDetailList.Count <= 0)
                msg.AppendLine(Messages.EnterMaterials);

            if (msg.Length > 0)
            {
                Program.DisplayMessage(msg.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void TxtDiscount_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (e.NewValue != null && !string.IsNullOrEmpty(e.NewValue.ToString()) && e.NewValue.ToString().Contains("-"))
                e.Cancel = true;
        }

        private void RpsTxtQty_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (e.NewValue != null && !string.IsNullOrEmpty(e.NewValue.ToString()) && e.NewValue.ToString().Contains("-"))
                e.Cancel = true;
        }

        private async void brBtnNew_ItemClick(object sender, ItemClickEventArgs e)
        {
            invoice = new InvoiceDto();
            ClearControls();
            SetControlStatus(true);
            grdCtrMaterial.DataSource = new BindingList<InvoiceDetailDto>(invoice.InvoiceDetailList);
            currentState = FormStates.AddingNew;

            var result = await _mediator.Send(new GetMaxSerialQuery());

            txtSerial.EditValue = (result + 1).ToString();
        }

        private void brBtnReset_ItemClick(object sender, ItemClickEventArgs e)
        {
            invoice = new InvoiceDto();
            ClearControls();
            SetControlStatus(false);
            grdCtrMaterial.DataSource = new BindingList<InvoiceDetailDto>(invoice.InvoiceDetailList);
            currentState = FormStates.NormalFirstoaded;
        }

        private async Task Delete(int invoiceId)
        {
            var result = Result.Success();

            result = await _mediator.Send(new DeleteInvoiceCommand() { InvoiceId = invoiceId });

            if (result.IsFailure)
            {
                Program.DisplayMessage(result.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void brBtnDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Program.DisplayMessage(Messages.DeleteConfirmation, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            try
            {
                var progrssForm = new frmProgressForm();
                progrssForm.SetParamitraizedAction(Delete, invoice.InvoiceId);
                progrssForm.ShowDialog();

                ClearControls();
                SetControlStatus(false);
                Program.DisplayMessage(Messages.DeleteSuccessfuly, MessageBoxButtons.OK, MessageBoxIcon.Information);
                currentState = FormStates.NormalFirstoaded;
            }
            catch (Exception ex)
            {
                Program.DisplayMessage(ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task GetCustomers()
        {
            var getResult = await _mediator.Send(new GetCustomerQuery());

            lkUpCustomer.Properties.DataSource = getResult;
        }

        private async Task GetMaterials()
        {
            var getResult = await _mediator.Send(new GetMaterialQuery());

            rpsSearchLkUpMaterial.DataSource = getResult;
        }

        private void frmInvoiceForm_Activated(object sender, EventArgs e)
        {
            new frmProgressForm(GetCustomers).ShowDialog();
            new frmProgressForm(GetMaterials).ShowDialog();
        }

        private async Task Save(InvoiceDto invoice)
        {
            var result = Result.Success();

            if (invoice.InvoiceId <= 0)
                result = await _mediator.Send(new CreateInvoiceCommand() { invoice = invoice });
            else
                result = await _mediator.Send(new UpdateInvoiceCommand() { invoice = invoice });

            if (result.IsFailure)
            {
                Program.DisplayMessage(result.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void brBtnSave_ItemClick(object sender, ItemClickEventArgs e)
        {
            FillData();
            if (!ValidateIncoice())
                return;

            try
            {
                var progrssForm = new frmProgressForm();
                progrssForm.SetParamitraizedAction(Save, invoice);
                progrssForm.ShowDialog();

                ClearControls();
                SetControlStatus(false);
                Program.DisplayMessage(Messages.SaveSuccessfuly, MessageBoxButtons.OK, MessageBoxIcon.Information);
                currentState = FormStates.NormalFirstoaded;
            }
            catch (Exception ex)
            {
                Program.DisplayMessage(ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void brBtnOpen_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (frmInvoiceSearchForm frm = new frmInvoiceSearchForm(_mediator))
            {
                frm.ShowDialog(this);

                if (frm.DialogResult == DialogResult.OK)
                {
                    invoice = frm.Invoice;
                    ClearControls();
                    SetControlStatus(true);
                    FillControls();
                    currentState = FormStates.Editing;
                }
            }
        }

        private void txtDiscount_EditValueChanged(object sender, EventArgs e)
        {
            RefreshTotal();
        }
    }
}