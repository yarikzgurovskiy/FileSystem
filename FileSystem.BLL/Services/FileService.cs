using AutoMapper;
using FileSystem.BLL.DTO;
using FileSystem.BLL.Exceptions;
using FileSystem.BLL.Interfaces;

using FileSystem.DAL.Entities;
using FileSystem.DAL.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using File = FileSystem.DAL.Entities.File;

namespace FileSystem.BLL.Services {
    public class FileService : IFileService {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public FileService(IUnitOfWork unitOfWork, IMapper mapper) {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public int CreateFile(FileDTO file) {
            int newId = unitOfWork.FileRepository.Add(mapper.Map<File>(file));
            unitOfWork.Save();
            return newId;
        }

        public void DeleteFile(int id) {
            unitOfWork.FileRepository.Remove(id);
            unitOfWork.Save();
        }

        public void EditFile(FileDTO file) {
            unitOfWork.FileRepository.Update(mapper.Map<File>(file));
            unitOfWork.Save();
        }

        public FileDTO GetFile(int fileId) {
            return mapper.Map<FileDTO>(unitOfWork.FileRepository
                .AllFiles
                .FirstOrDefault(f => f.Id == fileId));
        }

        public IEnumerable<FileDTO> FindByName(string searchName) {
            return unitOfWork.FileRepository.UserFiles
                .Where(f => f.Name.ToLower().Contains(searchName.ToLower()))
                .Select(f => mapper.Map<FileDTO>(f));
        }

        public IEnumerable<FileDTO> GetAllPublic() {
            return unitOfWork.FileRepository.AllFiles
                .Where(f => f.IsPublic)
                .Select(f => mapper.Map<FileDTO>(f));
        }

        public byte[] ReadBytes(IFormFile file, int megabytesLimit) {
            if (file.Length > megabytesLimit * 1024 * 1024)
                throw new InvalidFileSizeException($"File size must be less than {megabytesLimit} MB");
            byte[] fileData = null;
            using (var binaryReader = new BinaryReader(file.OpenReadStream())) {
                fileData = binaryReader.ReadBytes((int)file.Length);
            }
            return fileData;
        }
    }
}
