using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Effects;
using System.Windows.Threading;

namespace MapMonitor.Tools
{
    public class SplashWindow
    {
        private static volatile Window _window;
        private static string _text;

        private static void ShowWindow()
        {
            Window owner = null;
            if (Application.Current?.MainWindow != null)
            {
                owner = Application.Current.MainWindow;
            }

            //if (_window == null)
            {
                var textBlock = new TextBlock
                {
                    Text = _text,
                    TextAlignment = TextAlignment.Center
                };
                var grid = new Grid
                {
                    HorizontalAlignment = HorizontalAlignment.Center, 
                    VerticalAlignment = VerticalAlignment.Center
                };
                grid.Children.Add(textBlock);
                _window = new Window
                {
                    Owner = owner,
                    Content = grid,
                    Title = _text,
                    Width = 200,
                    Height = 100,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    BorderThickness = new Thickness(3),
                    ResizeMode = ResizeMode.NoResize,
                    ShowInTaskbar = false,
                    WindowStyle = WindowStyle.None,
                    WindowStartupLocation = owner == null ? WindowStartupLocation.CenterScreen : WindowStartupLocation.CenterOwner
                };
                _window.Loaded += (sender, args) =>
                {
                    if (Application.Current?.MainWindow != null && Application.Current.MainWindow != _window)
                    {
                        Application.Current.MainWindow.Opacity = 0.5;
                        Application.Current.MainWindow.Effect = new BlurEffect();
                    }
                };
                _window.Closed += (sender, args) =>
                {
                    if (Application.Current?.MainWindow != null && Application.Current.MainWindow != _window)
                    {
                        Application.Current.MainWindow.Opacity = 1;
                        Application.Current.MainWindow.Effect = null;
                    }
                };
            }
            _window.ShowDialog();
        }

        public static T RunSync<T>(Func<Task<T>> task, string text = "Загрузка", int ms = 100) where T : new()
        {
            _text = text;

            var t = new Task<T>(() => task().Result);
            t.Start();
            t.ContinueWith(taskResult =>
            {
                Application.Current.Dispatcher?.Invoke(() =>
                {
                    _window?.Close();
                }, DispatcherPriority.Background);
            });

            if (!t.Wait(ms))
            {
                ShowWindow();
            }

            return t.Result;
        }

        public static async Task<T> RunAsync<T>(Func<Task<T>> task, string text = "Загрузка", int ms = 100)
        {
            T result;
            _text = text;
            try
            {
                var dispatcherTimer = new DispatcherTimer();
                dispatcherTimer.Tick += (sender, args) =>
                {
                    dispatcherTimer.Stop();
                    ShowWindow();
                };
                dispatcherTimer.Interval = new TimeSpan(0,0,0, 0, ms);
                dispatcherTimer.Start();
                result = await task();
            }
            finally
            {
                _window?.Close();
            }

            return result;
        }

        public static void RunSync(Func<Task> task, string text = "Загрузка", int ms = 100)
        {
            RunSync(async () =>
            {
                await task();
                return 0;
            }, text, ms);
        }

        public static async Task RunAsync(Func<Task> task, string text = "Загрузка", int ms = 100)
        {
            await RunAsync(async () =>
            {
                await task();
                return 0;
            }, text, ms);
        }
    }
}