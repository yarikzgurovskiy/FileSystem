using FileSystem.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace FileSystem.BLL.Interfaces {
    public interface IUserService {
        IEnumerable<UserDTO> GetUsers();
        void DeleteUser(int userId);

    }
}
