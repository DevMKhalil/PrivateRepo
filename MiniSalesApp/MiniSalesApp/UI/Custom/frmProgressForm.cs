using DevExpress.XtraWaitForm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiniSalesApp.UI.Custom
{
    public partial class frmProgressForm : WaitForm
    {
        private Func<Task> action;

        public frmProgressForm()
        {
            InitializeComponent();
            this.progressPanel1.AutoHeight = true;
        }

        public frmProgressForm(Func<Task> action) : this()
        {
            this.action = action;
        }

        #region Overrides

        public override void SetCaption(string caption)
        {
            base.SetCaption(caption);
            this.progressPanel1.Caption = caption;
        }
        public override void SetDescription(string description)
        {
            base.SetDescription(description);
            this.progressPanel1.Description = description;
        }
        public override void ProcessCommand(Enum cmd, object arg)
        {
            base.ProcessCommand(cmd, arg);
        }

        public enum WaitFormCommand
        {
        }

        #endregion

        private async void frmProgressForm_Shown(object sender, EventArgs e)
        {
            if (this.action != null)
                await action();

            Close();
        }

        internal void SetParamitraizedAction<T>(Func<T, Task> paramitrizedActionWithResult, T args)
        {
            action = async () =>
            {
                await paramitrizedActionWithResult(args);
            };
        }
    }
}