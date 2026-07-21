using Sofarashel.Domain.Models.Permission;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Sofarashel.Domain.ViewModels.Roles
{
    public class AdminEditRoleViewModel
    {
        [DisplayName("نام نقش")]
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public IEnumerable<Permission> permissions { get; set; }
            = Enumerable.Empty<Permission>();



        public List<int> PermissonSelectedIds { get; set; } = new();
    }
}
