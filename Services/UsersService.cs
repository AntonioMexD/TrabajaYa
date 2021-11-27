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
    public class UsersService : IUsersService
    {
        ILibraryRepository _libraryRepository;
        private IMapper _mapper;

        private HashSet<string> allowedOrderByParameters = new HashSet<string>()
        {
            "id",
            "name",
            "correo",
            "telefono",
        };

        public UsersService(ILibraryRepository libraryrepository, IMapper mapper)
        {
            _libraryRepository = libraryrepository;
            _mapper = mapper;
        }
        public async Task<UserModel> CreateUserAsync(UserModel userModel)
        {
            var userEntity = _mapper.Map<UserEntity>(userModel);
            _libraryRepository.CreateUser(userEntity);
            var result = await _libraryRepository.SaveChangesAsync();
            if(result)
            {
                return _mapper.Map<UserModel>(userEntity);
            }
            throw new Exception("Database Error");
        }

        public async Task<DeleteModel> DeleteUserAsync(int userId)
        {
            await GetUserAsync(userId);
            var DeleteResult = await _libraryRepository.DeleteUserAsync(userId);
            var saveResult = await _libraryRepository.SaveChangesAsync();
            if (!saveResult || !DeleteResult)
            {
                throw new Exception("Database Error");
            }
            if (saveResult)
            {
                return new DeleteModel()
                {
                    IsSuccess = saveResult,
                    Message = "The Boutique was deleted."
                };
            }
            else
            {
                return new DeleteModel()
                {
                    IsSuccess = saveResult,
                    Message = "The Boutique was not deleted."
                };
            }
        }

        public async Task<UserModel> GetUserAsync(int userId, bool showPublish = false)
        {
            var user = await _libraryRepository.GetUserAsync(userId, showPublish);
            if(user == null)
            {
                throw new NotFoundOperationException($"The User with id: {userId} doesn't exist");
            }
            return _mapper.Map<UserModel>(user);
        }

        public async Task<IEnumerable<UserModel>> GetUsersAsync(string orderBy, bool showPublish)
        {
            if (!allowedOrderByParameters.Contains(orderBy.ToLower()))
            {
                throw new BadRequestOperationException($"The field {orderBy} is wrong, please use one of these {string.Join(",", allowedOrderByParameters)}");
            }
            var entityList = await _libraryRepository.GetUsersAsync(orderBy, showPublish);
            var modelList = _mapper.Map<IEnumerable<UserModel>>(entityList);

            return modelList;
        }

        public async Task<UserModel> UpdateUserAsync(int userId, UserModel userModel)
        {
            var userEntity = _mapper.Map<UserEntity>(userModel);
            await GetUserAsync(userId);
            userEntity.Id = userId;
            _libraryRepository.UpdateUser(userEntity);

            var saveResult = await _libraryRepository.SaveChangesAsync();

            if (!saveResult)
            {
                throw new Exception("Database Error");
            }
            return userModel;
        }
    }
}
