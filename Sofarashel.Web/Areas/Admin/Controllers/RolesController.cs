using Bibaket.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sofarashel.Application.Services.Interfaces;
using Sofarashel.Data;
using Sofarashel.Domain.Models.Roles;
using Sofarashel.Domain.ViewModels.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sofarashel.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RolesController : Controller
    {
        private readonly GallaryDbcontext _context;
        private readonly IRoleServices _roleServices;
        private readonly IPermissionService _permissionService;

        public RolesController(GallaryDbcontext context, IRoleServices roleServices,IPermissionService permissionService)
        {
            _context = context;
            this._roleServices = roleServices;
            this._permissionService = permissionService;
        }

        #region Index
        // GET: Admin/Roles
        public async Task<IActionResult> Index()
        {
            return View(await _roleServices.GetAllRoleAsync());
        }
        #endregion

        #region Create
        // GET: Admin/Roles/Create
        public async Task<IActionResult> Create()
        {
            AdminCreateRoleViewModel adminCreateRole = new AdminCreateRoleViewModel
            {
                permissions = await _permissionService.GetAllPermissionAsync()
            };
            return View(adminCreateRole);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AdminCreateRoleViewModel adminCreate)
        {
            if (ModelState.IsValid)
            {
                await _roleServices.CreateRole(adminCreate);
                return RedirectToAction(nameof(Index));
            }
            adminCreate.permissions = await _permissionService.GetAllPermissionAsync();
            return View(adminCreate);
        }
        #endregion

        #region Edit
        // GET: Admin/Roles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            AdminEditRoleViewModel adminEdit = new AdminEditRoleViewModel();
            adminEdit.permissions = await _permissionService.GetAllPermissionAsync();
            var role = await _roleServices.GetRoleByIdForAdmin(id);
            adminEdit.RoleId = role.Id;
            if (role.RolePermissionMappings.Any())
            {
                adminEdit.PermissonSelectedIds = role.RolePermissionMappings.Select(r => r.PermissionId).ToList();
            }
            adminEdit.RoleName = role.RoleName;
            return View(adminEdit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AdminEditRoleViewModel role)
        {
            if (id != role.RoleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _roleServices.EditRoleAsync(role);
                return RedirectToAction(nameof(Index));
            }
            role.permissions = await _permissionService.GetAllPermissionAsync();
            return View(role);
        }

        #endregion

        #region Delete
        // GET: Admin/Roles/Delete/5
        public async Task Delete(int id)
        {
            await _roleServices.DeleteRoleAsync(id);
        }
        #endregion


    }
}
