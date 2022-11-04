namespace SQLHelper
{
    partial class frmQueryHelper
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmQueryHelper));
            this.ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.brBtnRefresh = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonStatusBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btnAddParamsToQuery = new DevExpress.XtraEditors.SimpleButton();
            this.btnGetParams = new DevExpress.XtraEditors.SimpleButton();
            this.grdCtrParameters = new DevExpress.XtraGrid.GridControl();
            this.grdVwParameters = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colParamName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPramType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rpsLkUpTypeName = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.colParamSize = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rpsTxtParamSize = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colParamSize2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rpsTxtParamSize2 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colParamValue = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rpsTxtParamValueString = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.rpsTxtParamValueInt = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.rpsTxtParamValueDecimal = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.rpsTxtParamValueBool = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.rpsTxtParamValueDateTime = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.memofinalQuery = new DevExpress.XtraEditors.MemoEdit();
            this.memoInPutQuery = new DevExpress.XtraEditors.MemoEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdCtrParameters)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdVwParameters)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpsLkUpTypeName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpsTxtParamSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpsTxtParamSize2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpsTxtParamValueString)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpsTxtParamValueInt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpsTxtParamValueDecimal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpsTxtParamValueBool)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpsTxtParamValueDateTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpsTxtParamValueDateTime.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memofinalQuery.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoInPutQuery.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbon
            // 
            this.ribbon.ExpandCollapseItem.Id = 0;
            this.ribbon.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbon.ExpandCollapseItem,
            this.ribbon.SearchEditItem,
            this.brBtnRefresh});
            this.ribbon.Location = new System.Drawing.Point(0, 0);
            this.ribbon.MaxItemId = 2;
            this.ribbon.Name = "ribbon";
            this.ribbon.QuickToolbarItemLinks.Add(this.brBtnRefresh);
            this.ribbon.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.False;
            this.ribbon.Size = new System.Drawing.Size(807, 49);
            this.ribbon.StatusBar = this.ribbonStatusBar;
            // 
            // brBtnRefresh
            // 
            this.brBtnRefresh.Caption = "Refresh";
            this.brBtnRefresh.Id = 1;
            this.brBtnRefresh.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("brBtnRefresh.ImageOptions.Image")));
            this.brBtnRefresh.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("brBtnRefresh.ImageOptions.LargeImage")));
            this.brBtnRefresh.Name = "brBtnRefresh";
            this.brBtnRefresh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.brBtnRefresh_ItemClick);
            // 
            // ribbonStatusBar
            // 
            this.ribbonStatusBar.Location = new System.Drawing.Point(0, 495);
            this.ribbonStatusBar.Name = "ribbonStatusBar";
            this.ribbonStatusBar.Ribbon = this.ribbon;
            this.ribbonStatusBar.Size = new System.Drawing.Size(807, 31);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btnAddParamsToQuery);
            this.layoutControl1.Controls.Add(this.btnGetParams);
            this.layoutControl1.Controls.Add(this.grdCtrParameters);
            this.layoutControl1.Controls.Add(this.memofinalQuery);
            this.layoutControl1.Controls.Add(this.memoInPutQuery);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 49);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(807, 446);
            this.layoutControl1.TabIndex = 2;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // btnAddParamsToQuery
            // 
            this.btnAddParamsToQuery.Location = new System.Drawing.Point(250, 412);
            this.btnAddParamsToQuery.Name = "btnAddParamsToQuery";
            this.btnAddParamsToQuery.Size = new System.Drawing.Size(321, 22);
            this.btnAddParamsToQuery.StyleController = this.layoutControl1;
            this.btnAddParamsToQuery.TabIndex = 5;
            this.btnAddParamsToQuery.Text = "Add Parameters To Query";
            this.btnAddParamsToQuery.Click += new System.EventHandler(this.btnAddParamsToQuery_Click);
            // 
            // btnGetParams
            // 
            this.btnGetParams.Location = new System.Drawing.Point(12, 412);
            this.btnGetParams.Name = "btnGetParams";
            this.btnGetParams.Size = new System.Drawing.Size(234, 22);
            this.btnGetParams.StyleController = this.layoutControl1;
            this.btnGetParams.TabIndex = 4;
            this.btnGetParams.Text = "Get Parameters";
            this.btnGetParams.Click += new System.EventHandler(this.btnGetParams_Click);
            // 
            // grdCtrParameters
            // 
            this.grdCtrParameters.Location = new System.Drawing.Point(250, 12);
            this.grdCtrParameters.MainView = this.grdVwParameters;
            this.grdCtrParameters.MenuManager = this.ribbon;
            this.grdCtrParameters.Name = "grdCtrParameters";
            this.grdCtrParameters.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rpsLkUpTypeName,
            this.rpsTxtParamSize,
            this.rpsTxtParamSize2,
            this.rpsTxtParamValueString,
            this.rpsTxtParamValueInt,
            this.rpsTxtParamValueDecimal,
            this.rpsTxtParamValueBool,
            this.rpsTxtParamValueDateTime});
            this.grdCtrParameters.Size = new System.Drawing.Size(321, 396);
            this.grdCtrParameters.TabIndex = 2;
            this.grdCtrParameters.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdVwParameters});
            // 
            // grdVwParameters
            // 
            this.grdVwParameters.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colParamName,
            this.colPramType,
            this.colParamSize,
            this.colParamSize2,
            this.colParamValue});
            this.grdVwParameters.GridControl = this.grdCtrParameters;
            this.grdVwParameters.Name = "grdVwParameters";
            this.grdVwParameters.OptionsView.ShowGroupPanel = false;
            this.grdVwParameters.CustomRowCellEdit += new DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventHandler(this.grdVwParameters_CustomRowCellEdit);
            this.grdVwParameters.CustomRowCellEditForEditing += new DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventHandler(this.grdVwParameters_CustomRowCellEditForEditing);
            this.grdVwParameters.ValidateRow += new DevExpress.XtraGrid.Views.Base.ValidateRowEventHandler(this.grdVwParameters_ValidateRow);
            // 
            // colParamName
            // 
            this.colParamName.Caption = "Parameter Name";
            this.colParamName.FieldName = "ParamName";
            this.colParamName.Name = "colParamName";
            this.colParamName.OptionsColumn.AllowEdit = false;
            this.colParamName.OptionsColumn.AllowFocus = false;
            this.colParamName.Visible = true;
            this.colParamName.VisibleIndex = 0;
            // 
            // colPramType
            // 
            this.colPramType.Caption = "Pramter Type";
            this.colPramType.ColumnEdit = this.rpsLkUpTypeName;
            this.colPramType.FieldName = "ParamType";
            this.colPramType.Name = "colPramType";
            this.colPramType.Visible = true;
            this.colPramType.VisibleIndex = 1;
            // 
            // rpsLkUpTypeName
            // 
            this.rpsLkUpTypeName.AutoHeight = false;
            this.rpsLkUpTypeName.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rpsLkUpTypeName.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ParamTypeNameStraing", "")});
            this.rpsLkUpTypeName.DisplayMember = "ParamTypeNameStraing";
            this.rpsLkUpTypeName.Name = "rpsLkUpTypeName";
            this.rpsLkUpTypeName.NullText = "--Select--";
            this.rpsLkUpTypeName.ValueMember = "ParamTypeNameStraing";
            // 
            // colParamSize
            // 
            this.colParamSize.Caption = "Paramter Size";
            this.colParamSize.ColumnEdit = this.rpsTxtParamSize;
            this.colParamSize.FieldName = "ParamSize";
            this.colParamSize.Name = "colParamSize";
            this.colParamSize.Visible = true;
            this.colParamSize.VisibleIndex = 2;
            // 
            // rpsTxtParamSize
            // 
            this.rpsTxtParamSize.AutoHeight = false;
            this.rpsTxtParamSize.DisplayFormat.FormatString = "n0";
            this.rpsTxtParamSize.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.rpsTxtParamSize.EditFormat.FormatString = "n0";
            this.rpsTxtParamSize.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.rpsTxtParamSize.Mask.EditMask = "n0";
            this.rpsTxtParamSize.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.rpsTxtParamSize.Mask.UseMaskAsDisplayFormat = true;
            this.rpsTxtParamSize.Name = "rpsTxtParamSize";
            this.rpsTxtParamSize.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.rpsTxtParamSize_EditValueChanging);
            // 
            // colParamSize2
            // 
            this.colParamSize2.Caption = "Paramter Size2";
            this.colParamSize2.ColumnEdit = this.rpsTxtParamSize2;
            this.colParamSize2.FieldName = "ParamSize2";
            this.colParamSize2.Name = "colParamSize2";
            this.colParamSize2.Visible = true;
            this.colParamSize2.VisibleIndex = 3;
            // 
            // rpsTxtParamSize2
            // 
            this.rpsTxtParamSize2.AutoHeight = false;
            this.rpsTxtParamSize2.DisplayFormat.FormatString = "n0";
            this.rpsTxtParamSize2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.rpsTxtParamSize2.EditFormat.FormatString = "n0";
            this.rpsTxtParamSize2.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.rpsTxtParamSize2.Mask.EditMask = "n0";
            this.rpsTxtParamSize2.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.rpsTxtParamSize2.Mask.UseMaskAsDisplayFormat = true;
            this.rpsTxtParamSize2.Name = "rpsTxtParamSize2";
            this.rpsTxtParamSize2.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.rpsTxtParamSize2_EditValueChanging);
            // 
            // colParamValue
            // 
            this.colParamValue.Caption = "Value";
            this.colParamValue.ColumnEdit = this.rpsTxtParamValueString;
            this.colParamValue.FieldName = "ParamValue";
            this.colParamValue.Name = "colParamValue";
            // 
            // rpsTxtParamValueString
            // 
            this.rpsTxtParamValueString.AutoHeight = false;
            this.rpsTxtParamValueString.DisplayFormat.FormatString = "\\D+";
            this.rpsTxtParamValueString.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.rpsTxtParamValueString.EditFormat.FormatString = "\\D+";
            this.rpsTxtParamValueString.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.rpsTxtParamValueString.Mask.EditMask = "\\D+";
            this.rpsTxtParamValueString.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.rpsTxtParamValueString.Mask.ShowPlaceHolders = false;
            this.rpsTxtParamValueString.Mask.UseMaskAsDisplayFormat = true;
            this.rpsTxtParamValueString.Name = "rpsTxtParamValueString";
            // 
            // rpsTxtParamValueInt
            // 
            this.rpsTxtParamValueInt.AutoHeight = false;
            this.rpsTxtParamValueInt.DisplayFormat.FormatString = "n0";
            this.rpsTxtParamValueInt.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.rpsTxtParamValueInt.EditFormat.FormatString = "n0";
            this.rpsTxtParamValueInt.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.rpsTxtParamValueInt.Mask.EditMask = "n0";
            this.rpsTxtParamValueInt.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.rpsTxtParamValueInt.Mask.UseMaskAsDisplayFormat = true;
            this.rpsTxtParamValueInt.Name = "rpsTxtParamValueInt";
            this.rpsTxtParamValueInt.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.rpsTxtParamValueInt_EditValueChanging);
            // 
            // rpsTxtParamValueDecimal
            // 
            this.rpsTxtParamValueDecimal.AutoHeight = false;
            this.rpsTxtParamValueDecimal.DisplayFormat.FormatString = "n4";
            this.rpsTxtParamValueDecimal.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.rpsTxtParamValueDecimal.EditFormat.FormatString = "n4";
            this.rpsTxtParamValueDecimal.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.rpsTxtParamValueDecimal.Mask.EditMask = "n4";
            this.rpsTxtParamValueDecimal.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.rpsTxtParamValueDecimal.Mask.UseMaskAsDisplayFormat = true;
            this.rpsTxtParamValueDecimal.Name = "rpsTxtParamValueDecimal";
            this.rpsTxtParamValueDecimal.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.rpsTxtParamValueDecimal_EditValueChanging);
            // 
            // rpsTxtParamValueBool
            // 
            this.rpsTxtParamValueBool.AutoHeight = false;
            this.rpsTxtParamValueBool.Name = "rpsTxtParamValueBool";
            // 
            // rpsTxtParamValueDateTime
            // 
            this.rpsTxtParamValueDateTime.AutoHeight = false;
            this.rpsTxtParamValueDateTime.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rpsTxtParamValueDateTime.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rpsTxtParamValueDateTime.Name = "rpsTxtParamValueDateTime";
            this.rpsTxtParamValueDateTime.EditValueChanged += new System.EventHandler(this.rpsTxtParamValueDateTime_EditValueChanged);
            // 
            // memofinalQuery
            // 
            this.memofinalQuery.Location = new System.Drawing.Point(575, 12);
            this.memofinalQuery.MenuManager = this.ribbon;
            this.memofinalQuery.Name = "memofinalQuery";
            this.memofinalQuery.Properties.ReadOnly = true;
            this.memofinalQuery.Properties.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.memofinalQuery.Size = new System.Drawing.Size(220, 422);
            this.memofinalQuery.StyleController = this.layoutControl1;
            this.memofinalQuery.TabIndex = 3;
            // 
            // memoInPutQuery
            // 
            this.memoInPutQuery.Location = new System.Drawing.Point(12, 12);
            this.memoInPutQuery.MenuManager = this.ribbon;
            this.memoInPutQuery.Name = "memoInPutQuery";
            this.memoInPutQuery.Properties.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.memoInPutQuery.Size = new System.Drawing.Size(234, 396);
            this.memoInPutQuery.StyleController = this.layoutControl1;
            this.memoInPutQuery.TabIndex = 0;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(807, 446);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.memoInPutQuery;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(238, 400);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.memofinalQuery;
            this.layoutControlItem2.Location = new System.Drawing.Point(563, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(224, 426);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.grdCtrParameters;
            this.layoutControlItem3.Location = new System.Drawing.Point(238, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(325, 400);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.btnGetParams;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 400);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(238, 26);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.btnAddParamsToQuery;
            this.layoutControlItem5.Location = new System.Drawing.Point(238, 400);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(325, 26);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // frmQueryHelper
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(807, 526);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.ribbonStatusBar);
            this.Controls.Add(this.ribbon);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmQueryHelper";
            this.Ribbon = this.ribbon;
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.StatusBar = this.ribbonStatusBar;
            this.Text = "Query Helper";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdCtrParameters)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdVwParameters)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpsLkUpTypeName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpsTxtParamSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpsTxtParamSize2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpsTxtParamValueString)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpsTxtParamValueInt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpsTxtParamValueDecimal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpsTxtParamValueBool)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpsTxtParamValueDateTime.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpsTxtParamValueDateTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memofinalQuery.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoInPutQuery.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbon;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraGrid.GridControl grdCtrParameters;
        private DevExpress.XtraGrid.Views.Grid.GridView grdVwParameters;
        private DevExpress.XtraGrid.Columns.GridColumn colParamName;
        private DevExpress.XtraGrid.Columns.GridColumn colPramType;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit rpsLkUpTypeName;
        private DevExpress.XtraGrid.Columns.GridColumn colParamSize;
        private DevExpress.XtraEditors.MemoEdit memofinalQuery;
        private DevExpress.XtraEditors.MemoEdit memoInPutQuery;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.SimpleButton btnAddParamsToQuery;
        private DevExpress.XtraEditors.SimpleButton btnGetParams;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraBars.BarButtonItem brBtnRefresh;
        private DevExpress.XtraGrid.Columns.GridColumn colParamSize2;
        private DevExpress.XtraGrid.Columns.GridColumn colParamValue;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rpsTxtParamSize;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rpsTxtParamSize2;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rpsTxtParamValueString;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rpsTxtParamValueInt;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rpsTxtParamValueDecimal;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit rpsTxtParamValueBool;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit rpsTxtParamValueDateTime;
    }
}