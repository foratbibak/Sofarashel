using Sofarashel.Models.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sofarashel.Domain.Models.Categories
{
    public class CategoryImage : BaseEntity
    {
        public string ImageUrl { get; set; }

        public bool IsMain { get; set; }

        public int CategoryId { get; set; }
        public byte ImageByte { get; set; }

        #region Realtions
        public Category? Category { get; set; }
        #endregion
    }
}
