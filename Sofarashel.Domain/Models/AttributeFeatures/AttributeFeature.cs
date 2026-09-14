using Sofarashel.Domain.Models.Relations;
using Sofarashel.Models.Common;
using System.Collections.Generic;

namespace Sofarashel.Domain.Models.AttributeFeatures
{
    public class AttributeFeature : BaseEntity
    {
        public string AttributeTitle { get; set; }
        public string AttributeValue { get; set; }

        #region Relations
        public ICollection<Rel_AttributesFetures_Product>? ProductAttributes { get; set; }
        #endregion
    }
}