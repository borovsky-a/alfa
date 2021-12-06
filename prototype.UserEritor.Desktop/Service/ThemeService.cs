using prototype.UserEritor.Desktop.Themes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace prototype.UserEritor.Desktop.Service
{
    public class ThemeService : IThemeService
    {
        private static ThemeInfoDefinition _currentTheme;

        private static Lazy<ReadOnlyCollection<ThemeInfoDefinition>> _availableThemeCache;

        static ThemeService()
        {
            var greenThemeInfo  = new ThemeInfoDefinition("green_theme", "Зеленая тема", new Uri("/Themes/GreenTheme.xaml", UriKind.Relative));
            var whiteThemeInfo = new ThemeInfoDefinition("white_theme", "Белая тема", new Uri("/Themes/WhiteTheme.xaml", UriKind.Relative));
            var redThemeInfo = new ThemeInfoDefinition("red_theme", "Красная тема", new Uri("/Themes/RedTheme.xaml", UriKind.Relative));

            _currentTheme = whiteThemeInfo;

            _availableThemeCache = new Lazy<ReadOnlyCollection<ThemeInfoDefinition>>(() =>
            {
                var list = new List<ThemeInfoDefinition>
                {
                    whiteThemeInfo,
                    greenThemeInfo,
                    redThemeInfo
                };

                var result = new ReadOnlyCollection<ThemeInfoDefinition>(list);
                return result;
            });
        }

        public ThemeService()
        {
            SetTheme(_currentTheme.Name).GetAwaiter().GetResult();
        }
        public async Task<IResponse<ThemeInfo>> GetAvailableThemes()
        {
            var info = new ThemeInfo(_availableThemeCache.Value, _currentTheme);
            return await Task.FromResult(new Response<ThemeInfo> { Value = info });
        }        

        public async Task<IResponse<ThemeInfoDefinition>> SetTheme(string name)
        {         
            var themeInfo = _availableThemeCache.Value.FirstOrDefault(o => o.Name == name);
            if(themeInfo == null)
            {
                return new Response<ThemeInfoDefinition> { IsValid = false, Description = $"Не найдена тема с ключем {name}" };
            }
            _currentTheme = themeInfo;
            ThemeSelector.SetCurrentThemeDictionary(themeInfo.Path);
            return await Task.FromResult(new Response<ThemeInfoDefinition> { Value = themeInfo });
        }
    }
}
