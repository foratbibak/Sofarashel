using Bibaket.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;
using Sofarashel.Application.Extensions;
using Sofarashel.Application.Services.Interfaces;

namespace Sofarashel.Web.Attributes
{
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Method)]
    public class PermissionCheckerAttribute(string permissionName) : Attribute, IAsyncAuthorizationFilter
    {
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var permissionservice = context.HttpContext.RequestServices.GetService<IPermissionService>();

            var returnUrl = context.HttpContext.Request.Path
                   + context.HttpContext.Request.QueryString;

            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                var userId = context.HttpContext.User.GetUserId();

                bool userHaveAccsess=await permissionservice.CheckUserPermission(userId,permissionName);

                if (!userHaveAccsess)
                {
                    context.HttpContext.Response.Redirect(
                        $"/Admin/AccsesDenided?returnUrl={Uri.EscapeDataString(returnUrl)}"
                        );

                }
            }
            else
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;

                context.HttpContext.Response.Redirect(
                    $"/Admin/AccsesDenided?returnUrl={Uri.EscapeDataString(returnUrl)}"
                );
            }
        }
    }
}
