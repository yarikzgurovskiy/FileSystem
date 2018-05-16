using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace FileSystem.BLL.DTO {
    public class FolderDTO : FileSystemElement {
        public List<FolderDTO> Folders { get; set; }
        public List<FileDTO> Files { get; set; }
    }
}
