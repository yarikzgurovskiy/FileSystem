using System;
using System.Collections.Generic;
using System.Text;

namespace FileSystem.DAL.Interfaces {
    public interface IUnitOfWork {
        IFileRepository FileRepository { get; }
        IFolderRepository FolderRepository { get; }
        void Save();
    }
}
