using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlogProject.Entity.Messages
{
    public enum ErrorMessageCode
    {
        UsernameAlreadyExists = 1001,
        EmailAlreadyExists = 1002,
        UserIsNotActive = 1003,
        UsernameOrPasswordWrong = 1004,
        UnavailableEmail = 1005,
        UserAlreadyActive = 1006,
        ActivationIdDoesNotExist = 1007,
    }
}
