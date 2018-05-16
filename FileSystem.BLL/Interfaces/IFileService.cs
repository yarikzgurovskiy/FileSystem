using FileSystem.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace FileSystem.BLL.Interfaces {
    public interface IFileService : IDisposable {
        FileDTO GetFile(int fileId);

        int CreateFile(FileDTO file);

        void DeleteFile(int id);

        void EditFile(FileDTO file);

        IEnumerable<FileDTO> FindByName(string searchName);
    }
}
