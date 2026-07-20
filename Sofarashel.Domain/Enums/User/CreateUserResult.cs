using System;
using System.Collections.Generic;
using System.Text;

namespace Sofarashel.Enum.User
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
