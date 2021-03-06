using FileSystem.BLL.DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FileSystem.Web.Models {
    
    public class FolderEditViewModel {
        public int Id { get; set; }
        public int FolderId { get; set; }

        [Required]
        public string Name { get; set; }
        public ulong Size { get; set; }
        public int FilesCount { get; set; }
        public int FoldersCount { get; set; }
    }
}