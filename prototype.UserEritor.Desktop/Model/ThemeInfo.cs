using System.Collections.Generic;

namespace prototype.UserEritor.Desktop
{
    public class ThemeInfo
    {
        public ThemeInfo(IEnumerable<ThemeInfoDefinition> availableThemes, ThemeInfoDefinition currentTheme)
        {
            AvailableThemes = availableThemes;
            CurrentTheme = currentTheme;
        }
        public IEnumerable<ThemeInfoDefinition> AvailableThemes { get; }

        public ThemeInfoDefinition CurrentTheme { get; }
    }
}
