using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Text;

namespace FileSystem.DAL.Entities {
    public class File : FileSystemElement {
        
        public byte[] FileData { get; set; }
        public string ContentType { get; set; }
    }
}
