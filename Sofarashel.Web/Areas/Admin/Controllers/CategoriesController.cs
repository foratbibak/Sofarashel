using Bibaket.Application.Utilities;
using Microsoft.AspNetCore.Mvc;
using Sofarashel.Application.Generator;
using Sofarashel.Application.Security;
using Sofarashel.Application.Services.Interfaces;
using Sofarashel.Domain.Enums.Categories;
using Sofarashel.Domain.ViewModels.Categories;
using Sofarashel.Ifra.Data.Static;
using Sofarashel.Web.Attributes;

namespace Sofarashel.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesController : Controller
    {
        private readonly ICategoryServices _categoryServices;
        private readonly IWebHostEnvironment _env;

        public CategoriesController(ICategoryServices categoryServices, IWebHostEnvironment env)
        {
            _categoryServices = categoryServices;
            _env = env;
        }

        #region Index
        [PermissionChecker(PermissionName.ManageCategories)]
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryServices.GetRootCategoriesAsync();
            return Json(categories);
        }
        #endregion

        #region SubCategories
        [PermissionChecker(PermissionName.ManageCategories)]
        public async Task<IActionResult> SubCategories(int parentId)
        {
            var categories = await _categoryServices.GetSubCategoriesAsync(parentId);
            return Json(categories);
        }
        #endregion

        #region GetProducts
        [PermissionChecker(PermissionName.ManageCategories)]
        public async Task<IActionResult> GetProducts(int parentId)
        {
            var products = await _categoryServices.GetProductsByParentAsync(parentId);
            return Json(products);
        }
        #endregion

        #region Create
        [PermissionChecker(PermissionName.AddCategories)]
        public async Task<IActionResult> Create()
        {
            var parents = await _categoryServices.GetParentCategoryOptionsAsync(null);
            return Json(parents);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionChecker(PermissionName.AddCategories)]
        public async Task<IActionResult> Create(AdminCreateCategoryViewModel adminCreate)
        {
            if (!ModelState.IsValid)
            {
                return Json(CreateCategoryResult.Error);
            }

            var result = await _categoryServices.CreateCategoryAsync(adminCreate);
            return Json(result);
        }
        #endregion

        #region Edit
        [PermissionChecker(PermissionName.EditCategories)]
        public async Task<IActionResult> Edit(int? id)
        {
            var model = await _categoryServices.GetEditViewModelAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            return Json(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionChecker(PermissionName.EditCategories)]
        public async Task<IActionResult> Edit(int id, AdminEditCategoryViewModel category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return Json(AdminEditCategoryResult.Error);
            }

            var result = await _categoryServices.EditCategoryAsync(category);
            return Json(result);
        }
        #endregion

        #region Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionChecker(PermissionName.DeleteCategories)]
        public async Task Delete(int id)
        {
            await _categoryServices.DeleteCategoryAsync(id);
        }
        #endregion

        #region Images
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionChecker(PermissionName.EditCategories)]
        public async Task<IActionResult> UploadImage(int categoryId, IFormFile file)
        {
            if (file == null || !file.ImageValidate())
            {
                return BadRequest("فرمت تصویر مجاز نیست.");
            }

            var uploadsFolder = Path.Combine(_env.WebRootPath, "CategoryImages");
            Directory.CreateDirectory(uploadsFolder);

            var fileName = NameGenerator.GenerateUniqName() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            await _categoryServices.AddImageAsync(categoryId, fileName);

            return Json(new { success = true, fileName });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionChecker(PermissionName.EditCategories)]
        public async Task DeleteImage(int id)
        {
            var image = await _categoryServices.GetImageByIdAsync(id);

            if (image != null)
            {
                var filePath = Path.Combine(_env.WebRootPath, "CategoryImages", image.ImageUrl);
                FileHellper.DeletePath(filePath);
            }

            await _categoryServices.DeleteImageAsync(id);
        }
        #endregion
    }
}