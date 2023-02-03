using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace MyBlogProject.Entity.ViewModels
{
    public class RegisterViewModel
    {
        [DisplayName("Username"), Required(ErrorMessage = "{0} is required!")]
        public string Username { get; set; }

        [DisplayName("Email"), Required(ErrorMessage = "{0} is required!"), DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DisplayName("Password"), Required(ErrorMessage = "{0} is required!"), DataType(DataType.Password), StringLength(10, ErrorMessage = "{0} could be maximum {1} character.")]
        public string Password { get; set; }

        [DisplayName("Re-Password"), Required(ErrorMessage = "{0} is required!"), DataType(DataType.Password), StringLength(6, ErrorMessage = "{0} could be maximum {1} character.")]
        [Compare("Password", ErrorMessage = "Passwords not match")]
        public string RePassword { get; set; }
    }
}