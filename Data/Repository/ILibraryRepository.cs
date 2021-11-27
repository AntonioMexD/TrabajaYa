using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrabajaYaAPI.Data.Entities;

namespace TrabajaYaAPI.Data.Repository
{
    public interface ILibraryRepository
    {
        //User
        Task<IEnumerable<UserEntity>> GetUsersAsync(string orderBy, bool showPublish = false);
        Task<UserEntity> GetUserAsync(int userId, bool showPublish = false);
        void CreateUser(UserEntity userModel);
        Task<bool> DeleteUserAsync(int userId);
        bool UpdateUser(UserEntity userModel);

        //Publish

        void CreatePublish(PublishEntity publish);
        Task<PublishEntity> GetPublishAsync(int publishId);
        Task<IEnumerable<PublishEntity>> GetPublishssAsync(int userId);
        Task<bool> UpdatePublishAsync(PublishEntity publish);
        bool DeletePublish(int publishId);

        //save changes
        Task<bool> SaveChangesAsync();
    }
}
