using Sofarashel.Application.Mapper;
using Sofarashel.Application.Services.Interfaces;
using Sofarashel.Domain.Contracts;
using Sofarashel.Domain.Models.Media;
using System;
using System.Collections.Generic;

namespace Sofarashel.Application.Services.Implementation
{
    public class ImageServices(IGenericRepository<Image> genericImageRepository) : IImageServices
    {
        public async Task<Image> UploadAsync(string imageUrl)
        {
            var image = ImageMapper.MapToImage(imageUrl);

            await genericImageRepository.AddAsync(image);
            await genericImageRepository.SaveAsync();

            return image;
        }

        public async Task<IEnumerable<Image>> SearchAsync(string? keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return await genericImageRepository.FindAsync(i => !i.IsDelete);
            }

            return await genericImageRepository.FindAsync(i => !i.IsDelete && i.ImageUrl.Contains(keyword));
        }

        public async Task<Image?> GetByIdAsync(int id)
        {
            return await genericImageRepository.SelectAsync(i => i.Id == id && !i.IsDelete);
        }

        public async Task<Image?> DeleteFromLibraryAsync(int id)
        {
            var image = await genericImageRepository.SelectAsync(i => i.Id == id && !i.IsDelete);
            if (image is null)
            {
                return null;
            }

            image.IsDelete = true;
            image.DeleteDate = DateTime.Now;
            genericImageRepository.Update(image);
            await genericImageRepository.SaveAsync();

            return image;
        }
    }
}