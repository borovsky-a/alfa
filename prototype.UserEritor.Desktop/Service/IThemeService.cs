using System.Threading.Tasks;

namespace prototype.UserEritor.Desktop.Service
{
    public interface IThemeService
    {
        Task<IResponse<ThemeInfo>> GetAvailableThemes();
        Task<IResponse<ThemeInfoDefinition>> SetTheme(string name);
    }
}