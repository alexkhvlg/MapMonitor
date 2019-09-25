using System;
using System.ComponentModel;
using System.Windows;
using MapMonitor.Models;
using MapMonitor.Tools;
using MapMonitor.ViewModels;

namespace MapMonitor.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            var config = RegConfig.Load();
            if (config == null)
            {
                // ReSharper disable once ExpressionIsAlwaysNull
                var settingsVm = new SettingsVm(config);
                var settingsWindow = new SettingsWindow(settingsVm);
                if (settingsWindow.ShowDialog() == false)
                {
                    Close();
                }
                else
                {
                    config = settingsVm.SqlConfig;
                    RegConfig.Save(config);
                }
            }
        }

        private void MainWindow_OnClosing(object sender, CancelEventArgs e)
        {
            // close all active threads
            Environment.Exit(0);
        }
    }
}
