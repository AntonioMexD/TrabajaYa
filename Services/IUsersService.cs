using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrabajaYa.Models;

namespace TrabajaYaAPI.Services
{
    public interface IUsersService
    {
        Task<IEnumerable<UserModel>> GetUsersAsync(string orderBy, bool showPublish);
        Task<UserModel> GetUserAsync(int userId, bool showPublish);
        Task<UserModel> CreateUserAsync(UserModel userModel);
        Task<DeleteModel> DeleteUserAsync(int userId);
        Task<UserModel> UpdateUserAsync(int userId, UserModel userModel);
    }
}
