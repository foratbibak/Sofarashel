using System;
using System.Collections.Generic;
using System.Text;

namespace Sofarashel.Domain.Enums.Categories
{
    public enum CreateCategoryResult
    {
        Success,
        Error,
        ParentNotFound,
        UnknownError,
        DatabaseError
    }
}
