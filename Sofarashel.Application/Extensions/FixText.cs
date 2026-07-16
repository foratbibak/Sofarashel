using System;
using System.Collections.Generic;
using System.Text;

namespace Sofarashel.Application.Extensions
{
    public static class FixText
    {
        public static string FixEmail(this string email)
        {
            return email.Trim().ToLower();
        }
        public static  string FixUserName(this string userName)
        {
            return userName.Trim().ToLower();
        }
    }
}
