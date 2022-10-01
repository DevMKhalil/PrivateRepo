using DevExpress.XtraBars;
using MediatR;
using MiniSalesApp.UI.Bank;
using MiniSalesApp.UI.BankRecivement;
using MiniSalesApp.UI.BankPayment;
using MiniSalesApp.UI.Customer;
using MiniSalesApp.UI.Invoice;
using MiniSalesApp.UI.Material;
using MiniSalesApp.UI.StoreDaily;
using MiniSalesApp.UI.StorePayment;
using MiniSalesApp.UI.StoreRecivement;
using MiniSalesApp.UI.Supplier;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MiniSalesApp.UI.Bill;

namespace MiniSalesApp.UI
{
    public partial class MainForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        frmMaterialForm materialForm;
        frmCustomerForm customerForm;
        frmSupplierForm supplierForm;
        frmInvoiceForm invoiceForm;
        frmStoreDailyFormNew storeDailyForm;
        frmStoreRecivementFormNew storeRecivementForm;
        frmBankForm bankForm;
        frmStorePaymentForm StorePaymentForm;
        frmBankRecivementForm bankRecivementForm;
        frmBankPaymentForm bankPaymentForm;
        frmBillForm billForm;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void btnMaterial_Click(object sender, EventArgs e)
        {
            try
            {
                if (materialForm == null)
                {
                    materialForm = new frmMaterialForm(Program.GetService<IMediator>());
                    materialForm.MdiParent = this;
                    materialForm.Disposed += materialForm_Disposed;
                }
                materialForm.Show();
                materialForm.BringToFront();
            }
            catch (Exception ex)
            {
                Program.DisplayMessage(ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void materialForm_Disposed(object sender, EventArgs e)
        {
            materialForm = null;
        }

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                if (customerForm == null)
                {
                    customerForm = new frmCustomerForm(Program.GetService<IMediator>());
                    customerForm.MdiParent = this;
                    customerForm.Disposed += customerForm_Disposed;
                }
                customerForm.Show();
                customerForm.BringToFront();
            }
            catch (Exception ex)
            {
                Program.DisplayMessage(ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void customerForm_Disposed(object sender, EventArgs e)
        {
            customerForm = null;
        }

        private void btnSupplier_Click(object sender, EventArgs e)
        {
            try
            {
                if (supplierForm == null)
                {
                    supplierForm = new frmSupplierForm(Program.GetService<IMediator>());
                    supplierForm.MdiParent = this;
                    supplierForm.Disposed += supplierForm_Disposed;
                }
                supplierForm.Show();
                supplierForm.BringToFront();
            }
            catch (Exception ex)
            {
                Program.DisplayMessage(ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void supplierForm_Disposed(object sender, EventArgs e)
        {
            supplierForm = null;
        }

        private void btnInvoice_Click(object sender, EventArgs e)
        {
            try
            {
                if (invoiceForm == null)
                {
                    invoiceForm = new frmInvoiceForm(Program.GetService<IMediator>());
                    invoiceForm.MdiParent = this;
                    invoiceForm.Disposed += InvoiceForm_Disposed;
                }
                invoiceForm.Show();
                invoiceForm.BringToFront();
            }
            catch (Exception ex)
            {
                Program.DisplayMessage(ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InvoiceForm_Disposed(object sender, EventArgs e)
        {
            invoiceForm = null;
        }

        private void btnStoreDaily_Click(object sender, EventArgs e)
        {
            try
            {
                if (storeDailyForm == null)
                {
                    storeDailyForm = new frmStoreDailyFormNew(Program.GetService<IMediator>());
                    storeDailyForm.MdiParent = this;
                    storeDailyForm.Disposed += storeDaily_Disposed;
                }
                storeDailyForm.Show();
                storeDailyForm.BringToFront();
            }
            catch (Exception ex)
            {
                Program.DisplayMessage(ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void storeDaily_Disposed(object sender, EventArgs e)
        {
            storeDailyForm = null;
        }

        private void btnStoreRecivement_Click(object sender, EventArgs e)
        {
            try
            {
                if (storeRecivementForm == null)
                {
                    storeRecivementForm = new frmStoreRecivementFormNew(Program.GetService<IMediator>());
                    storeRecivementForm.MdiParent = this;
                    storeRecivementForm.Disposed += storeRecivement_Disposed;
                }
                storeRecivementForm.Show();
                storeRecivementForm.BringToFront();
            }
            catch (Exception ex)
            {
                Program.DisplayMessage(ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void storeRecivement_Disposed(object sender, EventArgs e)
        {
            storeRecivementForm = null;
        }

        private void btnBank_Click(object sender, EventArgs e)
        {
            try
            {
                if (bankForm == null)
                {
                    bankForm = new frmBankForm(Program.GetService<IMediator>());
                    bankForm.MdiParent = this;
                    bankForm.Disposed += BankForm_Disposed; ;
                }
                bankForm.Show();
                bankForm.BringToFront();
            }
            catch (Exception ex)
            {
                Program.DisplayMessage(ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BankForm_Disposed(object sender, EventArgs e)
        {
            bankForm = null;
        }

        private void btnStorePayment_Click(object sender, EventArgs e)
        {
            try
            {
                if (StorePaymentForm == null)
                {
                    StorePaymentForm = new frmStorePaymentForm(Program.GetService<IMediator>());
                    StorePaymentForm.MdiParent = this;
                    StorePaymentForm.Disposed += storePayment_Disposed;
                }
                StorePaymentForm.Show();
                StorePaymentForm.BringToFront();
            }
            catch (Exception ex)
            {
                Program.DisplayMessage(ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void storePayment_Disposed(object sender, EventArgs e)
        {
            StorePaymentForm = null;
        }

        private void btnBankRecivement_Click(object sender, EventArgs e)
        {
            try
            {
                if (bankRecivementForm == null)
                {
                    bankRecivementForm = new frmBankRecivementForm(Program.GetService<IMediator>());
                    bankRecivementForm.MdiParent = this;
                    bankRecivementForm.Disposed += bankRecivement_Disposed;
                }
                bankRecivementForm.Show();
                bankRecivementForm.BringToFront();
            }
            catch (Exception ex)
            {
                Program.DisplayMessage(ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bankRecivement_Disposed(object sender, EventArgs e)
        {
            bankRecivementForm = null;
        }

        private void btnBankPayment_Click(object sender, EventArgs e)
        {
            try
            {
                if (bankPaymentForm == null)
                {
                    bankPaymentForm = new frmBankPaymentForm(Program.GetService<IMediator>());
                    bankPaymentForm.MdiParent = this;
                    bankPaymentForm.Disposed += bankPayment_Disposed;
                }
                bankPaymentForm.Show();
                bankPaymentForm.BringToFront();
            }
            catch (Exception ex)
            {
                Program.DisplayMessage(ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bankPayment_Disposed(object sender, EventArgs e)
        {
            bankPaymentForm = null;
        }

        private void btnBill_Click(object sender, EventArgs e)
        {
            try
            {
                if (billForm == null)
                {
                    billForm = new frmBillForm(Program.GetService<IMediator>());
                    billForm.MdiParent = this;
                    billForm.Disposed += billForm_Disposed;
                }
                billForm.Show();
                billForm.BringToFront();
            }
            catch (Exception ex)
            {
                Program.DisplayMessage(ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void billForm_Disposed(object sender, EventArgs e)
        {
            billForm = null;
        }
    }
}