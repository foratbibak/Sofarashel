using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Sofarashel.Enum.User;
using Sofarashel.ViewModels.User;

namespace Sofarashel.ViewModels.Users
{
    public class AdminUserFillterViewModel
    {
        [DisplayName("نام")]
        public string? FirstName { get; set; }

        [DisplayName("نام خانوادگی")]
        public string? LastName { get; set; }

        [DisplayName("نام کاربری")]
        public string UserName { get; set; }

        public bool IsActive { get; set; }


        [Display(Name = "وضعیت حذف")]
        public FilterDeleteStatus DeleteStatus { get; set; }

        public List<UserViewModel> Users { get; set; } = new();


    }
}
