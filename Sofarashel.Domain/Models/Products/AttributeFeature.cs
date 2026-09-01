using Sofarashel.Domain.Models.Relations;
using Sofarashel.Models.Common;
using System.Collections.Generic;

namespace Sofarashel.Domain.Models.Products
{
    public class AttributeFeature : BaseEntity
    {
        public string AttributTitle { get; set; }
        public string AttributValue { get; set; }

        #region Relations
        public ICollection<Rel_AttributesFetures_Product>? ProductAttributes{ get; set; }
        #endregion
    }
}