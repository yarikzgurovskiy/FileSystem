using AutoMapper;
using FileSystem.BLL.DTO;
using FileSystem.BLL.Interfaces;

using FileSystem.DAL.Entities;
using FileSystem.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;


namespace FileSystem.BLL.Services {
    public class FolderService : IFolderService {
        private readonly IUnitOfWork unitOfWork;
        private readonly IFileService fileService;
        private readonly IMapper mapper;

        public FolderService(IUnitOfWork unitOfWork, IFileService fileService, IMapper mapper) {
            this.fileService = fileService;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public int CreateFolder(FolderDTO folder) {
            int newId = unitOfWork.FolderRepository
                .Add(mapper.Map<Folder>(folder));
            unitOfWork.Save();
            return newId;
        }

        public FolderDTO GetFolder(int folderId) {
            return mapper.Map<FolderDTO>(unitOfWork.FolderRepository
                .AllFolders
                .FirstOrDefault(f => f.Id == folderId));
        }

        public FolderDTO GetRoot() {
            Folder folder = unitOfWork.FolderRepository.UserFolders
                .FirstOrDefault(f => f.FolderId == null);
            if (folder != null) {
                return mapper.Map<FolderDTO>(folder);
            } else {
                int folderId = CreateFolder(new FolderDTO { Name = "root" });
                return GetFolder(folderId);
            }
        }

        public void DeleteFolder(int id) {
            FolderDTO folderDto = GetFolder(id);
            folderDto.Elements.ForEach(el => {
                if (el is FolderDTO) DeleteFolder(el.Id);
                else fileService.DeleteFile(el.Id);
            });
            unitOfWork.FolderRepository.Remove(id);
            unitOfWork.Save();
        }

        public List<FolderDTO> Path(int folderId) {
            var folder = unitOfWork.FolderRepository.UserFolders
                .SingleOrDefault(f => f.Id == folderId);
            List<FolderDTO> folders = new List<FolderDTO>();
            if (folder.FolderId.HasValue) {
                folders.Add(
                    new FolderDTO {
                        Id = folder.FolderId.Value,
                        Name = folder.Folder.Name
                    });
                folders.AddRange(Path(folder.FolderId.Value));
            }
            folders.Reverse();
            return folders;
        }

        public void EditFolder(FolderDTO folder) {
            unitOfWork.FolderRepository.Update(mapper.Map<Folder>(folder));
            unitOfWork.Save();
        }

        public IEnumerable<FolderDTO> FindByName(string searchName) {
            return unitOfWork.FolderRepository
                .UserFolders
                .Where(f => f.Name.ToLower().Contains(searchName.ToLower()))
                .Select(f => mapper.Map<FolderDTO>(f));
        }
    }
}
