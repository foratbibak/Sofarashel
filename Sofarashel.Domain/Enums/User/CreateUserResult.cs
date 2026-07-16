using System;
using System.Collections.Generic;
using System.Text;

namespace User_Login.Enum.User
{
    public enum CreateUserResult
    {
        Success,
        Error,
        UserNameDuplicated,
        UnknownError,
        DatabaseError
    }
}
