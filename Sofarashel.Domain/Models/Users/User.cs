using User_Login.Models.Common;

namespace Sofarashel.Models.User
{
    public class User:BaseEntity
    {
        //public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public bool IsActive { get; set; }



    }
}
