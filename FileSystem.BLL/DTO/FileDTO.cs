using System;
using System.Collections.Generic;
using System.Text;

namespace FileSystem.BLL.DTO {
    public class FileDTO : FileSystemElement {
        public byte[] Data { get; set; }
        public string ContentType { get; set; }
        
    }
}
