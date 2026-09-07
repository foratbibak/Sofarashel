using Sofarashel.Domain.Models.Media;
using System;

namespace Sofarashel.Application.Mapper
{
    public static class ImageMapper
    {
        public static Image MapToImage(string imageUrl)
        {
            return new Image
            {
                ImageUrl = imageUrl,
                CreatDate = DateTime.Now,
                IsDelete = false,
            };
        }
    }
}