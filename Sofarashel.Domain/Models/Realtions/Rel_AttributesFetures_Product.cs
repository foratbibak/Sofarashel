using Sofarashel.Domain.Models.Products;

namespace Sofarashel.Domain.Models.Relations
{
    public class Rel_AttributesFetures_Product
    {
        public int AttributeFeatureId { get; set; }
        public int ProductId { get; set; }

        public int DisplayOrder { get; set; }

        #region Relations
        public AttributeFeature AttributeFeature { get; set; }
        public Product Product { get; set; }
        #endregion
    }
}