using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FileSystem.DAL.Entities {
    public class User : IdentityUser<int> {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [InverseProperty("User")]
        public ICollection<Folder> Folders { get; set; }

        [InverseProperty("User")]
        public ICollection<File> Files { get; set; }
    }
}
