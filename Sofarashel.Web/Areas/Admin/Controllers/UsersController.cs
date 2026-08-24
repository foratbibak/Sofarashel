using Bibaket.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sofarashel.Application.Services.Interfaces;
using Sofarashel.Data;
using Sofarashel.Domain.Enums.User;
using Sofarashel.Domain.ViewModels.User;
using Sofarashel.Enum.User;
using Sofarashel.Ifra.Data.Static;
using Sofarashel.Models.User;
using Sofarashel.ViewModels.Users;
using Sofarashel.Web.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sofarashel.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {
        private readonly IUserServices _userServices;
        private readonly IRoleServices _roleServices;

        public UsersController(IUserServices userServices,IRoleServices roleServices)
        {
            this._userServices = userServices;
            this._roleServices = roleServices;
        }

        #region Index
        [PermissionChecker(PermissionName.ManageUsers)]
        // GET: Admin/Users
        public async Task<IActionResult> Index(AdminUserFillterViewModel userFillter, string create = "false")
        {
            var lst = await _userServices.AdminFilterAsync(userFillter);
            ViewBag.Create = create;
            return View(lst);
        }
        #endregion


        #region  Create
        [PermissionChecker(PermissionName.AddUsers)]
        public async Task<IActionResult> Create()
        {
            var model = new CreateUserViewModel()
            {
                Roles = await _roleServices.GetAllRoleAsync()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionChecker(PermissionName.AddUsers)]
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userServices.CreateUserInAdminAsync(model);
                if (result == CreateUserResult.Success)
                {
                    return Redirect("/Admin/Users?Create=Success");
                }
                else
                {
                    ViewBag.Error = result;
                }
            }
            return View(model);
        }


        #endregion

        #region Edit
        [PermissionChecker(PermissionName.EditUsers)]
        // GET: Admin/Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userServices.GetUserForEditAsync(id.Value);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionChecker(PermissionName.EditUsers)]

        public async Task<IActionResult> Edit(int id, EditUserViewModel user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(user);
            }

            var result = await _userServices.EditUserAsync(user);

            if (result == AdminEditUserResult.Success)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewBag.Error = result;
            }
            return View(user);

        }

        #endregion


        //[PermissionChecker(PermissionName.ManageUsers)]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var user = await _userServices.GetUserForDeleteAsync(id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(user);
        //}

        //[PermissionChecker(PermissionName.ManageUsers)]
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    await _userServices.DeleteUserAsync(id);

        //    return RedirectToAction(nameof(Index));
        //}

        #region Details
        // GET: Admin/Users/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var user = await _context.Users
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(user);
        //}
        #endregion

    }
}
