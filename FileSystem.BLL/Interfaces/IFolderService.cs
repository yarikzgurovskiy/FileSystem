using FileSystem.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace FileSystem.BLL.Interfaces {
    public interface IFolderService {
        FolderDTO GetFolder(int folderId);

        FolderDTO GetRoot();

        int CreateFolder(FolderDTO folder);

        void DeleteFolder(int id);

        List<FolderDTO> Path(int folderId);

        void EditFolder(FolderDTO folder);

        IEnumerable<FolderDTO> FindByName(string searchName);
    }
}
