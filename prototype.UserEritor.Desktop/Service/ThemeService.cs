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
        private static ThemeInfo _currentTheme;

        private static Lazy<ReadOnlyCollection<ThemeInfo>> _availableThemeCache;

        static ThemeService()
        {
            var greenThemeInfo  = new ThemeInfo("green_theme", "Зеленая тема", new Uri("/Themes/GreenTheme.xaml", UriKind.Relative));
            var whiteThemeInfo = new ThemeInfo("white_theme", "Белая тема", new Uri("/Themes/WhiteTheme.xaml", UriKind.Relative));

            
            _currentTheme = greenThemeInfo;

            _availableThemeCache = new Lazy<ReadOnlyCollection<ThemeInfo>>(() =>
            {
                var list = new List<ThemeInfo>
                {
                    greenThemeInfo,
                    whiteThemeInfo
                };

                var result = new ReadOnlyCollection<ThemeInfo>(list);
                return result;
            });
        }

        public ThemeService()
        {
            SetTheme(_currentTheme.Name).GetAwaiter().GetResult();
        }
        public async Task<IResponse<ApplicationThemeInfo>> GetAvailableThemes()
        {
            var info = new ApplicationThemeInfo(_availableThemeCache.Value, _currentTheme);
            return await Task.FromResult(new Response<ApplicationThemeInfo> { Value = info });
        }        

        public async Task<IResponse<ThemeInfo>> SetTheme(string name)
        {         
            var themeInfo = _availableThemeCache.Value.FirstOrDefault(o => o.Name == name);
            if(themeInfo == null)
            {
                return new Response<ThemeInfo> { IsValid = false, Description = $"Не найдена тема с ключем {name}" };
            }
            _currentTheme = themeInfo;
            ThemeSelector.SetCurrentThemeDictionary(themeInfo.Path);
            return await Task.FromResult(new Response<ThemeInfo> { Value = themeInfo });
        }
    }
}
