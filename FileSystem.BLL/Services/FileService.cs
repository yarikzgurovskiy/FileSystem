using FileSystem.BLL.DTO;
using FileSystem.BLL.Interfaces;
using FileSystem.DAL;
using FileSystem.DAL.Entities;
using FileSystem.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FileSystem.BLL.Services {
    public class FileService : IFileService {
        private readonly IUnitOfWork unitOfWork;

        private readonly Func<File, FileDTO> toFileDto;
        private readonly Func<FileDTO, File> toFile;

        public FileService(IUnitOfWork unitOfWork) {
            this.unitOfWork = unitOfWork;
            toFile = ToFile;
            toFileDto = ToFileDto;
        }
        public int CreateFile(FileDTO file) {
            int newId = unitOfWork.FileRepository.Add(toFile(file));
            unitOfWork.Save();
            return newId;
        }

        public void DeleteFile(int id) {
            unitOfWork.FileRepository.Remove(id);
            unitOfWork.Save();
        }

        public void EditFile(FileDTO file) {
            unitOfWork.FileRepository.Update(toFile(file));
            unitOfWork.Save();
        }

        public FileDTO GetFile(int fileId) {
            return toFileDto(unitOfWork.FileRepository.Files.FirstOrDefault(f => f.Id == fileId));
        }

        private File ToFile(FileDTO f) => new File {
            Name = f.Name,
            FileData = f.Data,
            FolderId = f.FolderId,
            Id = f.Id,
            ContentType = f.ContentType,
            UserId = f.UserId
        };

        private FileDTO ToFileDto(File f) => new FileDTO {
            Name = f.Name,
            Data = f.FileData,
            Id = f.Id,
            ContentType = f.ContentType,
            FolderId = f.FolderId,
            UserId = f.UserId
        };

        public IEnumerable<FileDTO> FindByName(string searchName) {
            return unitOfWork.FileRepository.Files
                .Where(f => f.Name.ToLower().Equals(searchName.ToLower()))
                .Select(f => toFileDto(f));
        }
    }
}
