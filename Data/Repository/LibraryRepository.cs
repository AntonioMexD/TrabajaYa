using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrabajaYaAPI.Data.Entities;

namespace TrabajaYaAPI.Data.Repository
{
    public class LibraryRepository : ILibraryRepository
    {
        private LibraryDbContext _dbContext;

        public LibraryRepository(LibraryDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void CreateUser(UserEntity userModel)
        {
            _dbContext.Users.Add(userModel);
        }

        public void CreatePublish(PublishEntity publish)
        {
            if(publish.User != null)
            {
                _dbContext.Entry(publish.User).State = EntityState.Unchanged;
            }
            _dbContext.Publish.Add(publish);
        }


        public bool DeletePublish(int publishId)
        {
            var publishToDelete = new PublishEntity() { Id = publishId };
            _dbContext.Entry(publishToDelete).State = EntityState.Deleted;
            return true;
        }

        public async Task<bool> DeleteUserAsync(int userId)
        {
            var userToDelete = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
            _dbContext.Users.Remove(userToDelete);
            return true;
        }

        public async Task<PublishEntity> GetPublishAsync(int publishId)
        {
            IQueryable<PublishEntity> query = _dbContext.Publish;
            query = query.Include(p => p.User);
            query = query.AsNoTracking();
            var publish = await query.SingleOrDefaultAsync(p => p.Id == publishId);
            return publish;
        }

        public async Task<IEnumerable<PublishEntity>> GetPublishssAsync(int userId)
        {
            IQueryable<PublishEntity> query = _dbContext.Publish;
            query = query.Where(p => p.User.Id == userId);
            query = query.Include(p => p.User);
            query = query.AsNoTracking();
            return await query.ToArrayAsync();
        }

        public async Task<UserEntity> GetUserAsync(int userId, bool showPublish = false)
        {
            IQueryable<UserEntity> query = _dbContext.Users;
            query = query.AsNoTracking();
            if(showPublish)
            {
                query = query.Include(u => u.Publications);
            }
            return await query.FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<IEnumerable<UserEntity>> GetUsersAsync(string orderBy, bool showPublish = false)
        {
            IQueryable<UserEntity> query = _dbContext.Users;
            query = query.AsNoTracking();

            switch(orderBy)
            {
                case "id":
                    query = query.OrderBy(us => us.Id);
                    break;
                case "name":
                    query = query.OrderBy(us => us.Name);
                    break;
                case "correo":
                    query = query.OrderBy(us => us.Correo);
                    break;
                case "Telefono":
                    query = query.OrderBy(us => us.Correo);
                    break;
                default:
                    query = query.OrderBy(us => us.Id);
                    break;
            }
            return await query.ToListAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            try
            {
                var res = await _dbContext.SaveChangesAsync();
                return res > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UpdatePublishAsync(PublishEntity publish)
        {
            var publishToUpdate = await _dbContext.Publish.FirstOrDefaultAsync(p => p.Id == publish.Id);
            publishToUpdate.Empresa = publish.Empresa ?? publishToUpdate.Empresa;
            publishToUpdate.Titulo = publish.Titulo ?? publishToUpdate.Titulo;
            publishToUpdate.Descripcion = publish.Descripcion ?? publishToUpdate.Descripcion;
            publishToUpdate.Area = publish.Area ?? publishToUpdate.Area;
            publishToUpdate.Departamento = publish.Departamento ?? publishToUpdate.Departamento;
            publishToUpdate.Jornada = publish.Jornada ?? publishToUpdate.Jornada;
            publishToUpdate.Requisitos = publish.Requisitos ?? publishToUpdate.Requisitos;
            publishToUpdate.Correo = publish.Correo ?? publishToUpdate.Correo;
            publishToUpdate.Telefono = publish.Telefono ?? publishToUpdate.Telefono;
            return true;
        }

        public bool UpdateUser(UserEntity userModel)
        {
            var userToUpdate = _dbContext.Users.FirstOrDefault(u => u.Id == userModel.Id);
            _dbContext.Entry(userToUpdate).CurrentValues.SetValues(userModel);
            return true;
        }
    }
}
