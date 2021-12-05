using System.Threading.Tasks;
using prototype.UserEritor.Desktop.Data;

namespace prototype.UserEritor.Desktop.Service
{
    public interface IUserService
    {
        Task<IResponse<User>> CreateUserAsync(User user);
        Task<IResponse<User>> DeleteUserAsync(int id);
        Task<IResponse<User>> GetUserByIdAsync(int id);
        Task<IPagingResponse<User>> GetPagingListAsync(UserListRequest request);
    }
}