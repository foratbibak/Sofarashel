using Sofarashel.Domain.Models.Media;
using Sofarashel.Domain.Models.Products;

namespace Sofarashel.Domain.Models.Relations
{
    public class Rel_Image_Product
    {
        public int ImageId { get; set; }
        public int ProductId { get; set; }

        public bool IsMain { get; set; }
        public int DisplayOrder { get; set; }

        #region Relations
        public Image Image { get; set; }
        public Product Product { get; set; }
        #endregion
    }
}