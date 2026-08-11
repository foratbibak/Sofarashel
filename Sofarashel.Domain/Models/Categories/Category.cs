using Sofarashel.Models.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sofarashel.Domain.Models.Categories
{
    public class Category : BaseEntity
    {
        public string Title { get; set; }

        public string? Description { get; set; }

        public bool IsCategory { get; set; }

        public int? ParentId { get; set; }

        #region Realtions
        public Category? Parent { get; set; }

        public ICollection<Category>? SubCategories { get; set; }

        public ICollection<CategoryImage>? Images { get; set; }

        public ProductDetail? ProductDetail { get; set; }
        #endregion
    }
}