using CSharpFunctionalExtensions;
using DevExpress.XtraBars;
using DevExpress.XtraEditors.Controls;
using MediatR;
using MiniSalesApp.Application.Banks.Commands.CreateBank;
using MiniSalesApp.Application.Banks.Commands.DeleteBank;
using MiniSalesApp.Application.Banks.Commands.UpdateBank;
using MiniSalesApp.Application.Banks.Dtos;
using MiniSalesApp.Application.Banks.Queries.GetBank;
using MiniSalesApp.Application.Banks.Queries.SearchBank;
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

namespace MiniSalesApp.UI.Bank
{
    public partial class frmBankForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private List<ModuleRibbonButton> ribbonButtons;
        private FormStates _currentState;
        BankDto bank;
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
        public frmBankForm(IMediator mediator)
        {
            InitializeComponent();
            _mediator = mediator;
            ribbonButtons = new List<ModuleRibbonButton>();
            ribbonButtons.Add(new ModuleRibbonButton(brBtnNew, BtnType.New, "", "", new FormStates[] { FormStates.NormalFirstoaded }));
            ribbonButtons.Add(new ModuleRibbonButton(brBtnReset, BtnType.Reset, "", "", new FormStates[] { FormStates.AddingNew, FormStates.Editing }));
            ribbonButtons.Add(new ModuleRibbonButton(brBtnSave, BtnType.Save, "", "", new FormStates[] { FormStates.AddingNew, FormStates.Editing }));
            ribbonButtons.Add(new ModuleRibbonButton(brBtnDelete, BtnType.Delete, "", "", new FormStates[] { FormStates.Editing }));
            currentState = FormStates.NormalFirstoaded;
        }

        private void SetControlStatus(bool status)
        {
            dtStartDate.ReadOnly = !status;
            txtStartAmount.ReadOnly = !status;
            txtName.ReadOnly = !status;
        }

        private void ClearControls()
        {
            txtSerial.EditValue = string.Empty;
            dtStartDate.DateTime = DateTime.Now;
            txtStartAmount.EditValue = default(decimal);
            txtName.EditValue = string.Empty;
        }

        private void FillControls()
        {
            txtSerial.EditValue = bank.Serial;
            dtStartDate.DateTime = bank.Date;
            txtStartAmount.EditValue = bank.StartAmount;
            txtName.EditValue = bank.Name;
        }

        private void FillData()
        {
            bank.Date = dtStartDate.DateTime;
            bank.StartAmount = Convert.ToDecimal(txtStartAmount.EditValue);
            bank.Name = txtName.EditValue.ToString();
        }

        private void brBtnNew_ItemClick(object sender, ItemClickEventArgs e)
        {
            bank = new BankDto();
            ClearControls();
            SetControlStatus(true);
            currentState = FormStates.AddingNew;
        }

        private void brBtnReset_ItemClick(object sender, ItemClickEventArgs e)
        {
            bank = new BankDto();
            ClearControls();
            SetControlStatus(false);
            currentState = FormStates.NormalFirstoaded;
        }

        private async Task Delete(int bankId)
        {
            var result = Result.Success();

            result = await _mediator.Send(new DeleteBankCommand() { BankId = bankId });

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
                progrssForm.SetParamitraizedAction(Delete, bank.BankId);
                progrssForm.ShowDialog();

                new frmProgressForm(GetstoreDailys).ShowDialog();
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

        private bool ValidatestoreDaily()
        {
            StringBuilder msg = new StringBuilder();

            if (bank.StartAmount < 0)
                msg.AppendLine(Messages.StartAmountCantBeNegative);

            if (bank.Date == DateTime.MinValue)
                msg.AppendLine(Messages.InvalidDate);

            if (string.IsNullOrEmpty(bank.Name))
                msg.AppendLine(Messages.NameIsRequired);

            if (msg.Length > 0)
            {
                Program.DisplayMessage(msg.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private async Task Save(BankDto bank)
        {
            var result = Result.Success();

            if (bank.BankId <= 0)
                result = await _mediator.Send(new CreateBankCommand() { Bank = bank });
            else
                result = await _mediator.Send(new UpdateBankCommand() { Bank = bank });

            if (result.IsFailure)
            {
                Program.DisplayMessage(result.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private async void brBtnSave_ItemClick(object sender, ItemClickEventArgs e)
        {
            FillData();
            if (!ValidatestoreDaily())
                return;

            try
            {
                var progrssForm = new frmProgressForm();
                progrssForm.SetParamitraizedAction(Save, bank);
                progrssForm.ShowDialog();

                new frmProgressForm(GetstoreDailys).ShowDialog();
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
            BankDto row = grdVwStoreDaily.GetFocusedRow() as BankDto;
            if (row == null)
                return;

            ClearControls();
            SetControlStatus(true);
            bank = row;
            FillControls();
            currentState = FormStates.Editing;
        }

        private async void frmStoreDailyForm_Load(object sender, EventArgs e)
        {
            await ClearSearch();
            SetControlStatus(false);
            ClearControls();
        }

        private async Task GetstoreDailys()
        {
            var getResult = await _mediator.Send(new GetBankQuery());

            grdCtrStoreDaily.DataSource = getResult;
        }

        private void txtStartAmount_EditValueChanging(object sender, ChangingEventArgs e)
        {
            if (e.NewValue != null && !string.IsNullOrEmpty(e.NewValue.ToString()) && e.NewValue.ToString().Contains("-"))
                e.Cancel = true;
        }

        private async void btnClear_Click(object sender, EventArgs e)
        {
            await ClearSearch();
        }

        private async Task ClearSearch()
        {
            txtSerialSearch.EditValue = null;
            txtName.EditValue = string.Empty;
            dtFromDate.DateTime = DateTime.Now;
            dtToDate.DateTime = DateTime.Now;
            new frmProgressForm(GetstoreDailys).ShowDialog();
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            var searchResult = await _mediator.Send(new SearchBankQuery()
            {
                Serial = (txtSerialSearch.EditValue == null || string.IsNullOrEmpty(txtSerialSearch.EditValue.ToString())) ? null : Convert.ToInt32(txtSerialSearch.EditValue),
                Name = txtName.EditValue.ToString(),
                FromDate = dtFromDate.DateTime,
                ToDate = dtToDate.DateTime
            });

            grdCtrStoreDaily.DataSource = searchResult;
        }

        private void txtSerialSearch_EditValueChanging(object sender, ChangingEventArgs e)
        {
            if (e.NewValue != null && !string.IsNullOrEmpty(e.NewValue.ToString()) && e.NewValue.ToString().Contains("-"))
                e.Cancel = true;
        }

        private void frmBankForm_Activated(object sender, EventArgs e)
        {
            new frmProgressForm(GetstoreDailys).ShowDialog();
        }
    }
}