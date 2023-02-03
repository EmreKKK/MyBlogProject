using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyBlogProject.Entity.ViewModels
{
    public class LoginViewModel
    {
        [DisplayName("Username"), Required(ErrorMessage = "{0} is required!")]
        public string Username { get; set; }

        [DisplayName("Password"), Required(ErrorMessage = "{0} is required!"), DataType(DataType.Password)]
        public string Password { get; set; }
    }
}