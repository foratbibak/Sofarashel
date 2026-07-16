using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace User_Login.ViewModels.Users
{
    public class CreateUserViewModel
    {
        [DisplayName("نام")]
        public string? FirstName { get; set; }

        [DisplayName("نام خانوادگی")]
        public string? LastName { get; set; }

        [DisplayName("نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} را وارد فرمایید")]
        public string UserName { get; set; }

        [DisplayName("کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد فرمایید")]
        public string Password { get; set; }

        [DisplayName("فعال/غیرفعال")]
        public bool IsActive { get; set; }
    }
}
