using System;
using System.Collections.Generic;
using System.Text;

namespace Sofarashel.Domain.Enums.Products
{
    public enum AdminEditProductResult
    {
        Success,
        Error,
        NotFound,
        CategoryNotFound,
        UnknownError,
        DatabaseError
    }
}
