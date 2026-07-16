using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sofarashel.Application.Security
{
    public static class FileValidatorExtensions
    {
        public static bool ImageValidate(this IFormFile image)
        {
            string[] allowedExtensions = { ".jpg",".jpeg",".gif",
            ".webp"};
            var fileExtension = Path.GetExtension(image.FileName).ToLower();
            if (!allowedExtensions.Contains(fileExtension))
            {
                return false;
            }
            return true;
        }
    }
}
