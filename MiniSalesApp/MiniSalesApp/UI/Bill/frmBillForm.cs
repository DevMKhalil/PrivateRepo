using CSharpFunctionalExtensions;
using DevExpress.XtraBars;
using MediatR;
using MiniSalesApp.Application.Bill.Commands.DeleteBill;
using MiniSalesApp.Application.Bill.Dtos;
using MiniSalesApp.Application.Bill.Queries.GetMaxSerial;
using MiniSalesApp.Application.Invoice.Commands.CreateInvoice;
using MiniSalesApp.Application.Invoice.Commands.UpdateInvoice;
using MiniSalesApp.Application.Materials.Queries.GetMaterial;
using MiniSalesApp.Application.Suppliers.Queries.GetSupplier;
using MiniSalesApp.Classes;
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

namespace MiniSalesApp.UI.Bill
{
    public partial class frmBillForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private List<ModuleRibbonButton> ribbonButtons;
        private FormStates _currentState;
        BillDto Bill;
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

        public frmBillForm(IMediator mediator)
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
            var row = grdVwMaterial.GetFocusedRow() as BillDetailDto;

            if (row == null)
                return;

            Bill.BillDetailList.Remove(row);
            grdCtrMaterial.RefreshDataSource();
            RefreshTotal();
        }

        private void RpsSearchLkUpMaterial_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            var materials = grdCtrMaterial.DataSource as BindingList<BillDetailDto>;

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
            var row = grdVwMaterial.GetFocusedRow() as BillDetailDto;

            if (row == null)
                return;

            if (row.Quantity <= 0)
            {
                e.Valid = false;
                e.ErrorText = Messages.EnterQuantity;
            }
            else if (row.PurchasePrice <= 0)
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
            if (Bill == null)
                return;

            txttotal.EditValue = Bill.BillDetailList.Sum(x => x.PurchasePrice * x.Quantity);
            txtTotalAfterDiscount.EditValue = Convert.ToDecimal(txttotal.EditValue) - Convert.ToDecimal(txtDiscount.EditValue);
        }

        private void SetControlStatus(bool status)
        {
            lkUpSupplier.ReadOnly = !status;
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
            lkUpSupplier.EditValue = default(int);
            txtDiscount.EditValue = default(decimal);
            dtDate.EditValue = DateTime.Now;
            memoDiscription.EditValue = string.Empty;
            grdCtrMaterial.DataSource = null;
        }

        private void FillControls()
        {
            txtSerial.EditValue = Bill.Serial;
            txttotal.EditValue = Bill.Total;
            txtTotalAfterDiscount.EditValue = Bill.TotalAfterDiscount;
            lkUpSupplier.EditValue = Bill.SupplierId;
            txtDiscount.EditValue = Bill.Discount;
            dtDate.EditValue = Bill.Date;
            memoDiscription.EditValue = Bill.Discription;
            grdCtrMaterial.DataSource = new BindingList<BillDetailDto>(Bill.BillDetailList);
        }

        private void FillData()
        {
            grdVwMaterial.PostEditor();
            Bill.SupplierId = Convert.ToInt32(lkUpSupplier.EditValue);
            Bill.Discount = Convert.ToDecimal(txtDiscount.EditValue);
            Bill.Date = dtDate.DateTime;
            Bill.Discription = memoDiscription.Text;
            Bill.BillDetailList = (grdCtrMaterial.DataSource as BindingList<BillDetailDto>).ToList();
        }

        private bool ValidateIncoice()
        {
            StringBuilder msg = new StringBuilder();

            if (Bill.SupplierId <= 0)
                msg.AppendLine(Messages.SelectSupplier);

            if (Bill.Discount < 0)
                msg.AppendLine(Messages.DiscountCantBeNegative);

            if (Bill.Date == DateTime.MinValue)
                msg.AppendLine(Messages.InvalidDate);

            if (Bill.BillDetailList.Count <= 0)
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
            Bill = new BillDto();
            ClearControls();
            SetControlStatus(true);
            grdCtrMaterial.DataSource = new BindingList<BillDetailDto>(Bill.BillDetailList);
            currentState = FormStates.AddingNew;

            var result = await _mediator.Send(new GetMaxSerialQuery());

            txtSerial.EditValue = (result + 1).ToString();
        }

        private void brBtnReset_ItemClick(object sender, ItemClickEventArgs e)
        {
            Bill = new BillDto();
            ClearControls();
            SetControlStatus(false);
            grdCtrMaterial.DataSource = new BindingList<BillDetailDto>(Bill.BillDetailList);
            currentState = FormStates.NormalFirstoaded;
        }

        private async Task Delete(int billId)
        {
            var result = Result.Success();

            result = await _mediator.Send(new DeleteBillCommand() { BillId = billId });

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
                progrssForm.SetParamitraizedAction(Delete, Bill.BillId);
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

        private async Task GetSuppliers()
        {
            var getResult = await _mediator.Send(new GetSupplierQuery());

            lkUpSupplier.Properties.DataSource = getResult;
        }

        private async Task GetMaterials()
        {
            var getResult = await _mediator.Send(new GetMaterialQuery());

            rpsSearchLkUpMaterial.DataSource = getResult;
        }

        private void frmBillForm_Activated(object sender, EventArgs e)
        {
            new frmProgressForm(GetSuppliers).ShowDialog();
            new frmProgressForm(GetMaterials).ShowDialog();
        }

        private async Task Save(BillDto bill)
        {
            var result = Result.Success();

            if (bill.BillId <= 0)
                result = await _mediator.Send(new CreateBillCommand() { bill = bill });
            else
                result = await _mediator.Send(new UpdateBillCommand() { bill = bill });

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
                progrssForm.SetParamitraizedAction(Save, Bill);
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
            using (frmBillSearchForm frm = new frmBillSearchForm(_mediator))
            {
                frm.ShowDialog(this);

                if (frm.DialogResult == DialogResult.OK)
                {
                    Bill = frm.Bill;
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