using AutoMapper;
using FileSystem.BLL.DTO;
using FileSystem.BLL.Interfaces;
using FileSystem.DAL.Entities;
using FileSystem.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

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
            unitOfWork.FileRepository.Update(file.Id, mapper.Map<File>(file));
            unitOfWork.Save();
        }

        public FileDTO GetFile(int fileId) {
            return mapper.Map<FileDTO>(unitOfWork.FileRepository.UserFiles
                .FirstOrDefault(f => f.Id == fileId));
        }

        public IEnumerable<FileDTO> FindByName(string searchName) {
            return unitOfWork.FileRepository.UserFiles
                .Where(f => f.Name.ToLower().Contains(searchName.ToLower()))
                .Select(f => mapper.Map<FileDTO>(f));
        }
    }
}
