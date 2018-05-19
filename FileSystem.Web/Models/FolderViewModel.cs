using FileSystem.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileSystem.Web.Models {
    public class FolderViewModel {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<FolderDTO> Folders { get; set; }
        public IEnumerable<FileDTO> Files { get; set; }
        public IEnumerable<FolderDTO> Path { get; set; }
    }
}
