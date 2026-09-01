using Sofarashel.Domain.Models.Relations;
using Sofarashel.Models.Common;
using System.Collections.Generic;

namespace Sofarashel.Domain.Models.Products
{
    public class Product : BaseEntity
    {
        public string Title { get; set; }

        public string? Description { get; set; }

        #region Relations
        public ICollection<Rel_Image_Product>? ProductImages { get; set; }

        public ICollection<Rel_AttributesFetures_Product>? ProductAttributes { get; set; }

        public ICollection<Rel_Product_Category>? ProductCategories { get; set; }
        #endregion
    }
}