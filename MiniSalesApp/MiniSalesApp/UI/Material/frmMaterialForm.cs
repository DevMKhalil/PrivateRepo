using CSharpFunctionalExtensions;
using DevExpress.XtraBars;
using DevExpress.XtraEditors.Controls;
using MediatR;
using MiniSalesApp.Application.Materials.Commands.CreateMaterial;
using MiniSalesApp.Application.Materials.Commands.DeleteMaterial;
using MiniSalesApp.Application.Materials.Commands.UpdateMaterial;
using MiniSalesApp.Application.Materials.Dtos;
using MiniSalesApp.Application.Materials.Queries.GetMaterial;
using MiniSalesApp.Application.Materials.Queries.SearchMaterial;
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

namespace MiniSalesApp.UI.Material
{
    public partial class frmMaterialForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private List<ModuleRibbonButton> ribbonButtons;
        private FormStates _currentState;
        MaterialDto material;
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
        public frmMaterialForm(IMediator mediator)
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

        private void SetControlStatus(bool status)
        {
            txtCode.ReadOnly = !status;
            txtName.ReadOnly = !status;
            txtSellPrice.ReadOnly = !status;
            txtPurchasePrice.ReadOnly = !status;
        }

        private void ClearControls()
        {
            txtCode.EditValue = default(int);
            txtName.EditValue = string.Empty;
            txtSellPrice.EditValue = default(decimal);
            txtPurchasePrice.EditValue = default(decimal);
        }

        private void FillControls()
        {
            txtCode.EditValue = material.Code;
            txtName.EditValue = material.Name;
            txtSellPrice.EditValue = material.SellPrice;
            txtPurchasePrice.EditValue = material.PurchasePrice;
        }

        private void FillData()
        {
            material.Code = Convert.ToInt32(txtCode.EditValue);
            material.Name = txtName.EditValue.ToString();
            material.SellPrice = Convert.ToDecimal(txtSellPrice.EditValue);
            material.PurchasePrice = Convert.ToDecimal(txtPurchasePrice.EditValue);
        }

        private void brBtnNew_ItemClick(object sender, ItemClickEventArgs e)
        {
            material = new MaterialDto();
            ClearControls();
            SetControlStatus(true);
            currentState = FormStates.AddingNew;
        }

        private void brBtnReset_ItemClick(object sender, ItemClickEventArgs e)
        {
            material = new MaterialDto();
            ClearControls();
            SetControlStatus(false);
            currentState = FormStates.NormalFirstoaded;
        }

        private async Task Delete(int materialId)
        {
            var result = Result.Success();

            result = await _mediator.Send(new DeleteMaterialCommand() { MaterialId = materialId });

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
                progrssForm.SetParamitraizedAction(Delete, material.MaterialId);
                progrssForm.ShowDialog();

                new frmProgressForm(GetMaterials).ShowDialog();
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

        private bool ValidateMaterial()
        {
            StringBuilder msg = new StringBuilder();

            if (string.IsNullOrEmpty(material.Name))
                msg.AppendLine(Messages.NameIsRequired);

            if (material.Code <= 0)
                msg.AppendLine(Messages.CodeIsRequired);

            if (material.SellPrice <= 0)
                msg.AppendLine(Messages.SellPriceIsRequired);

            if (material.PurchasePrice <= 0)
                msg.AppendLine(Messages.PurchasePriceIsRequired);

            if (msg.Length > 0)
            {
                Program.DisplayMessage(msg.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private async Task SaveMaterial(MaterialDto material)
        {
            var result = Result.Success();

            if (material.MaterialId <= 0)
                result = await _mediator.Send(new CreateMaterialCommand() { Material = material });
            else
                result = await _mediator.Send(new UpdateMaterialCommand() { Material = material });

            if (result.IsFailure)
            {
                Program.DisplayMessage(result.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void brBtnSave_ItemClick(object sender, ItemClickEventArgs e)
        {
            FillData();
            if (!ValidateMaterial())
                return;

            try
            {
                var progrssForm = new frmProgressForm();
                progrssForm.SetParamitraizedAction(SaveMaterial, material);
                progrssForm.ShowDialog();

                new frmProgressForm(GetMaterials).ShowDialog();
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
            MaterialDto row = grdVwMaterial.GetFocusedRow() as MaterialDto;
            if (row == null)
                return;

            ClearControls();
            SetControlStatus(true);
            material = row;
            FillControls();
            currentState = FormStates.Editing;
        }

        private void frmMaterialForm_Load(object sender, EventArgs e)
        {
            new frmProgressForm(GetMaterials).ShowDialog();
        }

        private async Task GetMaterials()
        {
            var getResult = await _mediator.Send(new GetMaterialQuery());

            grdCtrMaterial.DataSource = getResult;
        }

        private void txtCode_EditValueChanging(object sender, ChangingEventArgs e)
        {
            if (e.NewValue != null && !string.IsNullOrEmpty(e.NewValue.ToString()) && e.NewValue.ToString().Contains("-"))
                e.Cancel = true;
        }

        private void txtSellPrice_EditValueChanging(object sender, ChangingEventArgs e)
        {
            if (e.NewValue != null && !string.IsNullOrEmpty(e.NewValue.ToString()) && e.NewValue.ToString().Contains("-"))
                e.Cancel = true;
        }

        private void txtPurchasePrice_EditValueChanging(object sender, ChangingEventArgs e)
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
            txtCodeSearch.EditValue = default(int?);
            txtNameSearch.EditValue = string.Empty;
            txtSellPriceSearch.EditValue = default(decimal?);
            txtPurchasePriceSearch.EditValue = default(decimal?);
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            var searchResult = await _mediator.Send(new SearchMaterialQuery()
            {
                Code = (int?)txtCodeSearch.EditValue,
                Name = txtNameSearch.EditValue.ToString(),
                SellPrice = (int?)txtSellPriceSearch.EditValue,
                PurchasePrice = (int?)txtPurchasePriceSearch.EditValue
            });

            grdCtrMaterial.DataSource = searchResult;
        }

        private void txtCodeSearch_EditValueChanging(object sender, ChangingEventArgs e)
        {
            if (e.NewValue != null && !string.IsNullOrEmpty(e.NewValue.ToString()) && e.NewValue.ToString().Contains("-"))
                e.Cancel = true;
        }

        private void txtSellPriceSearch_EditValueChanging(object sender, ChangingEventArgs e)
        {
            if (e.NewValue != null && !string.IsNullOrEmpty(e.NewValue.ToString()) && e.NewValue.ToString().Contains("-"))
                e.Cancel = true;
        }

        private void txtPurchasePriceSearch_EditValueChanging(object sender, ChangingEventArgs e)
        {
            if (e.NewValue != null && !string.IsNullOrEmpty(e.NewValue.ToString()) && e.NewValue.ToString().Contains("-"))
                e.Cancel = true;
        }
    }
}