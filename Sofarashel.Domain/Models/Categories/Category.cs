using Sofarashel.Domain.Models.Relations;
using Sofarashel.Models.Common;
using System.Collections.Generic;

namespace Sofarashel.Domain.Models.Categories
{
    public class Category : BaseEntity
    {
        public string Title { get; set; }

        public string? Description { get; set; }

        public bool IsCategory { get; set; }

        public string? MainImage { get; set; }

        public int? ParentId { get; set; }

        #region Relations
        public Category? Parent { get; set; }

        public ICollection<Category>? SubCategories { get; set; }

        public ICollection<Rel_Product_Category>? ProductCategories { get; set; }
        #endregion
    }
}