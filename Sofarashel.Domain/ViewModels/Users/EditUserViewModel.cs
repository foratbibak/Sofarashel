using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Sofarashel.Domain.ViewModels.User
{
    public class EditUserViewModel
    {
        public int Id { get; set; }
        [DisplayName("نام")]
        public string? FirstName { get; set; }

        [DisplayName("نام خانوادگی")]
        public string? LastName { get; set; }

        [DisplayName("نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} را وارد فرمایید")]
        public string UserName { get; set; }
        [DisplayName("فعال/غیرفعال")]
        public bool IsActive { get; set; }

        [DisplayName("حذف شده؟")]
        public bool IsDelete { get; set; }

        //public IEnumerable<Role>? Roles { get; set; }
        //public List<int>? UserSelectedRoles { get; set; }
    }
}
