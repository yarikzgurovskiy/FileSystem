using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FileSystem.Web.Models {
    public class RegisterViewModel {
        [Required]
        [MaxLength(20)]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(30)]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [Required]
        [MaxLength(20)]
        [DisplayName("Unique username")]
        [Remote(action: "CheckUserName", controller: "Account", ErrorMessage = "UserName already exists")]
        public string UserName { get; set; }

        [Required]
        [MinLength(6, ErrorMessage="Password length must be greater than 6")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Password Confirmation")]
        [Compare("Password", ErrorMessage = "Passwords don't match")]
        public string PasswordConfirm { get; set; }
    }
}
