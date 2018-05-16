using System;
using System.Collections.Generic;
using System.Text;

namespace FileSystem.BLL.DTO {
    public abstract class FileSystemElement {
        public int Id { get; set; }
        public bool IsPublic { get; set; }
        public string Name { get; set; }

        public int? FolderId { get; set; }
        public int? UserId { get; set; }
    }
}
