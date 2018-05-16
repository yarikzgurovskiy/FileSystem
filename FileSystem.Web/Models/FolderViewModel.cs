using FileSystem.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileSystem.Web.Models {
    public class FolderViewModel {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<FolderDTO> Folders { get; set; }
        public List<FileDTO> Files { get; set; }
        public List<FolderDTO> Path { get; set; }

    }
}
