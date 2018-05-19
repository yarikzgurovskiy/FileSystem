using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FileSystem.BLL.DTO;
using FileSystem.BLL.Exceptions;
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
            var folder = id.HasValue
                ? folderService.GetFolder(id.Value)
                : folderService.GetRoot();

            if (folder != null) {
                var model = new FolderViewModel {
                    Folders = folder.Elements.OfType<FolderDTO>(),
                    Files = folder.Elements.OfType<FileDTO>(),
                    Id = folder.Id,
                    Path = folderService.Path(folder.Id),
                    Name = folder.Name
                };
                ViewData["Title"] = "My Drive";
                return View("Index", model);
            }
            return NotFound($"No folder with id {id}");
        }

        public IActionResult Public() {
            ViewData["Title"] = "Public";
            var files = fileService.GetAllPublic();
            FolderViewModel model = new FolderViewModel() {
                Files = files.ToList()
            };
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public IActionResult CreateFolder(int folderId, string folderName) {
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
        public IActionResult LoadFile(int folderId, IFormFile file) {
            byte[] fileData;
            try {
                fileData = fileService.ReadBytes(file, 4);
            } catch (InvalidFileSizeException) {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            var fileDto = new FileDTO {
                Name = file.FileName,
                FolderId = folderId,
                ContentType = file.ContentType,
                Data = fileData
            };
            var newFile = fileService.CreateFile(fileDto);
            return RedirectToAction(nameof(Drive), new { id = folderId });
        }

        [Authorize]
        public IActionResult DownloadFile(int id) {
            FileDTO file;
            try {
                file = fileService.GetFile(id);
            } catch (UnauthorizedAccessException) {
                return StatusCode(StatusCodes.Status403Forbidden);
            }

            if (file != null) {
                return File(file.Data, file.ContentType, file.Name);
            } else {
                return NotFound($"File with id {id} doesn't exists");
            }
        }

        [Authorize]
        [HttpPost]
        public IActionResult DeleteFile(int id, int folderId) {
            try {
                fileService.DeleteFile(id);
            } catch (UnauthorizedAccessException) {
                return StatusCode(StatusCodes.Status403Forbidden);
            }
            return RedirectToAction(nameof(Drive), new { id = folderId });
        }

        [Authorize]
        [HttpPost]
        public IActionResult DeleteFolder(int id, int folderId) {
            try {
                folderService.DeleteFolder(id);
            } catch (UnauthorizedAccessException) {
                return StatusCode(StatusCodes.Status403Forbidden);
            }
            return RedirectToAction(nameof(Drive), new { id = folderId });
        }

        [Authorize]
        [HttpPost]
        public IActionResult EditFolder(FolderEditViewModel model) {
            if (ModelState.IsValid) {
                FolderDTO folder = new FolderDTO {
                    Id = model.Id,
                    Name = model.Name,
                    FolderId = model.FolderId
                };
                try {
                    folderService.EditFolder(folder);
                } catch (UnauthorizedAccessException) {
                    return StatusCode(StatusCodes.Status403Forbidden);
                }
                return RedirectToAction(nameof(Drive), new { id = folder.FolderId });
            }
            return View(model);
        }

        [Authorize]
        public IActionResult EditFolder(int id) {
            FolderDTO folder;
            try {
                folder = folderService.GetFolder(id);
            } catch (UnauthorizedAccessException) {
                return StatusCode(StatusCodes.Status403Forbidden);
            }
            if (folder != null) {
                FolderEditViewModel model = new FolderEditViewModel {
                    Id = folder.Id,
                    Name = folder.Name,
                    FilesCount = folder.Elements.OfType<FileDTO>().Count(),
                    FoldersCount = folder.Elements.OfType<FolderDTO>().Count(),
                    Size = folder.Size,
                    FolderId = folder.FolderId ?? 0
                };
                return View(model);
            }
            return NotFound($"Folder with id {id} doesn't exists");
        }

        [Authorize]
        [HttpPost]
        public IActionResult EditFile(FileEditViewModel model) {
            if (ModelState.IsValid) {
                FileDTO file = new FileDTO {
                    Id = model.Id,
                    Name = model.Name,
                    IsPublic = model.IsPublic,
                    FolderId = model.FolderId
                };
                try {
                    fileService.EditFile(file);
                } catch (UnauthorizedAccessException) {
                    return StatusCode(StatusCodes.Status403Forbidden);
                }
                return RedirectToAction(nameof(Drive), new { id = file.FolderId });
            }
            return View(model);
        }

        [Authorize]
        public IActionResult EditFile(int id) {
            FileDTO file;
            try {
                file = fileService.GetFile(id);
            } catch (UnauthorizedAccessException) {
                return StatusCode(StatusCodes.Status403Forbidden);
            }

            if (file != null) {
                FileEditViewModel model = new FileEditViewModel {
                    Id = file.Id,
                    Name = file.Name,
                    IsPublic = file.IsPublic,
                    Size = file.Data.Length,
                    FolderId = file.FolderId ?? 0
                };
                return View(model);
            }
            return NotFound($"File with id {id} doesn't exists");
        }


        [Authorize]
        public IActionResult Search(string searchName) {
            var model = new FolderViewModel {
                Folders = folderService.FindByName(searchName),
                Files = fileService.FindByName(searchName)
            };
            ViewBag.SearchName = searchName;
            return View("Search", model);
        }
    }
}