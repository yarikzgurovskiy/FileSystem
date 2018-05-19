using FileSystem.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FileSystem.DAL {
    public class UnitOfWork : IUnitOfWork {
        private readonly DbContext db;
        private readonly IFileRepository fileRepository;
        private readonly IFolderRepository folderRepository;

        public UnitOfWork(
            IFileRepository fileRepository, 
            IFolderRepository folderRepository,
            FileSystemDbContext dbContext
        ) {
            this.fileRepository = fileRepository;
            this.folderRepository = folderRepository;
            this.db = dbContext;
        }

        public IFileRepository FileRepository => fileRepository;

        public IFolderRepository FolderRepository => folderRepository;

        public void Save() {
            db.SaveChanges();
        }
    }
}
