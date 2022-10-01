using CSharpFunctionalExtensions;
using DevExpress.XtraBars;
using DevExpress.XtraEditors.Controls;
using MediatR;
using MiniSalesApp.Application.Customers.Commands.CreateCustomer;
using MiniSalesApp.Application.Customers.Commands.DeleteCustomer;
using MiniSalesApp.Application.Customers.Commands.UpdateCustomer;
using MiniSalesApp.Application.Customers.Dtos;
using MiniSalesApp.Application.Customers.Queries.GetCustomer;
using MiniSalesApp.Application.Customers.Queries.GetMaxSerial;
using MiniSalesApp.Application.Customers.Queries.SearchCustomer;
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

namespace MiniSalesApp.UI.Customer
{
    public partial class frmCustomerForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private List<ModuleRibbonButton> ribbonButtons;
        private FormStates _currentState;
        CustomerDto Customer;
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
        public frmCustomerForm(IMediator mediator)
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
            txtSerial.EditValue = Customer.Serial;
            txtName.EditValue = Customer.Name;
            txtPhone.EditValue = Customer.Phone;
            txtAddress.EditValue = Customer.Address;
            txtBalance.EditValue = Customer.Balance;
        }

        private void FillData()
        {
            Customer.Serial = Convert.ToInt32(txtSerial.EditValue);
            Customer.Name = txtName.EditValue.ToString();
            Customer.Phone = txtPhone.EditValue.ToString();
            Customer.Address = txtAddress.EditValue.ToString();
            Customer.Balance = Convert.ToDecimal(txtBalance.EditValue);
        }

        private async  void brBtnNew_ItemClick(object sender, ItemClickEventArgs e)
        {
            Customer = new CustomerDto();
            ClearControls();
            SetControlStatus(true);
            currentState = FormStates.AddingNew;

            var result = await _mediator.Send(new GetMaxSerialQuery());

            txtSerial.EditValue = (result + 1).ToString();
        }

        private void brBtnReset_ItemClick(object sender, ItemClickEventArgs e)
        {
            Customer = new CustomerDto();
            ClearControls();
            SetControlStatus(false);
            currentState = FormStates.NormalFirstoaded;
        }

        private async Task DeleteCustomer(int customerId)
        {
            var result = Result.Success();

            result = await _mediator.Send(new DeleteCustomerCommand() { CustomerId = customerId });

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
                progrssForm.SetParamitraizedAction(DeleteCustomer, Customer.CustomerId);
                progrssForm.ShowDialog();

                new frmProgressForm(GetCustomers).ShowDialog();

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

        private bool ValidateCustomer()
        {
            StringBuilder msg = new StringBuilder();

            if (Customer.Serial <= 0)
                msg.AppendLine(Messages.SerialIsRequired);

            if (string.IsNullOrEmpty(Customer.Name))
                msg.AppendLine(Messages.NameIsRequired);

            if (Customer.Balance <= 0)
                msg.AppendLine(Messages.BalanceIsRequired);

            if (msg.Length > 0)
            {
                Program.DisplayMessage(msg.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private async Task SaveCustomer(CustomerDto customer)
        {
            var result = Result.Success();

            if (customer.CustomerId <= 0)
                result = await _mediator.Send(new CreateCustomerCommand() { Customer = customer });
            else
                result = await _mediator.Send(new UpdateCustomerCommand() { Customer = customer });

            if (result.IsFailure)
            {
                Program.DisplayMessage(result.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private async void brBtnSave_ItemClick(object sender, ItemClickEventArgs e)
        {
            FillData();
            if (!ValidateCustomer())
                return;

            try
            {
                var progrssForm = new frmProgressForm();
                progrssForm.SetParamitraizedAction(SaveCustomer, Customer);
                progrssForm.ShowDialog();

                new frmProgressForm(GetCustomers).ShowDialog();
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
            CustomerDto row = grdVwCustomer.GetFocusedRow() as CustomerDto;
            if (row == null)
                return;

            ClearControls();
            SetControlStatus(true);
            Customer = row;
            FillControls();
            currentState = FormStates.Editing;
        }

        private void frmCustomerForm_Load(object sender, EventArgs e)
        {
            new frmProgressForm(GetCustomers).ShowDialog();
        }

        private async Task GetCustomers()
        {
            var getResult = await _mediator.Send(new GetCustomerQuery());

            grdCtrCustomer.DataSource = getResult;
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
            var searchResult = await _mediator.Send(new SearchCustomerQuery()
            {
                Serial = (int?)txtSerialSearch.EditValue,
                Name = txtNameSearch.EditValue.ToString(),
                Phone = txtPhoneSearch.EditValue.ToString()
            });

            grdCtrCustomer.DataSource = searchResult;
        }

        private void txtBalance_EditValueChanging(object sender, ChangingEventArgs e)
        {
            if (e.NewValue != null && !string.IsNullOrEmpty(e.NewValue.ToString()) && e.NewValue.ToString().Contains("-"))
                e.Cancel = true;
        }
    }
}