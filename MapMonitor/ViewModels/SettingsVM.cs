using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using MapMonitor.Annotations;
using MapMonitor.Models;
using MapMonitor.Tools;

namespace MapMonitor.ViewModels
{
    public class SettingsVm : INotifyPropertyChanged
    {
        private string _server;
        private string _login;
        private string _password;
        private RelayCommand _saveCommand;
        private bool? _dialogResult;
        private string _database;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Server
        {
            get => _server;
            set
            {
                if (_server != value)
                {
                    _server = value;
                    OnPropertyChanged(nameof(Server));
                }
            }
        }

        public string Login
        {
            get => _login;
            set
            {
                if (_login != value)
                {
                    _login = value;
                    OnPropertyChanged(nameof(Login));
                }
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged(nameof(Password));
                }
            }
        }

        public string Database
        {
            get => _database;
            set
            {
                _database = value;
                OnPropertyChanged(nameof(Database));
            }
        }

        public RelayCommand SaveCommand => _saveCommand ?? (_saveCommand = new RelayCommand(obj=>
        {
            _password = ((PasswordBox) obj).Password;
            DialogResult = true;
        }));

        public bool? DialogResult
        {
            get => _dialogResult;
            set
            {
                _dialogResult = value;
                OnPropertyChanged(nameof(DialogResult));
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public SettingsVm(SqlConfig sqlConfig)
        {
            if (sqlConfig != null)
            {
                Server = sqlConfig.Server;
                Login = sqlConfig.Login;
                Password = sqlConfig.Password;
                Database = sqlConfig.Database;
            }
        }
        public SqlConfig SqlConfig
        {
            get
            {
                var config = new SqlConfig
                {
                    Server = Server,
                    Login = Login,
                    Password = Password,
                    Database = Database
                };
                return config;
            }
        }
    }
}