using System.Threading.Tasks;
using prototype.UserEritor.Desktop.Data;

namespace prototype.UserEritor.Desktop.Service
{
    public interface IUserSettingsService
    {
        Task<IResponse<TableSettings>> GetUserSettingsAsync();
        Task<IResponse<TableSettings>> SaveUserSettingsAsync(TableSettings settings);
    }
}