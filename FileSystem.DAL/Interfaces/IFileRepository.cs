using FileSystem.DAL.Entities;
using System;
using System.Linq;

namespace FileSystem.DAL.Interfaces {
    public interface IFileRepository {
        IQueryable<File> UserFiles { get; }
        IQueryable<File> AllFiles { get; }
        int Add(File file);
        File Update(File file);
        void Remove(int id);
    }
}