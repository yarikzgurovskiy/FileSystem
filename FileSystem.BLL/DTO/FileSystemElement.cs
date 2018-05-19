using System;
using System.Collections.Generic;
using System.Text;

namespace FileSystem.BLL.DTO {
    // Using Composite Pattern
    /// <summary>
    /// Class that represents "Component" in FileSystem tree
    /// </summary>
    public abstract class FileSystemElement {
        public int Id { get; set; }
        public string Name { get; set; }

        public int? FolderId { get; set; }

        public abstract ulong Size { get; }
        public abstract void Add(FileSystemElement el);
        public abstract void Remove(FileSystemElement el);
        
    }
}
