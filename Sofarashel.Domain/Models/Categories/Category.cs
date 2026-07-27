using Sofarashel.Models.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sofarashel.Domain.Models.Categories
{
    public class Category:BaseEntity
    {
        public string Title { get; set; }

        public string? Description { get; set; }

        public bool IsCategory { get; set; }

        public int? ParentId { get; set; }

        #region Product
        public string? Material { get; set; }

        public string? FabricType { get; set; }

        public string? Color { get; set; }

        public string? Style { get; set; }

        public decimal? Length { get; set; }

        public decimal? Width { get; set; }

        public decimal? Height { get; set; }
        #endregion

        #region Realtions
        public Category? Parent { get; set; }

        public ICollection<Category> Children { get; set; }

        public ICollection<CategoryImage>? Images { get; set; }
        #endregion
    }
}
