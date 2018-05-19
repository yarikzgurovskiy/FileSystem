using FileSystem.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FileSystem.DAL.Interfaces;

namespace FileSystem.DAL.Repositories {
    public class FolderRepository : BaseRepository<Folder, FileSystemDbContext>, IFolderRepository {
        public FolderRepository(FileSystemDbContext context, IApplicationUserAccessor userAccessor) : base(context, userAccessor) {
        }

        public IQueryable<Folder> UserFolders {
            get {
                int userId = userAccessor.GetUserId();
                return AllFolders.Where(fold => fold.UserId == userId).AsQueryable();
            }
        }

        public IQueryable<Folder> AllFolders => context.Folders
                    .Include(f => f.Folders)
                    .Include(f => f.Files)
                    .Include(f => f.Folder).AsQueryable();
    }
}
