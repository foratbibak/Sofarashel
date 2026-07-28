using System;
using System.Collections.Generic;
using System.Text;

namespace Sofarashel.Domain.Enums.Categories
{
    public enum AdminEditCategoryResult
    {
        Success,
        Error,
        NotFound,
        ParentNotFound,
        CircularReference,
        UnknownError,
        DatabaseError
    }
}
