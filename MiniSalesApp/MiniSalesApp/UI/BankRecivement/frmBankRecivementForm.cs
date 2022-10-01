using DevExpress.XtraBars;
using DevExpress.XtraEditors.Controls;
using MediatR;
using MiniSalesApp.Application.BankRecivement.Commands.CreateBankRecivement;
using MiniSalesApp.Application.BankRecivement.Commands.DeleteBankRecivement;
using MiniSalesApp.Application.BankRecivement.Commands.UpdateBankRecivement;
using MiniSalesApp.Application.BankRecivement.Dtos;
using MiniSalesApp.Application.BankRecivement.Queries.GetBankRecivement;
using MiniSalesApp.Application.BankRecivement.Queries.SearchBankRecivement;
using MiniSalesApp.Application.Banks.Queries.GetBank;
using MiniSalesApp.Application.Customers.Queries.GetCustomer;
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

namespace MiniSalesApp.UI.BankRecivement
{
    public partial class frmBankRecivementForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private List<ModuleRibbonButton> ribbonButtons;
        private FormStates _currentState;
        Application.BankRecivement.Dtos.BankRecivementDto BankRecivement;
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

        public frmBankRecivementForm(IMediator mediator)
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
        }

        private async Task GetCustomers()
        {
            var getResult = await _mediator.Send(new GetCustomerQuery());

            lkUpCustomer.Properties.DataSource = getResult;
            lkUpCustomerSearch.Properties.DataSource = getResult;
        }

        private async Task GetBanks()
        {
            var getResult = await _mediator.Send(new GetBankQuery());

            lkUpBank.Properties.DataSource = getResult;
            lkUpBankSearch.Properties.DataSource = getResult;
        }

        private void SetControlStatus(bool status)
        {
            dtDate.ReadOnly = !status;
            chkIsCustomerRecivement.ReadOnly = !status;
            lkUpCustomer.ReadOnly = !status;
            lkUpBank.ReadOnly = !status;
            txtAmount.ReadOnly = !status;
            memoDiscription.ReadOnly = !status;
        }

        private void ClearControls()
        {
            txtSerial.EditValue = string.Empty;
            dtDate.DateTime = DateTime.Now;
            chkIsCustomerRecivement.Checked = false;
            lkUpCustomer.EditValue = null;
            lkUpBank.EditValue = null;
            txtAmount.EditValue = default(decimal);
            memoDiscription.EditValue = string.Empty;
            chkIsCustomerRecivement_CheckedChanged(null, null);
        }

        private void FillControls()
        {
            txtSerial.EditValue = BankRecivement.Serial;
            dtDate.DateTime = BankRecivement.Date;
            chkIsCustomerRecivement.Checked = BankRecivement.IsCustomerRecivement;
            lkUpCustomer.EditValue = BankRecivement.CustomerId;
            lkUpBank.EditValue = BankRecivement.BankId;
            txtAmount.EditValue = BankRecivement.Amount;
            memoDiscription.EditValue = BankRecivement.Description;
        }

        private void FillData()
        {
            BankRecivement.Date = dtDate.DateTime;
            BankRecivement.IsCustomerRecivement = chkIsCustomerRecivement.Checked;
            BankRecivement.CustomerId = (int?)lkUpCustomer.EditValue;
            BankRecivement.BankId = Convert.ToInt32(lkUpBank.EditValue);
            BankRecivement.Amount = Convert.ToDecimal(txtAmount.EditValue);
            BankRecivement.Description = memoDiscription.EditValue.ToString();
        }

        private void brBtnNew_ItemClick(object sender, ItemClickEventArgs e)
        {
            BankRecivement = new BankRecivementDto();
            SetControlStatus(true);
            ClearControls();
            currentState = FormStates.AddingNew;
        }

        private void brBtnReset_ItemClick(object sender, ItemClickEventArgs e)
        {
            BankRecivement = new BankRecivementDto();
            SetControlStatus(false);
            ClearControls();
            currentState = FormStates.NormalFirstoaded;
        }

        private async void brBtnDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Program.DisplayMessage(Messages.DeleteConfirmation, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            try
            {
                var deleteResult = await _mediator.Send(new DeleteBankRecivementCommand()
                {
                    BankRecivementId = BankRecivement.BankRecivementId
                });

                if (deleteResult.IsFailure)
                {
                    Program.DisplayMessage(deleteResult.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                await GetBankDailys();
                SetControlStatus(false);
                ClearControls();
                Program.DisplayMessage(Messages.DeleteSuccessfuly, MessageBoxButtons.OK, MessageBoxIcon.Information);
                currentState = FormStates.NormalFirstoaded;
            }
            catch (Exception ex)
            {
                Program.DisplayMessage(ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateBankDaily()
        {
            StringBuilder msg = new StringBuilder();

            if (BankRecivement.Amount <= 0)
                msg.AppendLine(Messages.AmountCantBeNegative);

            if (BankRecivement.IsCustomerRecivement && BankRecivement.CustomerId == null)
                msg.AppendLine(Messages.SellectCustomer);

            if (msg.Length > 0)
            {
                Program.DisplayMessage(msg.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private async void brBtnSave_ItemClick(object sender, ItemClickEventArgs e)
        {
            FillData();
            if (!ValidateBankDaily())
                return;

            try
            {
                if (BankRecivement.BankRecivementId <= 0)
                {
                    var saveResult = await _mediator.Send(new CreateBankRecivementCommand()
                    {
                        BankRecivement = new BankRecivementDto()
                        {
                            IsCustomerRecivement = BankRecivement.IsCustomerRecivement,
                            Date = BankRecivement.Date,
                            CustomerId = BankRecivement.CustomerId,
                            Amount = BankRecivement.Amount,
                            Description = BankRecivement.Description,
                            BankId = BankRecivement.BankId
                        }
                    });

                    if (saveResult.IsFailure)
                    {
                        Program.DisplayMessage(saveResult.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                    var saveResult = await _mediator.Send(new UpdateBankRecivementCommand()
                    {
                        BankRecivement = new BankRecivementDto()
                        {
                            Serial = BankRecivement.Serial,
                            IsCustomerRecivement = BankRecivement.IsCustomerRecivement,
                            Date = BankRecivement.Date,
                            CustomerId = BankRecivement.CustomerId,
                            Amount = BankRecivement.Amount,
                            Description = BankRecivement.Description,
                            BankRecivementId = BankRecivement.BankRecivementId,
                            BankId = BankRecivement.BankId
                        }
                    });

                    if (saveResult.IsFailure)
                    {
                        Program.DisplayMessage(saveResult.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                await GetBankDailys();
                SetControlStatus(false);
                ClearControls();
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
            Application.BankRecivement.Dtos.BankRecivementDto row = grdVwBankRecivement.GetFocusedRow() as Application.BankRecivement.Dtos.BankRecivementDto;
            if (row == null)
                return;

            SetControlStatus(true);
            ClearControls();
            BankRecivement = row;
            FillControls();
            currentState = FormStates.Editing;
        }

        private async void frmBankRecivementFormNew_Load(object sender, EventArgs e)
        {
            await GetBankDailys();
        }

        private async Task GetBankDailys()
        {
            var getResult = await _mediator.Send(new GetBankRecivementQuery());

            grdCtrBankRecivement.DataSource = getResult;
        }

        private void txtAmount_EditValueChanging(object sender, ChangingEventArgs e)
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
            txtSerialSearch.EditValue = null;
            dtFromDate.DateTime = DateTime.Now;
            dtToDate.DateTime = DateTime.Now;
            chkIsCustomerRecivementSearch.Checked = false;
            lkUpCustomerSearch.EditValue = null;
            lkUpBankSearch.EditValue = null;
            chkIsCustomerRecivementSearch_CheckedChanged(null, null);
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            var searchResult = await _mediator.Send(new SearchBankRecivementQuery()
            {
                Serial = (txtSerialSearch.EditValue == null || string.IsNullOrEmpty(txtSerialSearch.EditValue.ToString())) ? null : Convert.ToInt32(txtSerialSearch.EditValue),
                FromDate = dtFromDate.DateTime,
                ToDate = dtToDate.DateTime,
                CustomerId = (int?)lkUpCustomerSearch.EditValue,
                IsCustomerRecivement = chkIsCustomerRecivementSearch.Checked,
                BankId = (int?)lkUpBankSearch.EditValue
            });

            grdCtrBankRecivement.DataSource = searchResult;
        }

        private void txtSerialSearch_EditValueChanging(object sender, ChangingEventArgs e)
        {
            if (e.NewValue != null && !string.IsNullOrEmpty(e.NewValue.ToString()) && e.NewValue.ToString().Contains("-"))
                e.Cancel = true;
        }

        private void chkIsCustomerRecivement_CheckedChanged(object sender, EventArgs e)
        {
            lkUpCustomer.ReadOnly = !chkIsCustomerRecivement.Checked;
            lkUpCustomer.EditValue = null;

        }

        private void lkUpCustomerSearch_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == ButtonPredefines.Delete)
                lkUpCustomerSearch.EditValue = null;
        }

        private void chkIsCustomerRecivementSearch_CheckedChanged(object sender, EventArgs e)
        {
            lkUpCustomerSearch.ReadOnly = !chkIsCustomerRecivementSearch.Checked;
            lkUpCustomerSearch.EditValue = null;
        }

        private async void frmBankRecivementFormNew_Activated(object sender, EventArgs e)
        {
            await GetCustomers();
            await GetBanks();
        }
    }
}