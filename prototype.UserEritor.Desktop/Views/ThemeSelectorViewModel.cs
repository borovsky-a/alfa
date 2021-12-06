using prototype.UserEritor.Desktop.Service;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

namespace prototype.UserEritor.Desktop.Views
{
    public class ThemeSelectorViewModel : BaseViewModel
    {
        private readonly IThemeService _themeService;
        private ThemeInfoDefinition _selectedTheme;
        private ObservableCollection<ThemeInfoDefinition> _themes;

        public ThemeSelectorViewModel(IThemeService themeService)
        {
            _themeService = themeService;
            Initialize();
        }

        public ObservableCollection<ThemeInfoDefinition> Themes
        {
            get { return _themes; }
            set
            {
                if(_themes != value)
                {
                    _themes = value;
                    OnPropertyChanged();
                }
            }
        }

        public ThemeInfoDefinition SelectedTheme
        {
            get { return _selectedTheme; }
            set
            {
                if(_selectedTheme != value)
                {
                    _selectedTheme = value;
                    OnPropertyChanged();
                }
            }
        }

        private void Initialize()
        {
            var currentThemeTask = _themeService.GetAvailableThemes().GetAwaiter().GetResult();
            SelectedTheme = currentThemeTask.Value.CurrentTheme;
            Themes = new ObservableCollection<ThemeInfoDefinition>( currentThemeTask.Value.AvailableThemes);         
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if(propertyName == nameof(SelectedTheme))
            {
                _themeService.SetTheme(SelectedTheme.Name).GetAwaiter().GetResult();
            }
        }
    }
}
