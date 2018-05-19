using FileSystem.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystem.DAL.Interfaces {
    public interface IFolderRepository {
        IQueryable<Folder> UserFolders { get; }
        IQueryable<Folder> AllFolders { get; }
        int Add(Folder folder);
        Folder Update(int id, Folder folder);
        void Remove(int id);
    }
}
