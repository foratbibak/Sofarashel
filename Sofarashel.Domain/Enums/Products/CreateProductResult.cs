using System;
using System.Collections.Generic;
using System.Text;

namespace Sofarashel.Domain.Enums.Products
{
    public enum CreateProductResult
    {
        Success,
        Error,
        CategoryNotFound,
        UnknownError,
        DatabaseError
    }
}