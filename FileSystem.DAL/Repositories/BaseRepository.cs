using FileSystem.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FileSystem.DAL.Repositories {
    public class BaseRepository<T, TContext>  where T : FileSystemElement, new() where TContext : DbContext {
        protected readonly TContext context;
        protected readonly IApplicationUserAccessor userAccessor;

        public BaseRepository(TContext context, IApplicationUserAccessor userAccessor) {
            this.context = context;
            this.userAccessor = userAccessor;
        }

        public int Add(T entity) {
            entity.UserId = userAccessor.GetUserId();
            context.Add(entity);
            return entity.Id;
        }

        public virtual T Update(int entityId, T updatedEntity) {
            var entity = context.Find<T>(entityId);
            if (entity.UserId != userAccessor.GetUserId() && !userAccessor.IsAdmin())
                throw new UnauthorizedAccessException("Cannot modify an entity that does not belong to current user");
            context.Entry(entity).CurrentValues.SetValues(updatedEntity);
            context.Entry(entity).Property(e => e.UserId).IsModified = false;
            return entity;
        }

        public void Remove(int id) {
            var entity = context.Find<T>(id);
            if (entity.UserId != userAccessor.GetUserId() && !userAccessor.IsAdmin())
                throw new UnauthorizedAccessException("Cannot remove an entity that does not belong to current user");
            context.Remove(entity);
        }
    }
}
