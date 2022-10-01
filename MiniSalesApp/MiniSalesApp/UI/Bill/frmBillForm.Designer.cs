
namespace MiniSalesApp.UI.Bill
{
    partial class frmBillForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBillForm));
            this.ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.brBtnNew = new DevExpress.XtraBars.BarButtonItem();
            this.brBtnSave = new DevExpress.XtraBars.BarButtonItem();
            this.brBtnDelete = new DevExpress.XtraBars.BarButtonItem();
            this.brBtnOpen = new DevExpress.XtraBars.BarButtonItem();
            this.brBtnReset = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonStatusBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.grdCtrMaterial = new DevExpress.XtraGrid.GridControl();
            this.grdVwMaterial = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colMaterial = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rpsSearchLkUpMaterial = new DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit();
            this.repositoryItemSearchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colQty = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rpsTxtQty = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colSellPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDelete = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rpsBtnDelete = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.lkUpSupplier = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.lkUpCustomerView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colSerial = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCustomerName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.memoDiscription = new DevExpress.XtraEditors.MemoExEdit();
            this.dtDate = new DevExpress.XtraEditors.DateEdit();
            this.txtTotalAfterDiscount = new DevExpress.XtraEditors.TextEdit();
            this.txtDiscount = new DevExpress.XtraEditors.TextEdit();
            this.txtSerial = new DevExpress.XtraEditors.TextEdit();
            this.txttotal = new DevExpress.XtraEditors.TextEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdCtrMaterial)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdVwMaterial)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpsSearchLkUpMaterial)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpsTxtQty)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpsBtnDelete)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkUpSupplier.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkUpCustomerView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoDiscription.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalAfterDiscount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDiscount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSerial.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txttotal.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbon
            // 
            this.ribbon.ExpandCollapseItem.Id = 0;
            this.ribbon.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbon.ExpandCollapseItem,
            this.ribbon.SearchEditItem,
            this.brBtnNew,
            this.brBtnSave,
            this.brBtnDelete,
            this.brBtnOpen,
            this.brBtnReset});
            this.ribbon.Location = new System.Drawing.Point(0, 0);
            this.ribbon.MaxItemId = 6;
            this.ribbon.Name = "ribbon";
            this.ribbon.QuickToolbarItemLinks.Add(this.brBtnNew);
            this.ribbon.QuickToolbarItemLinks.Add(this.brBtnSave);
            this.ribbon.QuickToolbarItemLinks.Add(this.brBtnDelete);
            this.ribbon.QuickToolbarItemLinks.Add(this.brBtnOpen);
            this.ribbon.QuickToolbarItemLinks.Add(this.brBtnReset);
            this.ribbon.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.False;
            this.ribbon.Size = new System.Drawing.Size(1015, 58);
            this.ribbon.StatusBar = this.ribbonStatusBar;
            // 
            // brBtnNew
            // 
            this.brBtnNew.Caption = "barButtonItem1";
            this.brBtnNew.Id = 1;
            this.brBtnNew.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("brBtnNew.ImageOptions.Image")));
            this.brBtnNew.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("brBtnNew.ImageOptions.LargeImage")));
            this.brBtnNew.Name = "brBtnNew";
            this.brBtnNew.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.brBtnNew_ItemClick);
            // 
            // brBtnSave
            // 
            this.brBtnSave.Caption = "barButtonItem2";
            this.brBtnSave.Id = 2;
            this.brBtnSave.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("brBtnSave.ImageOptions.Image")));
            this.brBtnSave.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("brBtnSave.ImageOptions.LargeImage")));
            this.brBtnSave.Name = "brBtnSave";
            this.brBtnSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.brBtnSave_ItemClick);
            // 
            // brBtnDelete
            // 
            this.brBtnDelete.Caption = "barButtonItem3";
            this.brBtnDelete.Id = 3;
            this.brBtnDelete.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("brBtnDelete.ImageOptions.Image")));
            this.brBtnDelete.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("brBtnDelete.ImageOptions.LargeImage")));
            this.brBtnDelete.Name = "brBtnDelete";
            this.brBtnDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.brBtnDelete_ItemClick);
            // 
            // brBtnOpen
            // 
            this.brBtnOpen.Caption = "barButtonItem4";
            this.brBtnOpen.Id = 4;
            this.brBtnOpen.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("brBtnOpen.ImageOptions.Image")));
            this.brBtnOpen.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("brBtnOpen.ImageOptions.LargeImage")));
            this.brBtnOpen.Name = "brBtnOpen";
            this.brBtnOpen.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.brBtnOpen_ItemClick);
            // 
            // brBtnReset
            // 
            this.brBtnReset.Caption = "barButtonItem5";
            this.brBtnReset.Id = 5;
            this.brBtnReset.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("brBtnReset.ImageOptions.Image")));
            this.brBtnReset.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("brBtnReset.ImageOptions.LargeImage")));
            this.brBtnReset.Name = "brBtnReset";
            this.brBtnReset.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.brBtnReset_ItemClick);
            // 
            // ribbonStatusBar
            // 
            this.ribbonStatusBar.Location = new System.Drawing.Point(0, 688);
            this.ribbonStatusBar.Name = "ribbonStatusBar";
            this.ribbonStatusBar.Ribbon = this.ribbon;
            this.ribbonStatusBar.Size = new System.Drawing.Size(1015, 24);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.grdCtrMaterial);
            this.layoutControl1.Controls.Add(this.lkUpSupplier);
            this.layoutControl1.Controls.Add(this.memoDiscription);
            this.layoutControl1.Controls.Add(this.dtDate);
            this.layoutControl1.Controls.Add(this.txtTotalAfterDiscount);
            this.layoutControl1.Controls.Add(this.txtDiscount);
            this.layoutControl1.Controls.Add(this.txtSerial);
            this.layoutControl1.Controls.Add(this.txttotal);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 58);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1015, 630);
            this.layoutControl1.TabIndex = 2;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // grdCtrMaterial
            // 
            this.grdCtrMaterial.Location = new System.Drawing.Point(12, 108);
            this.grdCtrMaterial.MainView = this.grdVwMaterial;
            this.grdCtrMaterial.MenuManager = this.ribbon;
            this.grdCtrMaterial.Name = "grdCtrMaterial";
            this.grdCtrMaterial.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rpsSearchLkUpMaterial,
            this.rpsTxtQty,
            this.rpsBtnDelete});
            this.grdCtrMaterial.Size = new System.Drawing.Size(991, 510);
            this.grdCtrMaterial.TabIndex = 12;
            this.grdCtrMaterial.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdVwMaterial});
            // 
            // grdVwMaterial
            // 
            this.grdVwMaterial.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colMaterial,
            this.colQty,
            this.colSellPrice,
            this.colDelete});
            this.grdVwMaterial.GridControl = this.grdCtrMaterial;
            this.grdVwMaterial.Name = "grdVwMaterial";
            this.grdVwMaterial.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
            this.grdVwMaterial.OptionsView.ShowGroupPanel = false;
            this.grdVwMaterial.ValidateRow += new DevExpress.XtraGrid.Views.Base.ValidateRowEventHandler(this.GrdVwMaterial_ValidateRow);
            // 
            // colMaterial
            // 
            this.colMaterial.Caption = "الصنف";
            this.colMaterial.ColumnEdit = this.rpsSearchLkUpMaterial;
            this.colMaterial.FieldName = "MaterialId";
            this.colMaterial.Name = "colMaterial";
            this.colMaterial.Visible = true;
            this.colMaterial.VisibleIndex = 0;
            // 
            // rpsSearchLkUpMaterial
            // 
            this.rpsSearchLkUpMaterial.AutoHeight = false;
            this.rpsSearchLkUpMaterial.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rpsSearchLkUpMaterial.DisplayMember = "Name";
            this.rpsSearchLkUpMaterial.Name = "rpsSearchLkUpMaterial";
            this.rpsSearchLkUpMaterial.NullText = "--اختر--";
            this.rpsSearchLkUpMaterial.PopupView = this.repositoryItemSearchLookUpEdit1View;
            this.rpsSearchLkUpMaterial.ValueMember = "MaterialId";
            this.rpsSearchLkUpMaterial.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.RpsSearchLkUpMaterial_EditValueChanging);
            // 
            // repositoryItemSearchLookUpEdit1View
            // 
            this.repositoryItemSearchLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colCode,
            this.colName});
            this.repositoryItemSearchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.repositoryItemSearchLookUpEdit1View.Name = "repositoryItemSearchLookUpEdit1View";
            this.repositoryItemSearchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.repositoryItemSearchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // colCode
            // 
            this.colCode.Caption = "الكود";
            this.colCode.FieldName = "Code";
            this.colCode.Name = "colCode";
            this.colCode.OptionsColumn.AllowEdit = false;
            this.colCode.OptionsColumn.AllowFocus = false;
            this.colCode.Visible = true;
            this.colCode.VisibleIndex = 0;
            // 
            // colName
            // 
            this.colName.Caption = "الاسم";
            this.colName.FieldName = "Name";
            this.colName.Name = "colName";
            this.colName.OptionsColumn.AllowEdit = false;
            this.colName.OptionsColumn.AllowFocus = false;
            this.colName.Visible = true;
            this.colName.VisibleIndex = 1;
            // 
            // colQty
            // 
            this.colQty.Caption = "الكمية";
            this.colQty.ColumnEdit = this.rpsTxtQty;
            this.colQty.FieldName = "Quantity";
            this.colQty.Name = "colQty";
            this.colQty.Visible = true;
            this.colQty.VisibleIndex = 1;
            // 
            // rpsTxtQty
            // 
            this.rpsTxtQty.AutoHeight = false;
            this.rpsTxtQty.Name = "rpsTxtQty";
            this.rpsTxtQty.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.RpsTxtQty_EditValueChanging);
            // 
            // colSellPrice
            // 
            this.colSellPrice.Caption = "السعر";
            this.colSellPrice.ColumnEdit = this.rpsTxtQty;
            this.colSellPrice.FieldName = "PurchasePrice";
            this.colSellPrice.Name = "colSellPrice";
            this.colSellPrice.Visible = true;
            this.colSellPrice.VisibleIndex = 2;
            // 
            // colDelete
            // 
            this.colDelete.Caption = "حذف";
            this.colDelete.ColumnEdit = this.rpsBtnDelete;
            this.colDelete.Name = "colDelete";
            this.colDelete.Visible = true;
            this.colDelete.VisibleIndex = 3;
            // 
            // rpsBtnDelete
            // 
            this.rpsBtnDelete.AutoHeight = false;
            this.rpsBtnDelete.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.rpsBtnDelete.Name = "rpsBtnDelete";
            this.rpsBtnDelete.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.rpsBtnDelete.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.RpsBtnDelete_ButtonClick);
            // 
            // lkUpSupplier
            // 
            this.lkUpSupplier.Location = new System.Drawing.Point(613, 12);
            this.lkUpSupplier.MenuManager = this.ribbon;
            this.lkUpSupplier.Name = "lkUpSupplier";
            this.lkUpSupplier.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkUpSupplier.Properties.DisplayMember = "Name";
            this.lkUpSupplier.Properties.NullText = "--اختر--";
            this.lkUpSupplier.Properties.PopupView = this.lkUpCustomerView;
            this.lkUpSupplier.Properties.ValueMember = "SupplierId";
            this.lkUpSupplier.Size = new System.Drawing.Size(390, 20);
            this.lkUpSupplier.StyleController = this.layoutControl1;
            this.lkUpSupplier.TabIndex = 12;
            // 
            // lkUpCustomerView
            // 
            this.lkUpCustomerView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colSerial,
            this.colCustomerName});
            this.lkUpCustomerView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.lkUpCustomerView.Name = "lkUpCustomerView";
            this.lkUpCustomerView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.lkUpCustomerView.OptionsView.ShowGroupPanel = false;
            // 
            // colSerial
            // 
            this.colSerial.Caption = "السيريال";
            this.colSerial.FieldName = "Serial";
            this.colSerial.Name = "colSerial";
            this.colSerial.Visible = true;
            this.colSerial.VisibleIndex = 0;
            // 
            // colCustomerName
            // 
            this.colCustomerName.Caption = "الاسم";
            this.colCustomerName.FieldName = "Name";
            this.colCustomerName.Name = "colCustomerName";
            this.colCustomerName.Visible = true;
            this.colCustomerName.VisibleIndex = 1;
            // 
            // memoDiscription
            // 
            this.memoDiscription.Location = new System.Drawing.Point(116, 84);
            this.memoDiscription.MenuManager = this.ribbon;
            this.memoDiscription.Name = "memoDiscription";
            this.memoDiscription.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.memoDiscription.Size = new System.Drawing.Size(887, 20);
            this.memoDiscription.StyleController = this.layoutControl1;
            this.memoDiscription.TabIndex = 17;
            // 
            // dtDate
            // 
            this.dtDate.EditValue = null;
            this.dtDate.Location = new System.Drawing.Point(613, 60);
            this.dtDate.MenuManager = this.ribbon;
            this.dtDate.Name = "dtDate";
            this.dtDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtDate.Size = new System.Drawing.Size(390, 20);
            this.dtDate.StyleController = this.layoutControl1;
            this.dtDate.TabIndex = 16;
            // 
            // txtTotalAfterDiscount
            // 
            this.txtTotalAfterDiscount.Location = new System.Drawing.Point(116, 60);
            this.txtTotalAfterDiscount.MenuManager = this.ribbon;
            this.txtTotalAfterDiscount.Name = "txtTotalAfterDiscount";
            this.txtTotalAfterDiscount.Properties.ReadOnly = true;
            this.txtTotalAfterDiscount.Size = new System.Drawing.Size(389, 20);
            this.txtTotalAfterDiscount.StyleController = this.layoutControl1;
            this.txtTotalAfterDiscount.TabIndex = 15;
            // 
            // txtDiscount
            // 
            this.txtDiscount.Location = new System.Drawing.Point(613, 36);
            this.txtDiscount.MenuManager = this.ribbon;
            this.txtDiscount.Name = "txtDiscount";
            this.txtDiscount.Size = new System.Drawing.Size(390, 20);
            this.txtDiscount.StyleController = this.layoutControl1;
            this.txtDiscount.TabIndex = 14;
            this.txtDiscount.EditValueChanged += new System.EventHandler(this.txtDiscount_EditValueChanged);
            this.txtDiscount.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.TxtDiscount_EditValueChanging);
            // 
            // txtSerial
            // 
            this.txtSerial.Location = new System.Drawing.Point(116, 12);
            this.txtSerial.MenuManager = this.ribbon;
            this.txtSerial.Name = "txtSerial";
            this.txtSerial.Properties.ReadOnly = true;
            this.txtSerial.Size = new System.Drawing.Size(389, 20);
            this.txtSerial.StyleController = this.layoutControl1;
            this.txtSerial.TabIndex = 11;
            // 
            // txttotal
            // 
            this.txttotal.Location = new System.Drawing.Point(116, 36);
            this.txttotal.MenuManager = this.ribbon;
            this.txttotal.Name = "txttotal";
            this.txttotal.Properties.ReadOnly = true;
            this.txttotal.Size = new System.Drawing.Size(389, 20);
            this.txttotal.StyleController = this.layoutControl1;
            this.txttotal.TabIndex = 13;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem4,
            this.layoutControlItem6,
            this.layoutControlItem7,
            this.layoutControlItem3,
            this.layoutControlItem5,
            this.layoutControlItem8});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(1015, 630);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.txtSerial;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(497, 24);
            this.layoutControlItem1.Text = "السيريال";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(92, 13);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.txttotal;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(497, 24);
            this.layoutControlItem2.Text = "الاجمالي";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(92, 13);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.txtTotalAfterDiscount;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 48);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(497, 24);
            this.layoutControlItem4.Text = "الاجمالي بعد الخصم";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(92, 13);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.memoDiscription;
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 72);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(995, 24);
            this.layoutControlItem6.Text = "الوصف";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(92, 13);
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.lkUpSupplier;
            this.layoutControlItem7.Location = new System.Drawing.Point(497, 0);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(498, 24);
            this.layoutControlItem7.Text = "المورد";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(92, 13);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.txtDiscount;
            this.layoutControlItem3.Location = new System.Drawing.Point(497, 24);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(498, 24);
            this.layoutControlItem3.Text = "الخصم";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(92, 13);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.dtDate;
            this.layoutControlItem5.Location = new System.Drawing.Point(497, 48);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(498, 24);
            this.layoutControlItem5.Text = "التاريخ";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(92, 13);
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.grdCtrMaterial;
            this.layoutControlItem8.Location = new System.Drawing.Point(0, 96);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(995, 514);
            this.layoutControlItem8.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem8.TextVisible = false;
            // 
            // frmBillForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1015, 712);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.ribbonStatusBar);
            this.Controls.Add(this.ribbon);
            this.IconOptions.ShowIcon = false;
            this.Name = "frmBillForm";
            this.Ribbon = this.ribbon;
            this.StatusBar = this.ribbonStatusBar;
            this.Text = "فاتورة شراء";
            this.Activated += new System.EventHandler(this.frmBillForm_Activated);
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdCtrMaterial)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdVwMaterial)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpsSearchLkUpMaterial)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpsTxtQty)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpsBtnDelete)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkUpSupplier.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkUpCustomerView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoDiscription.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalAfterDiscount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDiscount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSerial.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txttotal.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbon;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraEditors.SearchLookUpEdit lkUpSupplier;
        private DevExpress.XtraGrid.Views.Grid.GridView lkUpCustomerView;
        private DevExpress.XtraGrid.Columns.GridColumn colSerial;
        private DevExpress.XtraGrid.Columns.GridColumn colCustomerName;
        private DevExpress.XtraEditors.MemoExEdit memoDiscription;
        private DevExpress.XtraEditors.DateEdit dtDate;
        private DevExpress.XtraEditors.TextEdit txtTotalAfterDiscount;
        private DevExpress.XtraEditors.TextEdit txtDiscount;
        private DevExpress.XtraEditors.TextEdit txtSerial;
        private DevExpress.XtraEditors.TextEdit txttotal;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraGrid.GridControl grdCtrMaterial;
        private DevExpress.XtraGrid.Views.Grid.GridView grdVwMaterial;
        private DevExpress.XtraGrid.Columns.GridColumn colMaterial;
        private DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit rpsSearchLkUpMaterial;
        private DevExpress.XtraGrid.Views.Grid.GridView repositoryItemSearchLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn colCode;
        private DevExpress.XtraGrid.Columns.GridColumn colName;
        private DevExpress.XtraGrid.Columns.GridColumn colQty;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rpsTxtQty;
        private DevExpress.XtraGrid.Columns.GridColumn colSellPrice;
        private DevExpress.XtraGrid.Columns.GridColumn colDelete;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit rpsBtnDelete;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraBars.BarButtonItem brBtnNew;
        private DevExpress.XtraBars.BarButtonItem brBtnSave;
        private DevExpress.XtraBars.BarButtonItem brBtnDelete;
        private DevExpress.XtraBars.BarButtonItem brBtnOpen;
        private DevExpress.XtraBars.BarButtonItem brBtnReset;
    }
}