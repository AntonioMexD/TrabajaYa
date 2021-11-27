using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrabajaYa.Models;

namespace TrabajaYaAPI.Services
{
    public interface IPublicationsService
    {
        Task<PublishModel> CreatePublishAsync(int UserId, PublishModel publish);
        Task<PublishModel> GetPublishAsync(int UserId, int publishId);
        Task<IEnumerable<PublishModel>> GetPublishssAsync(int UserId);
        Task<bool> UpdatePublishAsync(int UserId, int publishId, PublishModel publish);
        Task<bool> DeletePublishAsync(int UserId, int publishId);
    }
}
