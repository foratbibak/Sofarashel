using Sofarashel.Domain.Models.Media;
using Sofarashel.Domain.ViewModels.Media;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public static ImageViewModel MapToViewModel(Image image)
        {
            return new ImageViewModel
            {
                Id = image.Id,
                ImageUrl = image.ImageUrl
            };
        }

        public static List<ImageViewModel> MapToViewModelList(IEnumerable<Image> images)
        {
            return images.Select(MapToViewModel).ToList();
        }
    }
}