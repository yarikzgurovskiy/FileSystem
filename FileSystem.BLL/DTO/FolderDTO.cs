using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace FileSystem.BLL.DTO {
    /// <summary>
    /// Class that represents "Composite" element - Folder in FileSystem tree
    /// </summary>
    public class FolderDTO : FileSystemElement {
        public List<FileSystemElement> Elements { get; set; }
        public FolderDTO() {
            Elements = new List<FileSystemElement>();
        }
        public override ulong Size {
            get {
                return Elements.Aggregate(0UL, (res, el) => res + el.Size);
            }
        }
        public override void Add(FileSystemElement el) {
            Elements.Add(el);
        }

        public override void Remove(FileSystemElement el) {
            Elements.Remove(el);
        }
    }
}
