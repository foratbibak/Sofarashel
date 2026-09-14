using Sofarashel.Domain.ViewModels.Media;
using System.Collections.Generic;

namespace Sofarashel.Application.Services.Interfaces
{
    public interface IImageServices
    {
        Task<ImageViewModel> UploadAsync(string imageUrl);

        Task<IEnumerable<ImageViewModel>> SearchAsync(string? keyword);

        Task<ImageViewModel?> GetByIdAsync(int id);

        Task<ImageViewModel?> DeleteFromLibraryAsync(int id);
    }
}