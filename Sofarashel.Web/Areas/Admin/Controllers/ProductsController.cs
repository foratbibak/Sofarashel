using Microsoft.AspNetCore.Mvc;
using Sofarashel.Application.Services.Interfaces;
using Sofarashel.Domain.Enums.Products;
using Sofarashel.Domain.ViewModels.Products;
using Sofarashel.Infra.Data.Static;
using Sofarashel.Web.Attributes;

namespace Sofarashel.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly IProductServices _productServices;
        private readonly ICategoryServices _categoryServices;

        public ProductsController(
            IProductServices productServices,
            ICategoryServices categoryServices)
        {
            _productServices = productServices;
            _categoryServices = categoryServices;
        }

        #region Index
        [PermissionChecker(PermissionName.ManageProducts)]
        public async Task<IActionResult> Index(AdminProductFilterViewModel model)
        {
            var result = await _productServices.AdminFilterAsync(model);
            return Json(result);
        }
        #endregion

        #region GetProduct_By_Category
        [PermissionChecker(PermissionName.ManageProducts)]
        public async Task<IActionResult> ProductByCategory(int categoryId)
        {
            var products = await _productServices.GetProductsByCategoryAsync(categoryId);
            return Json(products);
        }
        #endregion

        #region Create
        [PermissionChecker(PermissionName.AddProducts)]
        public async Task<IActionResult> Create()
        {
            var categories = await _categoryServices.GetAllCategoriesAsync();
            return Json(categories);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionChecker(PermissionName.AddProducts)]
        public async Task<IActionResult> Create(AdminCreateProductViewModel adminCreate)
        {
            if (!ModelState.IsValid)
            {
                return Json(CreateProductResult.Error);
            }

            var result = await _productServices.CreateProductAsync(adminCreate);
            return Json(result);
        }
        #endregion

        #region Edit
        [PermissionChecker(PermissionName.EditProducts)]
        public async Task<IActionResult> Edit(int? id)
        {
            var model = await _productServices.GetEditViewModelAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            return Json(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionChecker(PermissionName.EditProducts)]
        public async Task<IActionResult> Edit(int id, AdminEditProductViewModel product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return Json(AdminEditProductResult.Error);
            }

            var result = await _productServices.EditProductAsync(product);
            return Json(result);
        }
        #endregion

        #region Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionChecker(PermissionName.DeleteProducts)]
        public async Task Delete(int id)
        {
            await _productServices.DeleteProductAsync(id);
        }
        #endregion

        #region Images
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionChecker(PermissionName.EditProducts)]
        public async Task UnlinkImage(int productId, int imageId)
        {
            await _productServices.UnlinkImageAsync(productId, imageId);
        }
        #endregion
    }
}