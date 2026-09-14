using Sofarashel.Application.Mapper;
using Sofarashel.Application.Services.Interfaces;
using Sofarashel.Domain.Contracts;
using Sofarashel.Domain.Models.Media;
using Sofarashel.Domain.ViewModels.Media;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sofarashel.Application.Services.Implementation
{
    public class ImageServices(IGenericRepository<Image> genericImageRepository) : IImageServices
    {
        public async Task<ImageViewModel> UploadAsync(string imageUrl)
        {
            var image = ImageMapper.MapToImage(imageUrl);

            await genericImageRepository.AddAsync(image);
            await genericImageRepository.SaveAsync();

            return ImageMapper.MapToViewModel(image);
        }

        public async Task<IEnumerable<ImageViewModel>> SearchAsync(string? keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                var images = await genericImageRepository.GetAllAsync();
                return ImageMapper.MapToViewModelList(images);
            }

            var filteredImages = await genericImageRepository.FindAsync(image => image.ImageUrl.Contains(keyword));
            return ImageMapper.MapToViewModelList(filteredImages);
        }

        public async Task<ImageViewModel?> GetByIdAsync(int id)
        {
            var image = await genericImageRepository.GetByIdAsync(id);

            if (image == null)
            {
                return null;
            }

            return ImageMapper.MapToViewModel(image);
        }

        public async Task<ImageViewModel?> DeleteFromLibraryAsync(int id)
        {
            var image = await genericImageRepository.GetByIdAsync(id);

            if (image == null)
            {
                return null;
            }

            image.IsDelete = true;
            image.DeleteDate = DateTime.Now;
            genericImageRepository.Update(image);
            await genericImageRepository.SaveAsync();

            return ImageMapper.MapToViewModel(image);
        }
    }
}