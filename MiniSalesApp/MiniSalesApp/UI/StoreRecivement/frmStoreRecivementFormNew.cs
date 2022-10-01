using DevExpress.XtraBars;
using DevExpress.XtraEditors.Controls;
using MediatR;
using MiniSalesApp.Application.Customers.Queries.GetCustomer;
using MiniSalesApp.Application.StoreRecivement.Commands.CreateStoreRecivement;
using MiniSalesApp.Application.StoreRecivement.Commands.DeleteStoreRecivement;
using MiniSalesApp.Application.StoreRecivement.Commands.UpdateStoreRecivement;
using MiniSalesApp.Application.StoreRecivement.Dtos;
using MiniSalesApp.Application.StoreRecivement.Queries.GetStoreRecivement;
using MiniSalesApp.Application.StoreRecivement.Queries.SearchStoreRecivement;
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

namespace MiniSalesApp.UI.StoreRecivement
{
    public partial class frmStoreRecivementFormNew : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private List<ModuleRibbonButton> ribbonButtons;
        private FormStates _currentState;
        Application.StoreRecivement.Dtos.StoreRecivementDto storeRecivement;
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

        public frmStoreRecivementFormNew(IMediator mediator)
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

        private void SetControlStatus(bool status)
        {
            dtDate.ReadOnly = !status;
            chkIsCustomerRecivement.ReadOnly = !status;
            lkUpCustomer.ReadOnly = !status;
            txtAmount.ReadOnly = !status;
            memoDiscription.ReadOnly = !status;
        }

        private void ClearControls()
        {
            txtSerial.EditValue = string.Empty;
            dtDate.DateTime = DateTime.Now;
            chkIsCustomerRecivement.Checked = false;
            lkUpCustomer.EditValue = null;
            txtAmount.EditValue = default(decimal);
            memoDiscription.EditValue = string.Empty;
            chkIsCustomerRecivement_CheckedChanged(null, null);
        }

        private void FillControls()
        {
            txtSerial.EditValue = storeRecivement.Serial;
            dtDate.DateTime = storeRecivement.Date;
            chkIsCustomerRecivement.Checked = storeRecivement.IsCustomerRecivement;
            lkUpCustomer.EditValue = storeRecivement.CustomerId;
            txtAmount.EditValue = storeRecivement.Amount;
            memoDiscription.EditValue = storeRecivement.Description;
        }

        private void FillData()
        {
            storeRecivement.Date = dtDate.DateTime;
            storeRecivement.IsCustomerRecivement = chkIsCustomerRecivement.Checked;
            storeRecivement.CustomerId = (int?)lkUpCustomer.EditValue;
            storeRecivement.Amount = Convert.ToDecimal(txtAmount.EditValue);
            storeRecivement.Description = memoDiscription.EditValue.ToString();
        }

        private void brBtnNew_ItemClick(object sender, ItemClickEventArgs e)
        {
            storeRecivement = new StoreRecivementDto();
            SetControlStatus(true);
            ClearControls();
            currentState = FormStates.AddingNew;
        }

        private void brBtnReset_ItemClick(object sender, ItemClickEventArgs e)
        {
            storeRecivement = new StoreRecivementDto();
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
                var deleteResult = await _mediator.Send(new DeleteStoreRecivementCommand()
                {
                    StoreRecivementId = storeRecivement.StoreRecivementId
                });

                if (deleteResult.IsFailure)
                {
                    Program.DisplayMessage(deleteResult.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                await GetstoreDailys();
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

        private bool ValidatestoreDaily()
        {
            StringBuilder msg = new StringBuilder();

            if (storeRecivement.Amount <= 0)
                msg.AppendLine(Messages.AmountCantBeNegative);

            if (storeRecivement.IsCustomerRecivement && storeRecivement.CustomerId == null)
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
            if (!ValidatestoreDaily())
                return;

            try
            {
                if (storeRecivement.StoreRecivementId <= 0)
                {
                    var saveResult = await _mediator.Send(new CreateStoreRecivementCommand()
                    {
                        StoreRecivement = new StoreRecivementDto()
                        {
                            IsCustomerRecivement = storeRecivement.IsCustomerRecivement,
                            Date = storeRecivement.Date,
                            CustomerId = storeRecivement.CustomerId,
                            Amount = storeRecivement.Amount,
                            Description = storeRecivement.Description
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
                    var saveResult = await _mediator.Send(new UpdateStoreRecivementCommand()
                    {
                        StoreRecivement = new StoreRecivementDto()
                        {
                            Serial = storeRecivement.Serial,
                            IsCustomerRecivement = storeRecivement.IsCustomerRecivement,
                            Date = storeRecivement.Date,
                            CustomerId = storeRecivement.CustomerId,
                            Amount = storeRecivement.Amount,
                            Description = storeRecivement.Description,
                            StoreRecivementId = storeRecivement.StoreRecivementId,
                            StoreDailyId = storeRecivement.StoreDailyId
                        }
                    });

                    if (saveResult.IsFailure)
                    {
                        Program.DisplayMessage(saveResult.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                await GetstoreDailys();
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
            Application.StoreRecivement.Dtos.StoreRecivementDto row = grdVwStoreRecivement.GetFocusedRow() as Application.StoreRecivement.Dtos.StoreRecivementDto;
            if (row == null)
                return;

            SetControlStatus(true);
            ClearControls();
            storeRecivement = row;
            FillControls();
            currentState = FormStates.Editing;
        }

        private async void frmStoreRecivementFormNew_Load(object sender, EventArgs e)
        {
            await GetstoreDailys();
        }

        private async Task GetstoreDailys()
        {
            var getResult = await _mediator.Send(new GetStoreRecivementQuery());

            grdCtrStoreRecivement.DataSource = getResult;
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
            chkIsCustomerRecivementSearch_CheckedChanged(null, null);
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            var searchResult = await _mediator.Send(new SearchStoreRecivementQuery()
            {
                Serial = (txtSerialSearch.EditValue == null || string.IsNullOrEmpty(txtSerialSearch.EditValue.ToString())) ? null : Convert.ToInt32(txtSerialSearch.EditValue),
                FromDate = dtFromDate.DateTime,
                ToDate = dtToDate.DateTime,
                CustomerId = (int?)lkUpCustomerSearch.EditValue,
                IsCustomerRecivement = chkIsCustomerRecivementSearch.Checked
            });

            grdCtrStoreRecivement.DataSource = searchResult;
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

        private async void frmStoreRecivementFormNew_Activated(object sender, EventArgs e)
        {
            await GetCustomers();
        }
    }
}