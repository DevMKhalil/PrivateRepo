using MediatR;
//using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MiniSalesApp.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiniSalesApp
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            var services = new ServiceCollection();
            ConfigureServices(services);

            System.Windows.Forms.Application.SetHighDpiMode(System.Windows.Forms.HighDpiMode.SystemAware);

            //System.Windows.Forms.Application.Run(new Form1(GetService<IMediator>()));

            MiniSalesApp.Reports.Program.ConnectionString = Properties.Settings.Default.TempString;

            DevExpress.XtraEditors.WindowsFormsSettings.AllowRibbonFormGlass = DevExpress.Utils.DefaultBoolean.True;
            DevExpress.XtraEditors.WindowsFormsSettings.RightToLeft = DevExpress.Utils.DefaultBoolean.True;
            DevExpress.XtraEditors.WindowsFormsSettings.RightToLeftLayout = DevExpress.Utils.DefaultBoolean.True;
            DevExpress.UserSkins.BonusSkins.Register();
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);

            System.Windows.Forms.Application.Run(new MainForm());
        }

        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMiniSalesAppContext();

            Program.ServiceProvider = services.BuildServiceProvider();
        }

        public static DialogResult DisplayMessage(string Message, MessageBoxButtons buttons, MessageBoxIcon Icon)
        {
            return MessageBox.Show(Message, "", buttons, Icon);
        }

        public static T GetService<T>() where T : class
        {
            return ServiceProvider.GetService(typeof(T)) as T;
        }

        public static IServiceProvider ServiceProvider { get; set; }
    }
}
