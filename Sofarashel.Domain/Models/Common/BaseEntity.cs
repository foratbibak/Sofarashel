using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Sofarashel.Models.Common
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        [JsonIgnore]
        public DateTime? DeleteDate { get; set; }

        [JsonIgnore]
        public bool IsDelete { get; set; }
    }
}
