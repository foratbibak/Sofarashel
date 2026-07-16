using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace Sofarashel.Application.Extensions
{
    public static class IdentityExtensions
    {
        public static int GetUserId(this ClaimsPrincipal claims)
        {
            if (claims != null)
            {
                var data = claims.Claims.SingleOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                if (data != null)
                {
                    return int.Parse(data.Value);
                }
            }
            throw new ArgumentException("UserID Not Found");
        }
        public static int GetUserId(this IPrincipal principal)
            => (principal is ClaimsPrincipal claims) ? GetUserId(claims) : default;
     
    }
}
