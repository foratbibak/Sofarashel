using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using User_Login.Enum.User;
using User_Login.ViewModels.User;

namespace User_Login.ViewModels.Users
{
    public class UserFillterViewModel
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
