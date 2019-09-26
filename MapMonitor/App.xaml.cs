using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using MapMonitor.ViewModels;
using MapMonitor.Views;

namespace MapMonitor
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override async void OnStartup(StartupEventArgs e)
        {
            var mainWindow = new MainWindow();
            var mainWindowVm = new MainWindowVm(mainWindow);
            mainWindowVm.Show();
        }
    }
}
