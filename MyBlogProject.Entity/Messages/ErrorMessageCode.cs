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
        UserIsNotFound = 1008,
        ProfileCouldNotUpdated = 1009,
        UserCouldNotRemove = 1010,
        UserCouldNotFind = 1011,
    }
}
