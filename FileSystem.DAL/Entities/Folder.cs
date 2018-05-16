using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FileSystem.DAL.Entities {
    public class Folder : FileSystemElement {
        public virtual ICollection<Folder> Folders { get; set; }
        public virtual ICollection<File> Files { get; set; }
    }
}
