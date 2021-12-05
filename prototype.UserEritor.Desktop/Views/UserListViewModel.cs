using prototype.UserEritor.Desktop.Data;
using prototype.UserEritor.Desktop.Service;
using prototype.UserEritor.Desktop.Views.Dialogs;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace prototype.UserEritor.Desktop.Views
{
    public class UserListViewModel : TableViewModel<User>
    {
        private readonly IUserSettingsService _userSettingsService;
        private readonly IUserService _userService;
        private string _filter;

        public UserListViewModel()
        {
            _userService = new UserService();
            _userSettingsService = new UserSettingsService();
            Initialize();
        }
        public override ICommand RefreshCommand => 
            CreateCommand(RefreshCommandExecute, RefreshCommandCanExecute);

        public override ICommand DeleteRecordCommand =>
            CreateCommand(DeleteCommandExecute, DeleteCommandCanExecute);

        public override ICommand CreateRecordCommand =>
            CreateCommand(CreateCommandExecute, CreateCommandCanExecute);

        public string Filter
        {
            get { return _filter; }
            set
            {
                if (_filter != value)
                {
                    _filter = value;
                    OnPropertyChanged();
                }
            }
        }


        protected void Initialize()
        {
            ProcessFlag = true;
            _userSettingsService.GetUserSettingsAsync().ContinueWith(o => 
            {
                if(!o.IsCanceled && !o.IsFaulted && o.Result.IsValid)
                {
                    ApplySettings(o.Result.Value);
                    RefreshCommandExecute(null);
                }
                else
                {
                    //process Faulted or Canceled
                }
            }, TaskContinuationOptions.ExecuteSynchronously);          
        }

        private void RefreshCommandExecute(object parameter)
        {
            int.TryParse(parameter?.ToString(), out var pageIndex);
            ProcessFlag = true;
            _userService.GetPagingListAsync(new UserListRequest { PageIndex = pageIndex, NavsCount = 5, PageSize = 50, Filter = Filter }).ContinueWith(o =>
            {                
                ApplyResponse(o.Result);
            },
            TaskContinuationOptions.ExecuteSynchronously);
        }
        private bool RefreshCommandCanExecute(object parameter)
        {
            return !ProcessFlag;
        }

        private void CreateCommandExecute(object parameter)
        {
            var createUserDialog =
                new CreateUserDialogViewModel(_userService);

            createUserDialog.ShowDialog();
            if (createUserDialog.DialogResult == true)
            {
                Table.Rows.Insert(0, createUserDialog.Result);
            }
            if (!string.IsNullOrEmpty(createUserDialog.Message))
            {
                LastResult = createUserDialog.Message;
                IsError = !createUserDialog.DialogResult;
            }         
        }
        private bool CreateCommandCanExecute(object parameter)
        {
            return !ProcessFlag;
        }
        private void DeleteCommandExecute(object parameter)
        {
            if (SelectedItem != null)
            {
                var id = SelectedItem.Id;
                ProcessFlag = true;
                var deleteTask = _userService.DeleteUserAsync(SelectedItem.Id).GetAwaiter().GetResult();
                if (deleteTask.IsValid)
                {
                    var item = Table.Rows.FirstOrDefault(o => o.Id == id);
                    if(item != null)
                    {
                        Table.Rows.Remove(item);
                    }                  
                }
                LastResult = deleteTask.GetMessage();
                ProcessFlag = false;
                IsError = !deleteTask.IsValid;
            }
        }
        private bool DeleteCommandCanExecute(object parameter)
        {
            return !ProcessFlag && SelectedItem != null;
        }
    }   
}