using Bibaket.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sofarashel.Application.Services.Interfaces;
using Sofarashel.Data;
using Sofarashel.Domain.ViewModels.User;
using Sofarashel.Enum.User;
using Sofarashel.Models.User;
using Sofarashel.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sofarashel.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {
        private readonly GallaryDbcontext _context;
        private readonly IUserServices _userServices;
        private readonly IRoleServices _roleServices;

        public UsersController(GallaryDbcontext context,IUserServices userServices,IRoleServices roleServices)
        {
            _context = context;
            this._userServices = userServices;
            this._roleServices = roleServices;
        }

        #region Index
        // GET: Admin/Users
        public async Task<IActionResult> Index(AdminUserFillterViewModel userFillter, string create = "false")
        {
            var lst = await _userServices.AdminFilterAsync(userFillter);
            ViewBag.Create = create;
            return View(lst);
        }
        #endregion

        #region Details
        // GET: Admin/Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }
        #endregion

        // GET: Admin/Users/Create
        public async  Task<IActionResult> Create()
        {
            var model = new CreateUserViewModel()
            {
                Roles = await _roleServices.GetAllRoleAsync()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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
        public async Task<IActionResult> Edit(int id, EditUserViewModel editUser,User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Admin/Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Admin/Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
