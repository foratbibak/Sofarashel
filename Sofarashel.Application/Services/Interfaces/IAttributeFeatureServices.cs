using Sofarashel.Domain.Models.Products;
using System.Collections.Generic;

namespace Sofarashel.Application.Services.Interfaces
{
    public interface IAttributeFeatureServices
    {
        Task<AttributeFeature> GetOrCreateAsync(string title, string value);

        Task<IEnumerable<AttributeFeature>> SearchAsync(string? keyword);

        Task<AttributeFeature?> GetByIdAsync(int id);

        Task<AttributeFeature?> DeleteFromLibraryAsync(int id);
    }
}