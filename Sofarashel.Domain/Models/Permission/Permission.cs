using System;
using System.Collections.Generic;
using System.Text;
using Sofarashel.Models.Common;

namespace Sofarashel.Domain.Models.Permission
{
    public class Permission:BaseEntity
    {
        public int? ParentId { get; set; }
        public string UniqName { get; set; }
        public string DisplayName { get; set; }



        public  Permission? Parent { get; set; }

        public ICollection<RolePermissionMapping> RolePermissionMappings { get; set; }
    }
}
