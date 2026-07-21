
using Sofarashel.Domain.Models.Permission;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Sofarashel.Domain.ViewModels.Roles
{
    public class AdminCreateRoleViewModel
    {
        [DisplayName("نام نقش")]
        public string RoleName { get; set; }

        public IEnumerable<Permission>? permissions { get; set; }

        public List<int>? PermissonSelectedIds{ get; set; }


    }
}
