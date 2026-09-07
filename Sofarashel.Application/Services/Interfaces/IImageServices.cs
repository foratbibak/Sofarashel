using Sofarashel.Domain.Models.Media;
using System.Collections.Generic;

namespace Sofarashel.Application.Services.Interfaces
{
    public interface IImageServices
    {
        Task<Image> UploadAsync(string imageUrl);

        Task<IEnumerable<Image>> SearchAsync(string? keyword);

        Task<Image?> GetByIdAsync(int id);

        Task<bool> DeleteAsync(int id);
    }
}