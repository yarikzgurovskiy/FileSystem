using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FileSystem.DAL.Entities {
    public class FileSystemElement {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsPublic { get; set; }
        public int? UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }


        public int? FolderId { get; set; }

        [ForeignKey("FolderId")]
        public Folder Folder { get; set; }
    }
}
