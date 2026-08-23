using Sofarashel.Models.Common;

namespace Sofarashel.Domain.Models.Products
{
    public class ProductImage : BaseEntity
    {
        public string ImageUrl { get; set; }
        public bool IsMain { get; set; }
        public int ProductId { get; set; }
        public byte ImageByte { get; set; }

        #region Relations
        public Product? Product { get; set; }
        #endregion
    }
}