using System;
using System.Collections.Generic;
using System.Text;

namespace FileSystem.BLL.DTO {
    /// <summary>
    /// Class that represents "Leaf" element - File in FileSystem tree
    /// </summary>
    public class FileDTO : FileSystemElement {
        public byte[] Data { get; set; }
        public string ContentType { get; set; }
        public bool IsPublic { get; set; }

        public override ulong Size => (ulong)Data.LongLength;

        public override void Add(FileSystemElement el) {/* Implementation unnecessary */}
        public override void Remove(FileSystemElement el) {/* Implementation unnecessary */}

    }
}
