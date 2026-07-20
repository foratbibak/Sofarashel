using System.ComponentModel;

namespace Sofarashel.ViewModels.User
{
    public class UserViewModel
    {
        public int Id { get; set; }

        [DisplayName("نام")]
        public string? FirstName { get; set; }

        [DisplayName("نام خانوادگی")]
        public string? LastName { get; set; }

        [DisplayName("نام کاربری")]
        public string UserName { get; set; }
        [DisplayName("کلمه عبور")]
        public string Password { get; set; }

        [DisplayName("فعال/غیرفعال")]
        public bool IsActive { get; set; }

        [DisplayName("تاریخ ایجاد")]
        public DateTime CreatDate { get; set; }

        [DisplayName("تاریخ ویرایش")]
        public DateTime? UpdateDate { get; set; }

        public bool IsDelete { get; set; }
    }
}
