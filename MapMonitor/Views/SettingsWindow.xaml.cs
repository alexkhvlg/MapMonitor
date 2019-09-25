using System.Windows;
using MapMonitor.ViewModels;

namespace MapMonitor.Views
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public SettingsWindow(SettingsVm settingsVm)
        {
            InitializeComponent();
            DataContext = settingsVm;
        }
    }
}
