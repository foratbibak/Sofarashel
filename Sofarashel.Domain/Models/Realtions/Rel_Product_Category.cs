using Sofarashel.Domain.Models.Categories;
using Sofarashel.Domain.Models.Products;

namespace Sofarashel.Domain.Models.Relations
{
    public class Rel_Product_Category
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }

        #region Relations
        public Product Product { get; set; }
        public Category Category { get; set; }
        #endregion
    }
}