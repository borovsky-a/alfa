using prototype.UserEritor.Desktop.Views;

namespace prototype.UserEritor.Desktop
{
    public class MainViewModel : BaseViewModel
    {

        public MainViewModel(UserListViewModel listViewModel, ThemeSelectorViewModel themeSelectorViewModel)
        {
            UserList = listViewModel;
            ThemeSelector = themeSelectorViewModel;
        }

        public UserListViewModel UserList { get; }

        public ThemeSelectorViewModel ThemeSelector { get; }
    }
}
