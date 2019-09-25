using System.ComponentModel;
using System.ComponentModel.Design;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Controls;
using MapMonitor.Annotations;
using MapMonitor.Logic;
using MapMonitor.Models;

namespace MapMonitor.ViewModels
{
    public class SettingsVm : INotifyPropertyChanged
    {
        private string _server;
        private string _login;
        private string _password;
        private RelayCommand _saveCommand;

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

        public RelayCommand SaveCommand => _saveCommand ?? (_saveCommand = new RelayCommand(obj=>
        {
            _password = ((PasswordBox) obj).Password;
            DialogResult = true;
        }));

        private bool? _dialogResult;

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
                    Password = Password
                };
                return config;
            }
        }
    }
}