using System.Threading.Tasks;
using prototype.UserEritor.Desktop.Data;

namespace prototype.UserEritor.Desktop.Service
{
    public interface IUserSettingsService
    {
        Task<IResponse<UserTableSettings>> GetUserSettingsAsync();
        Task<IResponse<UserTableSettings>> SaveUserSettingsAsync(UserTableSettings settings);
    }
}