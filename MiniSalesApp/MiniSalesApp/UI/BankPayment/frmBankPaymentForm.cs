using DevExpress.XtraBars;
using DevExpress.XtraEditors.Controls;
using MediatR;
using MiniSalesApp.Application.BankPayment.Commands.CreateBankPayment;
using MiniSalesApp.Application.BankPayment.Commands.DeleteBankPayment;
using MiniSalesApp.Application.BankPayment.Commands.UpdateBankPayment;
using MiniSalesApp.Application.BankPayment.Dtos;
using MiniSalesApp.Application.BankPayment.Queries.SearchBankPayment;
using MiniSalesApp.Application.Banks.Queries.GetBank;
using MiniSalesApp.Application.StoreRecivement.Queries.GetStoreRecivement;
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

namespace MiniSalesApp.UI.BankPayment
{
    public partial class frmBankPaymentForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private List<ModuleRibbonButton> ribbonButtons;
        private FormStates _currentState;
        Application.BankPayment.Dtos.BankPaymentDto BankPayment;
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

        public frmBankPaymentForm(IMediator mediator)
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

        private async Task GetBanks()
        {
            var getResult = await _mediator.Send(new GetBankQuery());

            lkUpBank.Properties.DataSource = getResult;
            lkUpBankSearch.Properties.DataSource = getResult;
        }

        private void SetControlStatus(bool status)
        {
            dtDate.ReadOnly = !status;
            chkIsSupplierPayment.ReadOnly = !status;
            lkUpSupplier.ReadOnly = !status;
            txtAmount.ReadOnly = !status;
            lkUpBank.ReadOnly = !status;
            memoDiscription.ReadOnly = !status;
        }

        private void ClearControls()
        {
            txtSerial.EditValue = string.Empty;
            dtDate.DateTime = DateTime.Now;
            chkIsSupplierPayment.Checked = false;
            lkUpSupplier.EditValue = null;
            lkUpBank.EditValue = null;
            txtAmount.EditValue = default(decimal);
            memoDiscription.EditValue = string.Empty;
            chkIsSupplierPayment_CheckedChanged(null, null);
        }

        private void FillControls()
        {
            txtSerial.EditValue = BankPayment.Serial;
            dtDate.DateTime = BankPayment.Date;
            chkIsSupplierPayment.Checked = BankPayment.IsSupplierPayment;
            lkUpSupplier.EditValue = BankPayment.SupplierId;
            lkUpBank.EditValue = BankPayment.BankId;
            txtAmount.EditValue = BankPayment.Amount;
            memoDiscription.EditValue = BankPayment.Description;
        }

        private void FillData()
        {
            BankPayment.Date = dtDate.DateTime;
            BankPayment.IsSupplierPayment = chkIsSupplierPayment.Checked;
            BankPayment.SupplierId = (int?)lkUpSupplier.EditValue;
            BankPayment.BankId = Convert.ToInt32(lkUpBank.EditValue);
            BankPayment.Amount = Convert.ToDecimal(txtAmount.EditValue);
            BankPayment.Description = memoDiscription.EditValue.ToString();
        }

        private void brBtnNew_ItemClick(object sender, ItemClickEventArgs e)
        {
            BankPayment = new BankPaymentDto();
            SetControlStatus(true);
            ClearControls();
            currentState = FormStates.AddingNew;
        }

        private void brBtnReset_ItemClick(object sender, ItemClickEventArgs e)
        {
            BankPayment = new BankPaymentDto();
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
                var deleteResult = await _mediator.Send(new DeleteBankPaymentCommand()
                {
                    BankPaymentId = BankPayment.BankPaymentId
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

            if (BankPayment.Amount <= 0)
                msg.AppendLine(Messages.AmountCantBeNegative);

            if (BankPayment.IsSupplierPayment && BankPayment.SupplierId == null)
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
            if (!ValidateBankDaily())
                return;

            try
            {
                if (BankPayment.BankPaymentId <= 0)
                {
                    var saveResult = await _mediator.Send(new CreateBankPaymentCommand()
                    {
                        BankPayment = new BankPaymentDto()
                        {
                            IsSupplierPayment = BankPayment.IsSupplierPayment,
                            Date = BankPayment.Date,
                            SupplierId = BankPayment.SupplierId,
                            Amount = BankPayment.Amount,
                            Description = BankPayment.Description,
                            BankId = BankPayment.BankId
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
                    var saveResult = await _mediator.Send(new UpdateBankPaymentCommand()
                    {
                        bankPayment = new BankPaymentDto()
                        {
                            Serial = BankPayment.Serial,
                            IsSupplierPayment = BankPayment.IsSupplierPayment,
                            Date = BankPayment.Date,
                            SupplierId = BankPayment.SupplierId,
                            Amount = BankPayment.Amount,
                            Description = BankPayment.Description,
                            BankPaymentId = BankPayment.BankPaymentId,
                            BankId = BankPayment.BankId
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
            Application.BankPayment.Dtos.BankPaymentDto row = grdVwBankPayment.GetFocusedRow() as Application.BankPayment.Dtos.BankPaymentDto;
            if (row == null)
                return;

            SetControlStatus(true);
            ClearControls();
            BankPayment = row;
            FillControls();
            currentState = FormStates.Editing;
        }

        private async void frmBankPaymentFormNew_Load(object sender, EventArgs e)
        {
            await GetBankDailys();
        }

        private async Task GetBankDailys()
        {
            var getResult = await _mediator.Send(new GetBankPaymentQuery());

            grdCtrBankPayment.DataSource = getResult;
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
            var searchResult = await _mediator.Send(new SearchBankPaymentQuery()
            {
                Serial = (txtSerialSearch.EditValue == null || string.IsNullOrEmpty(txtSerialSearch.EditValue.ToString())) ? null : Convert.ToInt32(txtSerialSearch.EditValue),
                FromDate = dtFromDate.DateTime,
                ToDate = dtToDate.DateTime,
                SupplierId = (int?)lkUpSupplierSearch.EditValue,
                IsSupplierPayment = chkIsSupplierPaymentSearch.Checked,
                BankId = (int?)lkUpBankSearch.EditValue
            });

            grdCtrBankPayment.DataSource = searchResult;
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

        private async void frmBankPaymentFormNew_Activated(object sender, EventArgs e)
        {
            await GetSuppliers();
            await GetBanks();
        }
    }
}