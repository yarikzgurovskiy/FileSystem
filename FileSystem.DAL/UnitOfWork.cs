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

        public UnitOfWork(IFileRepository fileRepository, IFolderRepository folderRepository, FileSystemDbContext dbContext) {
            this.fileRepository = fileRepository;
            this.folderRepository = folderRepository;
            this.db = dbContext;
        }

        public IFileRepository FileRepository => fileRepository;

        public IFolderRepository FolderRepository => folderRepository;

        public void Save() {
            db.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing) {
            if (!disposed) {
                if (disposing) {
                    db.Dispose();
                    fileRepository.Dispose();
                    folderRepository.Dispose();
                }
                disposed = true;
            }
        }

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
