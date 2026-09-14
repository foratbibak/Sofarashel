using Sofarashel.Domain.ViewModels.Attributes;
using System.Collections.Generic;

namespace Sofarashel.Application.Services.Interfaces
{
    public interface IAttributeFeatureServices
    {
        Task<AttributeFeatureViewModel> GetOrCreateAsync(string title, string value);

        Task<IEnumerable<AttributeFeatureViewModel>> SearchAsync(string? keyword);

        Task<AttributeFeatureViewModel?> GetByIdAsync(int id);

        Task<AttributeFeatureViewModel?> UpdateAsync(int id, string title, string value);

        Task<AttributeFeatureViewModel?> DeleteFromLibraryAsync(int id);
    }
}