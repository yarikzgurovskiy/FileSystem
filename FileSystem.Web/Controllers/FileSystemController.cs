using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FileSystem.BLL.DTO;
using FileSystem.BLL.Interfaces;
using FileSystem.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FileSystem.Web.Controllers {
    public class FileSystemController : Controller {
        private readonly IFolderService folderService;
        private readonly IFileService fileService;

        public FileSystemController(IFolderService folderService, IFileService fileService) {
            this.folderService = folderService;
            this.fileService = fileService;
        }

        [Authorize]
        public IActionResult Drive(int? id) {
            FolderDTO folder = id != null ? folderService.GetFolder((int)id) : folderService.GetRoot();
            var model = new FolderViewModel {
                Folders = folder.Folders,
                Files = folder.Files,
                Id = folder.Id,
                Path = folderService.Path(folder.Id),
                Name = folder.Name
            };
            ViewData["Title"] = "My Drive";
            return View("Index", model);
        }

        [Authorize]
        public IActionResult Public() {
            ViewData["Title"] = "Public";
            FolderViewModel model = new FolderViewModel() {
                Folders = new List<FolderDTO>(),
                Files = new List<FileDTO>()
            };
            return View("Index", model);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateFolder(int folderId, [FromForm] string folderName) {
            if (!String.IsNullOrEmpty(folderName)) {
                var newFolder = folderService.CreateFolder(new FolderDTO { Name = folderName, FolderId = folderId });
                return RedirectToAction(nameof(Drive), new { id = folderId });
            } else {
                ModelState.AddModelError("", "Folder name can't be empty!");
                return View("Index");
            }
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult LoadFile(int folderId, IFormFile file) {
            var fileDto = new FileDTO {
                Name = file.FileName,
                FolderId = folderId,
                ContentType = file.ContentType
            };
            byte[] fileData = null;
            using (var binaryReader = new BinaryReader(file.OpenReadStream())) {
                fileData = binaryReader.ReadBytes((int)file.Length);
            }
            fileDto.Data = fileData;
            var newFile = fileService.CreateFile(fileDto);
            return RedirectToAction(nameof(Drive), new { id = folderId });
        }

        [Authorize]
        public IActionResult DownloadFile(int id) {
            var file = fileService.GetFile(id);
            return File(file.Data, file.ContentType, file.Name);
        }

        [Authorize]
        [HttpPost]
        public IActionResult DeleteFile(int id, int folderId) {
            fileService.DeleteFile(id);
            return RedirectToAction(nameof(Drive), new { id = folderId });
        }

        [Authorize]
        [HttpPost]
        public IActionResult DeleteFolder(int id, int folderId) {
            folderService.DeleteFolder(id);
            return RedirectToAction(nameof(Drive), new { id = folderId });
        }

        [Authorize]
        [HttpPost]
        public IActionResult EditFolder(FolderEditViewModel model) {
            if (ModelState.IsValid) {
                FolderDTO folder = new FolderDTO {
                    Id = model.Id,
                    Name = model.Name,
                    IsPublic = model.IsPublic,
                    UserId = model.UserId
                };
                folderService.EditFolder(folder);
                return RedirectToAction(nameof(Drive), new { id = folder.Id });
            }
            return View(model);
        }

        [Authorize]
        public IActionResult EditFolder(int id){
            var folder = folderService.GetFolder(id);
            FolderEditViewModel model = new FolderEditViewModel {
                Id = folder.Id,
                Name = folder.Name,
                FilesCount = folder.Files.Count,
                FoldersCount = folder.Folders.Count,
                IsPublic = folder.IsPublic,
                UserId = folder.UserId == null ? 0 : folder.UserId.Value
            };
            return View(model);
        }

        [Authorize]
        public IActionResult Search(string searchName) {
            var model = new FolderViewModel {
                Folders = folderService.FindByName(searchName).ToList(),
                Files = fileService.FindByName(searchName).ToList(),
                Path = new List<FolderDTO>(),
            };
            ViewBag.SearchName = searchName;
            return View("Search", model);
        }
    }
}