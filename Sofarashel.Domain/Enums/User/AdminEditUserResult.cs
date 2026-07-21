using System;
using System.Collections.Generic;
using System.Text;

namespace Sofarashel.Domain.Enums.User
{
    public enum AdminEditUserResult
    {
        Success,
        Error,
        UserNameDuplicated,
        UnknownError,
        DatabaseError
    }
}
