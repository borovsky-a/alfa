using System.Collections.Generic;

namespace prototype.UserEritor.Desktop
{
    public class ApplicationThemeInfo
    {
        public ApplicationThemeInfo(IEnumerable<ThemeInfo> availableThemes, ThemeInfo currentTheme)
        {
            AvailableThemes = availableThemes;
            CurrentTheme = currentTheme;
        }
        public IEnumerable<ThemeInfo> AvailableThemes { get; }

        public ThemeInfo CurrentTheme { get; }
    }
}
