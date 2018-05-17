using FileSystem.BLL.DTO;
using FileSystem.BLL.Interfaces;
using FileSystem.DAL;
using FileSystem.DAL.Entities;
using FileSystem.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FileSystem.BLL.Services {
    public class FolderService : IFolderService {
        private readonly IUnitOfWork unitOfWork;
        private readonly IFileService fileService;

        private readonly Func<FolderDTO, Folder> toFolder;
        private readonly Func<Folder, FolderDTO> toFolderDto;

        public FolderService(IUnitOfWork unitOfWork, IFileService fileService) {
            this.fileService = fileService;
            this.unitOfWork = unitOfWork;
            toFolder = ToFolder;
            toFolderDto = ToFolderDto;
        }


        public int CreateFolder(FolderDTO folder) {
            int newId = unitOfWork.FolderRepository.Add(toFolder(folder));
            unitOfWork.Save();
            return newId;
        }

        public FolderDTO GetFolder(int folderId) {
            return toFolderDto(unitOfWork.FolderRepository.Folders.FirstOrDefault(f => f.Id == folderId));
        }

        public FolderDTO GetRoot() {
            Folder folder = unitOfWork.FolderRepository.Folders.FirstOrDefault(f => f.FolderId == null);
            if (folder != null) {
                return toFolderDto(folder);
            } else {
                int folderId = CreateFolder(new FolderDTO { Name = "root" });
                return GetFolder(folderId);
            }
        }

        private FolderDTO ToFolderDto(Folder fold) {
            var folder = new FolderDTO {
                Id = fold.Id,
                FolderId = fold.FolderId,
                Name = fold.Name,
                IsPublic = fold.IsPublic,
                UserId = fold.UserId,
                Folders = new List<FolderDTO>(),
                Files = new List<FileDTO>()
            };
            var folders = fold.Folders.Select(f => GetFolder(f.Id)).ToList();
            var files = fold.Files.Select(f => fileService.GetFile(f.Id)).ToList();
            folder.Files.AddRange(files);
            folder.Folders.AddRange(folders);
            return folder;
        }

        private Folder ToFolder(FolderDTO f) => new Folder {
            Name = f.Name,
            FolderId = f.FolderId,
            Id = f.Id,
            IsPublic = f.IsPublic,
            UserId = f.UserId
        };

        public void DeleteFolder(int id) {
            FolderDTO folderDto = GetFolder(id);
            folderDto.Folders.ForEach(f => DeleteFolder(f.Id));
            folderDto.Files.ForEach(f => fileService.DeleteFile(f.Id));
            unitOfWork.FolderRepository.Remove(id);
            unitOfWork.Save();
        }

        public List<FolderDTO> Path(int folderId) {
            var folder = unitOfWork.FolderRepository.Folders.SingleOrDefault(f => f.Id == folderId);
            List<FolderDTO> folders = new List<FolderDTO>();
            if (folder.FolderId.HasValue) {
                folders.Add(new FolderDTO { Id = folder.FolderId.Value, Name = folder.Folder.Name });
                folders.AddRange(Path(folder.FolderId.Value));
            }
            folders.Reverse();
            return folders;
        }

        public void EditFolder(FolderDTO folder) {
            unitOfWork.FolderRepository.Update(toFolder(folder));
            unitOfWork.Save();
        }

        public IEnumerable<FolderDTO> FindByName(string searchName) {
            return unitOfWork.FolderRepository.Folders
                .Where(f => f.Name.ToLower().Contains(searchName.ToLower()))
                .AsEnumerable()
                .Select(f => toFolderDto(f));
        }
        
    }
}
