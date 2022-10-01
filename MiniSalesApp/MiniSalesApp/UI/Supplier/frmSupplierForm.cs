using DevExpress.XtraBars;
using DevExpress.XtraEditors.Controls;
using MediatR;
using MiniSalesApp.Application.Suppliers.Commands.CreateSupplier;
using MiniSalesApp.Application.Suppliers.Commands.DeleteSupplier;
using MiniSalesApp.Application.Suppliers.Commands.UpdateSupplier;
using MiniSalesApp.Application.Suppliers.Dtos;
using MiniSalesApp.Application.Suppliers.Queries.GetSupplier;
using MiniSalesApp.Application.Suppliers.Queries.GetMaxSerial;
using MiniSalesApp.Application.Suppliers.Queries.SearchSupplier;
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
using MiniSalesApp.UI.Custom;
using CSharpFunctionalExtensions;

namespace MiniSalesApp.UI.Supplier
{
    public partial class frmSupplierForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private List<ModuleRibbonButton> ribbonButtons;
        private FormStates _currentState;
        SupplierDto Supplier;
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
        public frmSupplierForm(IMediator mediator)
        {
            InitializeComponent();
            _mediator = mediator;
            ribbonButtons = new List<ModuleRibbonButton>();
            ribbonButtons.Add(new ModuleRibbonButton(brBtnNew, BtnType.New, "", "", new FormStates[] { FormStates.NormalFirstoaded }));
            ribbonButtons.Add(new ModuleRibbonButton(brBtnReset, BtnType.Reset, "", "", new FormStates[] { FormStates.AddingNew, FormStates.Editing }));
            ribbonButtons.Add(new ModuleRibbonButton(brBtnSave, BtnType.Save, "", "", new FormStates[] { FormStates.AddingNew, FormStates.Editing }));
            ribbonButtons.Add(new ModuleRibbonButton(brBtnDelete, BtnType.Delete, "", "", new FormStates[] { FormStates.Editing }));
            currentState = FormStates.NormalFirstoaded;
            SetControlStatus(false);
            ClearControls();
            ClearSearch();
            txtSerial.ReadOnly = true;
        }

        private void SetControlStatus(bool status)
        {
            txtName.ReadOnly = !status;
            txtPhone.ReadOnly = !status;
            txtAddress.ReadOnly = !status;
            txtBalance.ReadOnly = !status;
        }

        private void ClearControls()
        {
            txtSerial.EditValue = default(int);
            txtName.EditValue = string.Empty;
            txtPhone.EditValue = string.Empty;
            txtAddress.EditValue = string.Empty;
            txtBalance.EditValue = default(decimal);
        }

        private void FillControls()
        {
            txtSerial.EditValue = Supplier.Serial;
            txtName.EditValue = Supplier.Name;
            txtPhone.EditValue = Supplier.Phone;
            txtAddress.EditValue = Supplier.Address;
            txtBalance.EditValue = Supplier.Balance;
        }

        private void FillData()
        {
            Supplier.Serial = Convert.ToInt32(txtSerial.EditValue);
            Supplier.Name = txtName.EditValue.ToString();
            Supplier.Phone = txtPhone.EditValue.ToString();
            Supplier.Address = txtAddress.EditValue.ToString();
            Supplier.Balance = Convert.ToDecimal(txtBalance.EditValue);
        }

        private async  void brBtnNew_ItemClick(object sender, ItemClickEventArgs e)
        {
            Supplier = new SupplierDto();
            ClearControls();
            SetControlStatus(true);
            currentState = FormStates.AddingNew;

            var result = await _mediator.Send(new GetMaxSerialQuery());

            txtSerial.EditValue = (result + 1).ToString();
        }

        private void brBtnReset_ItemClick(object sender, ItemClickEventArgs e)
        {
            Supplier = new SupplierDto();
            ClearControls();
            SetControlStatus(false);
            currentState = FormStates.NormalFirstoaded;
        }

        private async Task Delete(int supplierId)
        {
            var result = Result.Success();

            result = await _mediator.Send(new DeleteSupplierCommand() { SupplierId = supplierId });

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
                progrssForm.SetParamitraizedAction(Delete, Supplier.SupplierId);
                progrssForm.ShowDialog();

                new frmProgressForm(GetSuppliers).ShowDialog();
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

        private bool ValidateSupplier()
        {
            StringBuilder msg = new StringBuilder();

            if (Supplier.Serial <= 0)
                msg.AppendLine(Messages.SerialIsRequired);

            if (string.IsNullOrEmpty(Supplier.Name))
                msg.AppendLine(Messages.NameIsRequired);

            if (Supplier.Balance <= 0)
                msg.AppendLine(Messages.BalanceIsRequired);

            if (msg.Length > 0)
            {
                Program.DisplayMessage(msg.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private async Task Save(SupplierDto supplier)
        {
            var result = Result.Success();

            if (supplier.SupplierId <= 0)
                result = await _mediator.Send(new CreateSupplierCommand() { Supplier = supplier });
            else
                result = await _mediator.Send(new UpdateSupplierCommand() { Supplier = supplier });

            if (result.IsFailure)
            {
                Program.DisplayMessage(result.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void brBtnSave_ItemClick(object sender, ItemClickEventArgs e)
        {
            FillData();
            if (!ValidateSupplier())
                return;

            try
            {
                var progrssForm = new frmProgressForm();
                progrssForm.SetParamitraizedAction(Save, Supplier);
                progrssForm.ShowDialog();

                new frmProgressForm(GetSuppliers).ShowDialog();
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

        private void rpsBtnEdit_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            SupplierDto row = grdVwSupplier.GetFocusedRow() as SupplierDto;
            if (row == null)
                return;

            ClearControls();
            SetControlStatus(true);
            Supplier = row;
            FillControls();
            currentState = FormStates.Editing;
        }

        private void frmSupplierForm_Load(object sender, EventArgs e)
        {
            new frmProgressForm(GetSuppliers).ShowDialog();
        }

        private async Task GetSuppliers()
        {
            var getResult = await _mediator.Send(new GetSupplierQuery());

            grdCtrSupplier.DataSource = getResult;
        }

        private void txtCode_EditValueChanging(object sender, ChangingEventArgs e)
        {
            if (e.NewValue != null && !string.IsNullOrEmpty(e.NewValue.ToString()) && e.NewValue.ToString().Contains("-"))
                e.Cancel = true;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearSearch();
        }

        private void ClearSearch()
        {
            txtSerialSearch.EditValue = default(int?);
            txtNameSearch.EditValue = string.Empty;
            txtPhoneSearch.EditValue = string.Empty;
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            var searchResult = await _mediator.Send(new SearchSupplierQuery()
            {
                Serial = (int?)txtSerialSearch.EditValue,
                Name = txtNameSearch.EditValue.ToString(),
                Phone = txtPhoneSearch.EditValue.ToString()
            });

            grdCtrSupplier.DataSource = searchResult;
        }

        private void txtBalance_EditValueChanging(object sender, ChangingEventArgs e)
        {
            if (e.NewValue != null && !string.IsNullOrEmpty(e.NewValue.ToString()) && e.NewValue.ToString().Contains("-"))
                e.Cancel = true;
        }
    }
}