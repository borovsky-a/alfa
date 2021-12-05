using System.Threading.Tasks;

namespace prototype.UserEritor.Desktop.Service
{
    public interface IThemeService
    {
        Task<IResponse<ApplicationThemeInfo>> GetAvailableThemes();
        Task<IResponse<ThemeInfo>> SetTheme(string name);
    }
}