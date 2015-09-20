using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Xb.Simulador;
using Xb.Simulador.View;
using Xb.Simulador.Viewmodel;

namespace Simulador
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            string culture = ConfigurationManager.AppSettings["Culture"];
            if (!string.IsNullOrEmpty(culture))
            {
                CultureInfo ci = new CultureInfo(culture);
                Thread.CurrentThread.CurrentCulture = ci;
                Thread.CurrentThread.CurrentUICulture = ci;
            }
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            MainWindow window = new MainWindow();

            MainWindowViewModel viewModel = new MainWindowViewModel();

            window.DataContext = viewModel;

            window.Show();
        }
    }
}
