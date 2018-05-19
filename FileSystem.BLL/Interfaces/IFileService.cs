using FileSystem.BLL.DTO;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace FileSystem.BLL.Interfaces {
    public interface IFileService {
        /// <summary>
        /// Get File by id
        /// </summary>
        /// <param name="fileId">File id</param>
        /// <returns>File with specified id or null</returns>
        FileDTO GetFile(int fileId);


        /// <summary>
        /// Creates new File
        /// </summary>
        /// <param name="file">New File</param>
        /// <returns>Id of new file</returns>
        int CreateFile(FileDTO file);


        /// <summary>
        /// Delete file by id
        /// </summary>
        /// <param name="id">File id</param>
        void DeleteFile(int id);


        /// <summary>
        /// Edit specified file
        /// </summary>
        /// <param name="file">File to edit</param>
        void EditFile(FileDTO file);


        /// <summary>
        /// Find files that contains given string
        /// </summary>
        /// <param name="searchName">Given string</param>
        /// <returns>IEnumerable of Files</returns>
        IEnumerable<FileDTO> FindByName(string searchName);


        /// <summary>
        /// Find all public files
        /// </summary>
        /// <returns>IEnumerable of founded files</returns>
        IEnumerable<FileDTO> GetAllPublic();

        /// <summary>
        /// Reads file bytes
        /// </summary>
        /// <param name="file">File to read bytes from</param>
        /// <param name="megabytesLimit">Limit in megabytes</param>
        /// <returns>Array of bytes</returns>
        /// <exception cref="InvalidFileSizeException">Throws if File size exceeds the limit</exception>
        byte[] ReadBytes(IFormFile file, int megabytesLimit);
    }
}
