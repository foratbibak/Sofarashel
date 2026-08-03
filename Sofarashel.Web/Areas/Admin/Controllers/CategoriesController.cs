using Microsoft.AspNetCore.Mvc;
using Sofarashel.Application.Mapper;
using Sofarashel.Application.Services.Interfaces;
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
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryServices.GetRootCategoriesAsync();
            return Json(categories);
        }
        #endregion

        #region SubCategories
        // GET: Admin/Categories/SubCategories/5
        public async Task<IActionResult> SubCategories(int parentId)
        {
            var categories = await _categoryServices.GetSubCategoriesAsync(parentId);
            return Json(categories);
        }
        #endregion

        #region Create
        public async Task<IActionResult> Create()
        {
            var parents = await _categoryServices.GetSelectableParentsAsync(null);
            return Json(parents);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AdminCreateCategoryViewModel adminCreate)
        {
            var result = await _categoryServices.CreateCategoryAsync(adminCreate);
            return Json(result);
        }
        #endregion

        #region Edit
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

            var model = CategoryMapper.MapToEditCategoryViewModel(category);
            model.ParentCategories = await _categoryServices.GetSelectableParentsAsync(category.Id);

            return Json(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AdminEditCategoryViewModel category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            var result = await _categoryServices.EditCategoryAsync(category);
            return Json(result);
        }
        #endregion

        #region Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task Delete(int id)
        {
            await _categoryServices.DeleteCategoryAsync(id);
        }
        #endregion
    }
}