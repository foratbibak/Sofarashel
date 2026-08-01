using Microsoft.AspNetCore.Mvc;
using Sofarashel.Application.Mapper;
using Sofarashel.Application.Services.Interfaces;
using Sofarashel.Domain.Enums.Categories;
using Sofarashel.Domain.ViewModels.Categories;

namespace Sofarashel.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesController : Controller
    {
        private readonly ICategoryServices _categoryServices;

        public CategoriesController(ICategoryServices categoryServices)
        {
            _categoryServices = categoryServices;
        }

        #region Index
        // GET: Admin/Categories
        public async Task<IActionResult> Index()
        {
            return View(await _categoryServices.GetRootCategoriesAsync());
        }
        #endregion

        #region Children
        // GET: Admin/Categories/Children/5
        public async Task<IActionResult> Children(int parentId)
        {
            return View(await _categoryServices.GetChildrenAsync(parentId));
        }
        #endregion

        #region Create
        // GET: Admin/Categories/Create
        public async Task<IActionResult> Create()
        {
            AdminCreateCategoryViewModel adminCreateCategory = new AdminCreateCategoryViewModel
            {
                ParentCategories = await _categoryServices.GetSelectableParentsAsync(null)
            };
            return View(adminCreateCategory);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AdminCreateCategoryViewModel adminCreate)
        {
            if (ModelState.IsValid)
            {
                var result = await _categoryServices.CreateCategoryAsync(adminCreate);
                if (result == CreateCategoryResult.Success)
                {
                    return RedirectToAction(nameof(Index));
                }
                ViewBag.Error = result;
            }
            adminCreate.ParentCategories = await _categoryServices.GetSelectableParentsAsync(null);
            return View(adminCreate);
        }
        #endregion

        #region Edit
        // GET: Admin/Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _categoryServices.GetCategoryByIdForAdmin(id);
            if (category == null)
            {
                return NotFound();
            }

            var adminEdit = CategoryMapper.MapToEditCategoryViewModel(category);
            adminEdit.ParentCategories = await _categoryServices.GetSelectableParentsAsync(category.Id);
            return View(adminEdit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AdminEditCategoryViewModel category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var result = await _categoryServices.EditCategoryAsync(category);
                if (result == AdminEditCategoryResult.Success)
                {
                    return RedirectToAction(nameof(Index));
                }
                ViewBag.Error = result;
            }
            category.ParentCategories = await _categoryServices.GetSelectableParentsAsync(category.Id);
            return View(category);
        }
        #endregion

        #region Delete
        // GET: Admin/Categories/Delete/5
        public async Task Delete(int id)
        {
            await _categoryServices.DeleteCategoryAsync(id);
        }
        #endregion

        //public async Task<IActionResult> GetById(int id)
        //{
        //    var category = await _categoryServices.GetCategoryByIdForAdmin(id);

        //    if (category == null)
        //    {
        //        return NotFound();
        //    }

        //    return Json(category);
        //}
    }
}