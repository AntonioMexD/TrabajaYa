using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrabajaYa.Models;
using TrabajaYaAPI.Data.Entities;
using TrabajaYaAPI.Data.Repository;
using TrabajaYaAPI.Exceptions;

namespace TrabajaYaAPI.Services
{
    public class PublicationsService : IPublicationsService
    {
        ILibraryRepository _libraryRepository;
        private IMapper _mapper;

        public PublicationsService(IMapper mapper, ILibraryRepository libraryRepository)
        {
            _mapper = mapper;
            _libraryRepository = libraryRepository;
        }
        public async Task<PublishModel> CreatePublishAsync(int UserId, PublishModel publish)
        {
            await validateUser(UserId);
            var publishEntity = _mapper.Map<PublishEntity>(publish);
            _libraryRepository.CreatePublish(publishEntity);
            var saveResult = await _libraryRepository.SaveChangesAsync();
            if (!saveResult)
            {
                throw new Exception("Save Error");
            }
            var modelToReturn = _mapper.Map<PublishModel>(publishEntity);
            modelToReturn.UserIde = UserId;
            return modelToReturn;
        }

        public async Task<bool> DeletePublishAsync(int UserId, int publishId)
        {
            await GetPublishAsync(UserId, publishId);
            _libraryRepository.DeletePublish(publishId);
            var saveResult = await _libraryRepository.SaveChangesAsync();
            if (!saveResult)
            {
                throw new Exception("Error while saving.");
            }
            return true;
        }

        public async Task<PublishModel> GetPublishAsync(int UserId, int publishId)
        {
            await validateUser(UserId);
            await validatePublish(publishId);
            var publish = await _libraryRepository.GetPublishAsync(publishId);
            if (publish.User.Id != UserId)
            {
                throw new NotFoundOperationException($"The publish with id: {publishId} does not exist for User id: {UserId}");
            }
            return _mapper.Map<PublishModel>(publish);
        }

        public async Task<IEnumerable<PublishModel>> GetPublishssAsync(int UserId)
        {
            await validateUser(UserId);
            var publishss = await _libraryRepository.GetPublishssAsync(UserId);
            return _mapper.Map<IEnumerable<PublishModel>>(publishss);
        }

        public async Task<bool> UpdatePublishAsync(int UserId, int publishId, PublishModel publish)
        {
            await GetPublishAsync(UserId, publishId);
            publish.Id = publishId;
            await _libraryRepository.UpdatePublishAsync(_mapper.Map<PublishEntity>(publish));
            var saveResult = await _libraryRepository.SaveChangesAsync();
            if (!saveResult)
            {
                throw new Exception("Error while saving.");
            }
            return true;
        }

        private async Task validateUser(int userId)
        {
            var user = await _libraryRepository.GetUserAsync(userId);
            if (user == null)
            {
                throw new NotFoundOperationException($"The User with id: {userId}, does not exist");
            }
        }

        private async Task validatePublish(int publishId)
        {
            var publish = await _libraryRepository.GetPublishAsync(publishId);
            if (publish == null)
            {
                throw new NotFoundOperationException($"The publish with id: {publishId}, does not exist");
            }
        }
    }
}
