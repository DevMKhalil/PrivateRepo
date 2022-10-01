
namespace MiniSalesApp.UI
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.ribbonStatusBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.defaultLookAndFeel1 = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
            this.xtraTabbedMdiManager1 = new DevExpress.XtraTabbedMdi.XtraTabbedMdiManager(this.components);
            this.accordionControl1 = new DevExpress.XtraBars.Navigation.AccordionControl();
            this.accordionControlElement1 = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.btnMaterial = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.btnCustomer = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.btnSupplier = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.accordionControlElement5 = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.btnInvoice = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.btnBill = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.accordionControlElement8 = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.btnStoreDaily = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.btnStoreRecivement = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.btnStorePayment = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.accordionControlElement12 = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.btnBank = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.btnBankRecivement = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.btnBankPayment = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabbedMdiManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.accordionControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbon
            // 
            this.ribbon.ExpandCollapseItem.Id = 0;
            this.ribbon.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbon.ExpandCollapseItem,
            this.ribbon.SearchEditItem});
            resources.ApplyResources(this.ribbon, "ribbon");
            this.ribbon.MaxItemId = 1;
            this.ribbon.Name = "ribbon";
            this.ribbon.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.False;
            this.ribbon.StatusBar = this.ribbonStatusBar;
            // 
            // ribbonStatusBar
            // 
            resources.ApplyResources(this.ribbonStatusBar, "ribbonStatusBar");
            this.ribbonStatusBar.Name = "ribbonStatusBar";
            this.ribbonStatusBar.Ribbon = this.ribbon;
            // 
            // defaultLookAndFeel1
            // 
            this.defaultLookAndFeel1.LookAndFeel.SkinName = "Office 2016 Colorful";
            // 
            // xtraTabbedMdiManager1
            // 
            this.xtraTabbedMdiManager1.ClosePageButtonShowMode = DevExpress.XtraTab.ClosePageButtonShowMode.InActiveTabPageHeader;
            this.xtraTabbedMdiManager1.MdiParent = this;
            // 
            // accordionControl1
            // 
            this.accordionControl1.Appearance.AccordionControl.Font = ((System.Drawing.Font)(resources.GetObject("accordionControl1.Appearance.AccordionControl.Font")));
            this.accordionControl1.Appearance.AccordionControl.Options.UseFont = true;
            resources.ApplyResources(this.accordionControl1, "accordionControl1");
            this.accordionControl1.ElementPositionOnExpanding = DevExpress.XtraBars.Navigation.ElementPositionOnExpanding.ScrollUp;
            this.accordionControl1.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.accordionControlElement1,
            this.accordionControlElement5,
            this.accordionControlElement8,
            this.accordionControlElement12});
            this.accordionControl1.ExpandElementMode = DevExpress.XtraBars.Navigation.ExpandElementMode.Single;
            this.accordionControl1.Name = "accordionControl1";
            this.accordionControl1.ShowFilterControl = DevExpress.XtraBars.Navigation.ShowFilterControl.Always;
            this.accordionControl1.ViewType = DevExpress.XtraBars.Navigation.AccordionControlViewType.HamburgerMenu;
            // 
            // accordionControlElement1
            // 
            this.accordionControlElement1.Appearance.Default.Font = ((System.Drawing.Font)(resources.GetObject("accordionControlElement1.Appearance.Default.Font")));
            this.accordionControlElement1.Appearance.Default.Options.UseFont = true;
            this.accordionControlElement1.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.btnMaterial,
            this.btnCustomer,
            this.btnSupplier});
            this.accordionControlElement1.Expanded = true;
            this.accordionControlElement1.Name = "accordionControlElement1";
            resources.ApplyResources(this.accordionControlElement1, "accordionControlElement1");
            // 
            // btnMaterial
            // 
            this.btnMaterial.Appearance.Default.Font = ((System.Drawing.Font)(resources.GetObject("btnMaterial.Appearance.Default.Font")));
            this.btnMaterial.Appearance.Default.Options.UseFont = true;
            this.btnMaterial.Name = "btnMaterial";
            this.btnMaterial.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            resources.ApplyResources(this.btnMaterial, "btnMaterial");
            this.btnMaterial.Click += new System.EventHandler(this.btnMaterial_Click);
            // 
            // btnCustomer
            // 
            this.btnCustomer.Appearance.Default.Font = ((System.Drawing.Font)(resources.GetObject("btnCustomer.Appearance.Default.Font")));
            this.btnCustomer.Appearance.Default.Options.UseFont = true;
            this.btnCustomer.Name = "btnCustomer";
            this.btnCustomer.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            resources.ApplyResources(this.btnCustomer, "btnCustomer");
            this.btnCustomer.Click += new System.EventHandler(this.btnCustomer_Click);
            // 
            // btnSupplier
            // 
            this.btnSupplier.Appearance.Default.Font = ((System.Drawing.Font)(resources.GetObject("btnSupplier.Appearance.Default.Font")));
            this.btnSupplier.Appearance.Default.Options.UseFont = true;
            this.btnSupplier.Name = "btnSupplier";
            this.btnSupplier.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            resources.ApplyResources(this.btnSupplier, "btnSupplier");
            this.btnSupplier.Click += new System.EventHandler(this.btnSupplier_Click);
            // 
            // accordionControlElement5
            // 
            this.accordionControlElement5.Appearance.Default.Font = ((System.Drawing.Font)(resources.GetObject("accordionControlElement5.Appearance.Default.Font")));
            this.accordionControlElement5.Appearance.Default.Options.UseFont = true;
            this.accordionControlElement5.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.btnInvoice,
            this.btnBill});
            this.accordionControlElement5.Expanded = true;
            this.accordionControlElement5.Name = "accordionControlElement5";
            resources.ApplyResources(this.accordionControlElement5, "accordionControlElement5");
            // 
            // btnInvoice
            // 
            this.btnInvoice.Appearance.Default.Font = ((System.Drawing.Font)(resources.GetObject("btnInvoice.Appearance.Default.Font")));
            this.btnInvoice.Appearance.Default.Options.UseFont = true;
            this.btnInvoice.Name = "btnInvoice";
            this.btnInvoice.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            resources.ApplyResources(this.btnInvoice, "btnInvoice");
            this.btnInvoice.Click += new System.EventHandler(this.btnInvoice_Click);
            // 
            // btnBill
            // 
            this.btnBill.Appearance.Default.Font = ((System.Drawing.Font)(resources.GetObject("btnBill.Appearance.Default.Font")));
            this.btnBill.Appearance.Default.Options.UseFont = true;
            this.btnBill.Name = "btnBill";
            this.btnBill.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            resources.ApplyResources(this.btnBill, "btnBill");
            this.btnBill.Click += new System.EventHandler(this.btnBill_Click);
            // 
            // accordionControlElement8
            // 
            this.accordionControlElement8.Appearance.Default.Font = ((System.Drawing.Font)(resources.GetObject("accordionControlElement8.Appearance.Default.Font")));
            this.accordionControlElement8.Appearance.Default.Options.UseFont = true;
            this.accordionControlElement8.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.btnStoreDaily,
            this.btnStoreRecivement,
            this.btnStorePayment});
            this.accordionControlElement8.Expanded = true;
            this.accordionControlElement8.Name = "accordionControlElement8";
            resources.ApplyResources(this.accordionControlElement8, "accordionControlElement8");
            // 
            // btnStoreDaily
            // 
            this.btnStoreDaily.Appearance.Default.Font = ((System.Drawing.Font)(resources.GetObject("btnStoreDaily.Appearance.Default.Font")));
            this.btnStoreDaily.Appearance.Default.Options.UseFont = true;
            this.btnStoreDaily.Name = "btnStoreDaily";
            this.btnStoreDaily.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            resources.ApplyResources(this.btnStoreDaily, "btnStoreDaily");
            this.btnStoreDaily.Click += new System.EventHandler(this.btnStoreDaily_Click);
            // 
            // btnStoreRecivement
            // 
            this.btnStoreRecivement.Appearance.Default.Font = ((System.Drawing.Font)(resources.GetObject("btnStoreRecivement.Appearance.Default.Font")));
            this.btnStoreRecivement.Appearance.Default.Options.UseFont = true;
            this.btnStoreRecivement.Name = "btnStoreRecivement";
            this.btnStoreRecivement.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            resources.ApplyResources(this.btnStoreRecivement, "btnStoreRecivement");
            this.btnStoreRecivement.Click += new System.EventHandler(this.btnStoreRecivement_Click);
            // 
            // btnStorePayment
            // 
            this.btnStorePayment.Appearance.Default.Font = ((System.Drawing.Font)(resources.GetObject("btnStorePayment.Appearance.Default.Font")));
            this.btnStorePayment.Appearance.Default.Options.UseFont = true;
            this.btnStorePayment.Name = "btnStorePayment";
            this.btnStorePayment.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            resources.ApplyResources(this.btnStorePayment, "btnStorePayment");
            this.btnStorePayment.Click += new System.EventHandler(this.btnStorePayment_Click);
            // 
            // accordionControlElement12
            // 
            this.accordionControlElement12.Appearance.Default.Font = ((System.Drawing.Font)(resources.GetObject("accordionControlElement12.Appearance.Default.Font")));
            this.accordionControlElement12.Appearance.Default.Options.UseFont = true;
            this.accordionControlElement12.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.btnBank,
            this.btnBankRecivement,
            this.btnBankPayment});
            this.accordionControlElement12.Expanded = true;
            this.accordionControlElement12.Name = "accordionControlElement12";
            resources.ApplyResources(this.accordionControlElement12, "accordionControlElement12");
            // 
            // btnBank
            // 
            this.btnBank.Appearance.Default.Font = ((System.Drawing.Font)(resources.GetObject("btnBank.Appearance.Default.Font")));
            this.btnBank.Appearance.Default.Options.UseFont = true;
            this.btnBank.Name = "btnBank";
            this.btnBank.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            resources.ApplyResources(this.btnBank, "btnBank");
            this.btnBank.Click += new System.EventHandler(this.btnBank_Click);
            // 
            // btnBankRecivement
            // 
            this.btnBankRecivement.Appearance.Default.Font = ((System.Drawing.Font)(resources.GetObject("btnBankRecivement.Appearance.Default.Font")));
            this.btnBankRecivement.Appearance.Default.Options.UseFont = true;
            this.btnBankRecivement.Name = "btnBankRecivement";
            this.btnBankRecivement.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            resources.ApplyResources(this.btnBankRecivement, "btnBankRecivement");
            this.btnBankRecivement.Click += new System.EventHandler(this.btnBankRecivement_Click);
            // 
            // btnBankPayment
            // 
            this.btnBankPayment.Appearance.Default.Font = ((System.Drawing.Font)(resources.GetObject("btnBankPayment.Appearance.Default.Font")));
            this.btnBankPayment.Appearance.Default.Options.UseFont = true;
            this.btnBankPayment.Name = "btnBankPayment";
            this.btnBankPayment.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            resources.ApplyResources(this.btnBankPayment, "btnBankPayment");
            this.btnBankPayment.Click += new System.EventHandler(this.btnBankPayment_Click);
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.accordionControl1);
            this.Controls.Add(this.ribbonStatusBar);
            this.Controls.Add(this.ribbon);
            this.IsMdiContainer = true;
            this.Name = "MainForm";
            this.Ribbon = this.ribbon;
            this.StatusBar = this.ribbonStatusBar;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabbedMdiManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.accordionControl1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbon;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar;
        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel1;
        public DevExpress.XtraTabbedMdi.XtraTabbedMdiManager xtraTabbedMdiManager1;
        private DevExpress.XtraBars.Navigation.AccordionControl accordionControl1;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElement1;
        private DevExpress.XtraBars.Navigation.AccordionControlElement btnMaterial;
        private DevExpress.XtraBars.Navigation.AccordionControlElement btnCustomer;
        private DevExpress.XtraBars.Navigation.AccordionControlElement btnSupplier;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElement5;
        private DevExpress.XtraBars.Navigation.AccordionControlElement btnInvoice;
        private DevExpress.XtraBars.Navigation.AccordionControlElement btnBill;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElement8;
        private DevExpress.XtraBars.Navigation.AccordionControlElement btnStoreDaily;
        private DevExpress.XtraBars.Navigation.AccordionControlElement btnStoreRecivement;
        private DevExpress.XtraBars.Navigation.AccordionControlElement btnStorePayment;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElement12;
        private DevExpress.XtraBars.Navigation.AccordionControlElement btnBank;
        private DevExpress.XtraBars.Navigation.AccordionControlElement btnBankRecivement;
        private DevExpress.XtraBars.Navigation.AccordionControlElement btnBankPayment;
    }
}