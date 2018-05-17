using FileSystem.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileSystem.Web.Models {
    public class UsersViewModel {
        public List<UserDTO> Users { get; set; }
    }
}
