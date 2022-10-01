using CSharpFunctionalExtensions;
using DevExpress.XtraBars;
using DevExpress.XtraEditors.Controls;
using MediatR;
using MiniSalesApp.Application.StoreDaily.Commands.CreateStoreDaily;
using MiniSalesApp.Application.StoreDaily.Commands.DeleteStoreDaily;
using MiniSalesApp.Application.StoreDaily.Commands.UpdateStoreDaily;
using MiniSalesApp.Application.StoreDaily.Dtos;
using MiniSalesApp.Application.StoreDaily.Queries.GetStoreDaily;
using MiniSalesApp.Application.StoreDaily.Queries.SearchStoreDaily;
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

namespace MiniSalesApp.UI.StoreDaily
{
    public partial class frmStoreDailyFormNew : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private List<ModuleRibbonButton> ribbonButtons;
        private FormStates _currentState;
        StoreDailyDto storeDaily;
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
        public frmStoreDailyFormNew(IMediator mediator)
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
            txtStartAmount.ReadOnly = (grdCtrStoreDaily.DataSource as List<StoreDailyDto>).Count > 0 ? true : !status;
        }

        private void ClearControls()
        {
            txtSerial.EditValue = string.Empty;
            dtStartDate.DateTime = DateTime.Now;
            txtStartAmount.EditValue = default(decimal);
            txtEndAmount.EditValue = default(decimal);
        }

        private void FillControls()
        {
            txtSerial.EditValue = storeDaily.Serial;
            dtStartDate.DateTime = storeDaily.StartDate;
            txtStartAmount.EditValue = storeDaily.StartAmount;
            txtEndAmount.EditValue = storeDaily.EndAmount;
        }

        private void FillData()
        {
            storeDaily.StartDate = dtStartDate.DateTime;
            storeDaily.StartAmount = Convert.ToDecimal(txtStartAmount.EditValue);
        }

        private void brBtnNew_ItemClick(object sender, ItemClickEventArgs e)
        {
            storeDaily = new StoreDailyDto();
            ClearControls();
            SetControlStatus(true);
            currentState = FormStates.AddingNew;
        }

        private void brBtnReset_ItemClick(object sender, ItemClickEventArgs e)
        {
            storeDaily = new StoreDailyDto();
            ClearControls();
            SetControlStatus(false);
            currentState = FormStates.NormalFirstoaded;
        }

        private async Task Delete(int storeDailyId)
        {
            var result = Result.Success();

            result = await _mediator.Send(new DeleteStoreDailyCommand() { StoreDailyId = storeDailyId });

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
                progrssForm.SetParamitraizedAction(Delete, storeDaily.StoreDailyId);
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

            if (storeDaily.StartAmount < 0)
                msg.AppendLine(Messages.StartAmountCantBeNegative);

            if (msg.Length > 0)
            {
                Program.DisplayMessage(msg.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private async Task Save(StoreDailyDto bank)
        {
            var result = Result.Success();

            if (bank.StoreDailyId <= 0)
                result = await _mediator.Send(new CreateStoreDailyCommand() { StoreDaily = bank });
            else
                result = await _mediator.Send(new UpdateStoreDailyCommand() { StoreDaily = bank });

            if (result.IsFailure)
            {
                Program.DisplayMessage(result.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void brBtnSave_ItemClick(object sender, ItemClickEventArgs e)
        {
            FillData();
            if (!ValidatestoreDaily())
                return;

            try
            {
                var progrssForm = new frmProgressForm();
                progrssForm.SetParamitraizedAction(Save, storeDaily);
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
            StoreDailyDto row = grdVwStoreDaily.GetFocusedRow() as StoreDailyDto;
            if (row == null)
                return;

            ClearControls();
            SetControlStatus(true);
            storeDaily = row;
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
            var getResult = await _mediator.Send(new GetStoreDailyQuery());

            grdCtrStoreDaily.DataSource = getResult;
        }

        private void txtStartAmount_EditValueChanging(object sender, ChangingEventArgs e)
        {
            if (e.NewValue != null && !string.IsNullOrEmpty(e.NewValue.ToString()) && e.NewValue.ToString().Contains("-"))
                e.Cancel = true;
        }

        private void txtEndAmount_EditValueChanging(object sender, ChangingEventArgs e)
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
            dtFromDate.DateTime = DateTime.Now;
            dtToDate.DateTime = DateTime.Now;
            new frmProgressForm(GetstoreDailys).ShowDialog();
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            var searchResult = await _mediator.Send(new SearchStoreDailyQuery()
            {
                Serial = (txtSerialSearch.EditValue == null || string.IsNullOrEmpty(txtSerialSearch.EditValue.ToString())) ? null : Convert.ToInt32(txtSerialSearch.EditValue),
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

        private void frmStoreDailyFormNew_Activated(object sender, EventArgs e)
        {
            new frmProgressForm(GetstoreDailys).ShowDialog();
        }
    }
}