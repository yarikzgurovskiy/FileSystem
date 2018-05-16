using FileSystem.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystem.DAL.Interfaces {
    public interface IFolderRepository : IDisposable {
        IQueryable<Folder> Folders { get; }
        int Add(Folder folder);
        void Update(Folder folder);
        void Remove(Folder f);
        void Remove(int id);
    }
}
