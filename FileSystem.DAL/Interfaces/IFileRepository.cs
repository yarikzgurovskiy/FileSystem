using FileSystem.DAL.Entities;
using System;
using System.Linq;

namespace FileSystem.DAL.Interfaces {
    public interface IFileRepository {
        IQueryable<File> Files { get; }
        int Add(File file);
        void Update(File file);
        void Remove(File file);
        void Remove(int id);
    }
}