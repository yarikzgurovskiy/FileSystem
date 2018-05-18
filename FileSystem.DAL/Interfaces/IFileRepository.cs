using FileSystem.DAL.Entities;
using System;
using System.Linq;

namespace FileSystem.DAL.Interfaces {
    public interface IFileRepository {
        IQueryable<File> Files { get; }
        int Add(File file);
        File Update(int id, File file);
        void Remove(int id);
    }
}