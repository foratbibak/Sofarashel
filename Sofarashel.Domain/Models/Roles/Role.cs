
using System;
using System.Collections.Generic;
using System.Text;
using User_Login.Models.Common;

namespace Sofarashel.Domain.Models.Roles
{
    public class Role:BaseEntity
    {
        public string RoleName { get; set; }


        #region Realations
        public ICollection<UserInRoles>? UserInRole { get; set; }

        //public ICollection<RolePermissionMapping>? RolePermissionMappings { get; set; }
        #endregion

    }
}
