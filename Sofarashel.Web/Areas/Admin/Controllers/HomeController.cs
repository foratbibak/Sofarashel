using Microsoft.AspNetCore.Mvc;

namespace Sofarashel.Web.Areas.Admin.Controllers
{
    public class HomeController : AdminBaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
