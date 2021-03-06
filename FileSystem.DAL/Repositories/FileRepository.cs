﻿using FileSystem.DAL.EF;
using FileSystem.DAL.Entities;
using FileSystem.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FileSystem.DAL.Repositories {
    public class FileRepository : BaseRepository<File, FileSystemDbContext>, IFileRepository {
        public FileRepository(FileSystemDbContext context, IApplicationUserAccessor userAccessor) 
            : base(context, userAccessor) {
        }

        public IQueryable<File> UserFiles {
            get {
                int userId = userAccessor.GetUserId();
                return context.Files
                    .Where(fold => fold.UserId == userId).AsQueryable();
            }
        }

        public IQueryable<File> AllFiles => context.Files.AsQueryable();

        public override File Update(File updatedEntity) {
            File entity = base.Update(updatedEntity);
            context.Entry(entity).Property(f => f.ContentType).IsModified = false;
            context.Entry(entity).Property(f => f.FileData).IsModified = false;
            return entity;
        }

    }
}
