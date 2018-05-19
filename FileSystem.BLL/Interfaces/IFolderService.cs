using FileSystem.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace FileSystem.BLL.Interfaces {
    public interface IFolderService {
        /// <summary>
        /// Find folder by id
        /// </summary>
        /// <param name="folderId">Folder id</param>
        /// <returns>Founded folder</returns>
        FolderDTO GetFolder(int folderId);


        /// <summary>
        /// Find root folder for current user
        /// </summary>
        /// <returns>Founded folder</returns>
        FolderDTO GetRoot();


        /// <summary>
        /// Creates new folder
        /// </summary>
        /// <param name="folder">Folder to add</param>
        /// <returns>Id of new folder</returns>
        int CreateFolder(FolderDTO folder);


        /// <summary>
        /// Deletes folder by id
        /// </summary>
        /// <param name="id">Folder id</param>
        void DeleteFolder(int id);


        /// <summary>
        /// Returns names of folder in path from root folder to current
        /// </summary>
        /// <param name="folderId">Id of current folder</param>
        /// <returns>List of folders names</returns>
        List<FolderDTO> Path(int folderId);


        /// <summary>
        /// Edits givent folder
        /// </summary>
        /// <param name="folder">Folder to edit</param>
        void EditFolder(FolderDTO folder);


        /// <summary>
        /// Find folders that contains given string
        /// </summary>
        /// <param name="searchName">String to filter folders</param>
        /// <returns>IEnumerable of folders that were founded</returns>
        IEnumerable<FolderDTO> FindByName(string searchName);
    }
}
