using Sofarashel.Domain.Models.Roles;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sofarashel.Domain.Models.Permission
{
    public class RolePermissionMapping
    {
        public int RoleId { get; set; }
        public int PermissionId { get; set; }

        public Role? Role { get; set; }
        public Permission? Permission { get; set; }
    }
}
