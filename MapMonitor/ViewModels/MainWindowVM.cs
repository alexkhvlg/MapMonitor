using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using MapMonitor.Annotations;
using MapMonitor.Logic;
using MapMonitor.Tools;
using MapMonitor.Views;

namespace MapMonitor.ViewModels
{
    public class MainWindowVm : INotifyPropertyChanged
    {
        private readonly MainWindow _mainWindow;
        private RepositoryDb _repositoryDb;

        public MainWindowVm(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
            _mainWindow.Loaded += async (sender, args) =>
            {
                await Loaded(sender, args);
            };
            _mainWindow.Closing += (sender, args) =>
            {
                Closing(); 
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async Task Loaded(object sender, RoutedEventArgs args)
        {
            var config = RegConfig.Load();
            if (config == null)
            {
                // ReSharper disable once ExpressionIsAlwaysNull
                var settingsVm = new SettingsVm(config);
                var settingsWindow = new SettingsWindow(settingsVm);
                if (settingsWindow.ShowDialog() == false)
                {
                    _mainWindow.Close();
                }
                else
                {
                    config = settingsVm.SqlConfig;
                    RegConfig.Save(config);
                }
            }

            _repositoryDb = new RepositoryDb();
            _repositoryDb.Connect(config);
        }

        public void Closing()
        {
            // close all active threads
            Environment.Exit(0);
        }

        public void Show()
        {
            _mainWindow.Show();
        }
    }
}