using Sofarashel.Domain.Models.Products;
using Sofarashel.Domain.ViewModels.Attributes;
using System.Collections.Generic;
using System.Linq;

namespace Sofarashel.Application.Mapper
{
    public static class AttributeFeatureMapper
    {
        public static AttributeFeatureViewModel MapToViewModel(AttributeFeature attribute)
        {
            return new AttributeFeatureViewModel
            {
                Id = attribute.Id,
                Title = attribute.AttributTitle,
                Value = attribute.AttributValue
            };
        }

        public static List<AttributeFeatureViewModel> MapToViewModelList(IEnumerable<AttributeFeature> attributes)
        {
            return attributes.Select(MapToViewModel).ToList();
        }
    }
}