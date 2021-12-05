using prototype.UserEritor.Desktop.Data;
using prototype.UserEritor.Desktop.Service;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace prototype.UserEritor.Desktop.Views.Dialogs
{
    public class CreateUserDialogViewModel : BaseViewModel, IDataErrorInfo
    {
        private string _emailAddress;
        private string _firstName;
        private string _lastName;
        private string _middleName;
        private string _phoneNumber;
        private CreateUserDialog _dialog;
        private string _message;
        private readonly IUserService _userService;

        public CreateUserDialogViewModel(IUserService userService)
        {
            _userService = userService;
        }

        public ICommand SaveCommand =>
            CreateCommand(SaveCommandExecute, SaveCommandCanExecute);

        public User Result { get; private set; }

        public string Message
        {
            get { return _message; }
            set
            {
                if(_message != value)
                {
                    _message = value;
                    OnPropertyChanged();
                }
            }
        }

        public string this[string columnName]
        {
            get
            {
                var result = string.Empty;
                switch (columnName)
                {
                    case nameof(FirstName):
                        {
                            if (string.IsNullOrEmpty(FirstName))
                            {
                                result = $"Не указано имя пользователя";
                            }
                            break;
                        }
                    case nameof(LastName):
                        {
                            if (string.IsNullOrEmpty(LastName))
                            {
                                result = $"Не указана фамилия пользователя пользователя";
                            }
                            break;
                        }
                    case nameof(EmailAddress):
                        {
                            if (string.IsNullOrEmpty(EmailAddress))
                            {
                                result = $"Не указан адрес электронной почты пользователя";
                            }
                            break;
                        }
                    default:
                        break;
                }
                return  result;
            }
        }

        public bool DialogResult { get; private set; }


        public string FirstName
        {
            get { return _firstName; }
            set
            {
                if(_firstName != value)
                {
                    _firstName = value;
                    OnPropertyChanged();
                }
            }
        }
        public string LastName
        {
            get { return _lastName; }
            set
            {
                if (_lastName != value)
                {
                    _lastName = value;
                    OnPropertyChanged();
                }
            }
        }
        public string MiddleName
        {
            get { return _middleName; }
            set
            {
                if (_middleName != value)
                {
                    _middleName = value;
                    OnPropertyChanged();
                }
            }
        }
        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set
            {
                if (_phoneNumber != value)
                {
                    _phoneNumber = value;
                    OnPropertyChanged();
                }
            }
        }
        public string EmailAddress
        {
            get { return _emailAddress; }
            set
            {
                if (_emailAddress != value)
                {
                    _emailAddress = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Error { get; set; }

        public void ShowDialog()
        {
            _dialog = new CreateUserDialog { DataContext = this };
            _dialog.ShowDialog();
        }

        private void SaveCommandExecute(object parameter)
        {
            if (SaveCommandCanExecute(parameter))
            {
                var user = new User
                {
                    FirstName = FirstName,
                    LastName = LastName,
                    MiddleName = MiddleName,
                    EmailAddress = EmailAddress,
                    PhoneNumber = PhoneNumber
                };
                _userService.CreateUserAsync(user).ContinueWith(o => 
                {
                    if (o.IsCanceled)
                    {
                        //TODO Canceled impl
                    }
                    else if (o.IsFaulted)
                    {
                        //TODO Faulted impl
                    }
                    else
                    {
                        
                        DialogResult = o.Result.IsValid;
                        Result = o.Result.Value;
                        Message = o.Result.Description;

                        if(DialogResult == true)
                        {
                            _dialog.Close();
                        }
                        
                    }
                }, 
                TaskContinuationOptions.ExecuteSynchronously);              
            }
        }

        private bool SaveCommandCanExecute(object parameter)
        {
            if (ProcessFlag)
                return false;


            return string.IsNullOrEmpty(this[nameof(FirstName)])
                && string.IsNullOrEmpty(this[nameof(LastName)])
                && string.IsNullOrEmpty(this[nameof(EmailAddress)]);
        }
    }
}
