using System;
using System.Collections.Generic;
using System.Text;

namespace Sofarashel.Models.Common
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public bool IsDelete { get; set; }
    }
}
