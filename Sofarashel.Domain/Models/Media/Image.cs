using Sofarashel.Domain.Models.Relations;
using Sofarashel.Models.Common;
using System.Collections.Generic;

namespace Sofarashel.Domain.Models.Media
{
    public class Image : BaseEntity
    {
        public string ImageUrl { get; set; }

        #region Relations
        public ICollection<Rel_Image_Product>? ProductAttribute { get; set; }
        #endregion
    }
}