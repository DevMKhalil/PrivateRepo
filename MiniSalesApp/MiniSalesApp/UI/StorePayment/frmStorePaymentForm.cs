using DevExpress.XtraBars;
using DevExpress.XtraEditors.Controls;
using MediatR;
using MiniSalesApp.Application.StorePayment.Commands.CreateStorePayment;
using MiniSalesApp.Application.StorePayment.Commands.DeleteStorePayment;
using MiniSalesApp.Application.StorePayment.Commands.UpdateStorePayment;
using MiniSalesApp.Application.StorePayment.Dtos;
using MiniSalesApp.Application.StorePayment.Queries.GetStorePayment;
using MiniSalesApp.Application.StorePayment.Queries.SearchStorePayment;
using MiniSalesApp.Application.Suppliers.Queries.GetSupplier;
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

namespace MiniSalesApp.UI.StorePayment
{
    public partial class frmStorePaymentForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private List<ModuleRibbonButton> ribbonButtons;
        private FormStates _currentState;
        Application.StorePayment.Dtos.StorePaymentDto storePayment;
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

        public frmStorePaymentForm(IMediator mediator)
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

        private async Task GetSuppliers()
        {
            var getResult = await _mediator.Send(new GetSupplierQuery());

            lkUpSupplier.Properties.DataSource = getResult;
            lkUpSupplierSearch.Properties.DataSource = getResult;
        }

        private void SetControlStatus(bool status)
        {
            dtDate.ReadOnly = !status;
            chkIsSupplierPayment.ReadOnly = !status;
            lkUpSupplier.ReadOnly = !status;
            txtAmount.ReadOnly = !status;
            memoDiscription.ReadOnly = !status;
        }

        private void ClearControls()
        {
            txtSerial.EditValue = string.Empty;
            dtDate.DateTime = DateTime.Now;
            chkIsSupplierPayment.Checked = false;
            lkUpSupplier.EditValue = null;
            txtAmount.EditValue = default(decimal);
            memoDiscription.EditValue = string.Empty;
            chkIsSupplierPayment_CheckedChanged(null, null);
        }

        private void FillControls()
        {
            txtSerial.EditValue = storePayment.Serial;
            dtDate.DateTime = storePayment.Date;
            chkIsSupplierPayment.Checked = storePayment.IsSupplierPayment;
            lkUpSupplier.EditValue = storePayment.SupplierId;
            txtAmount.EditValue = storePayment.Amount;
            memoDiscription.EditValue = storePayment.Description;
        }

        private void FillData()
        {
            storePayment.Date = dtDate.DateTime;
            storePayment.IsSupplierPayment = chkIsSupplierPayment.Checked;
            storePayment.SupplierId = (int?)lkUpSupplier.EditValue;
            storePayment.Amount = Convert.ToDecimal(txtAmount.EditValue);
            storePayment.Description = memoDiscription.EditValue.ToString();
        }

        private void brBtnNew_ItemClick(object sender, ItemClickEventArgs e)
        {
            storePayment = new StorePaymentDto();
            SetControlStatus(true);
            ClearControls();
            currentState = FormStates.AddingNew;
        }

        private void brBtnReset_ItemClick(object sender, ItemClickEventArgs e)
        {
            storePayment = new StorePaymentDto();
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
                var deleteResult = await _mediator.Send(new DeleteStorePaymentCommand()
                {
                    StorePaymentId = storePayment.StorePaymentId
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

            if (storePayment.Amount <= 0)
                msg.AppendLine(Messages.AmountCantBeNegative);

            if (storePayment.IsSupplierPayment && storePayment.SupplierId == null)
                msg.AppendLine(Messages.SelectSupplier);

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
                if (storePayment.StorePaymentId <= 0)
                {
                    var saveResult = await _mediator.Send(new CreateStorePaymentCommand()
                    {
                        StorePayment = new StorePaymentDto()
                        {
                            IsSupplierPayment = storePayment.IsSupplierPayment,
                            Date = storePayment.Date,
                            SupplierId = storePayment.SupplierId,
                            Amount = storePayment.Amount,
                            Description = storePayment.Description
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
                    var saveResult = await _mediator.Send(new UpdateStorePaymentCommand()
                    {
                        StorePayment = new StorePaymentDto()
                        {
                            Serial = storePayment.Serial,
                            IsSupplierPayment = storePayment.IsSupplierPayment,
                            Date = storePayment.Date,
                            SupplierId = storePayment.SupplierId,
                            Amount = storePayment.Amount,
                            Description = storePayment.Description,
                            StorePaymentId = storePayment.StorePaymentId,
                            StoreDailyId = storePayment.StoreDailyId
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
            Application.StorePayment.Dtos.StorePaymentDto row = grdVwStorePayment.GetFocusedRow() as Application.StorePayment.Dtos.StorePaymentDto;
            if (row == null)
                return;

            SetControlStatus(true);
            ClearControls();
            storePayment = row;
            FillControls();
            currentState = FormStates.Editing;
        }

        private async void frmStorePaymentFormNew_Load(object sender, EventArgs e)
        {
            await GetstoreDailys();
        }

        private async Task GetstoreDailys()
        {
            var getResult = await _mediator.Send(new GetStorePaymentQuery());

            grdCtrStorePayment.DataSource = getResult;
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
            chkIsSupplierPaymentSearch.Checked = false;
            lkUpSupplierSearch.EditValue = null;
            chkIsSupplierPaymentSearch_CheckedChanged(null, null);
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            var searchResult = await _mediator.Send(new SearchStorePaymentQuery()
            {
                Serial = (txtSerialSearch.EditValue == null || string.IsNullOrEmpty(txtSerialSearch.EditValue.ToString())) ? null : Convert.ToInt32(txtSerialSearch.EditValue),
                FromDate = dtFromDate.DateTime,
                ToDate = dtToDate.DateTime,
                SupplierId = (int?)lkUpSupplierSearch.EditValue,
                IsSupplierPayment = chkIsSupplierPaymentSearch.Checked
            });

            grdCtrStorePayment.DataSource = searchResult;
        }

        private void txtSerialSearch_EditValueChanging(object sender, ChangingEventArgs e)
        {
            if (e.NewValue != null && !string.IsNullOrEmpty(e.NewValue.ToString()) && e.NewValue.ToString().Contains("-"))
                e.Cancel = true;
        }

        private void chkIsSupplierPayment_CheckedChanged(object sender, EventArgs e)
        {
            lkUpSupplier.ReadOnly = !chkIsSupplierPayment.Checked;
            lkUpSupplier.EditValue = null;

        }

        private void lkUpSupplierSearch_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == ButtonPredefines.Delete)
                lkUpSupplierSearch.EditValue = null;
        }

        private void chkIsSupplierPaymentSearch_CheckedChanged(object sender, EventArgs e)
        {
            lkUpSupplierSearch.ReadOnly = !chkIsSupplierPaymentSearch.Checked;
            lkUpSupplierSearch.EditValue = null;
        }

        private async void frmStorePaymentFormNew_Activated(object sender, EventArgs e)
        {
            await GetSuppliers();
        }
    }
}