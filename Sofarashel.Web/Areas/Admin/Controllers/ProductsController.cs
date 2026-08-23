using Bibaket.Application.Utilities;
using Microsoft.AspNetCore.Mvc;
using Sofarashel.Application.Generator;
using Sofarashel.Application.Security;
using Sofarashel.Application.Services.Interfaces;
using Sofarashel.Domain.Enums.Products;
using Sofarashel.Domain.ViewModels.Products;
using Sofarashel.Ifra.Data.Static;
using Sofarashel.Web.Attributes;

namespace Sofarashel.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly IProductServices _productServices;
        private readonly ICategoryServices _categoryServices;
        private readonly IWebHostEnvironment _env;

        public ProductsController(
            IProductServices productServices,
            ICategoryServices categoryServices,
            IWebHostEnvironment env)
        {
            _productServices = productServices;
            _categoryServices = categoryServices;
            _env = env;
        }

        #region Index
        [PermissionChecker(PermissionName.ManageProducts)]
        public async Task<IActionResult> Index()
        {
            var products = await _productServices.GetAllProductsAsync();
            return Json(products);
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

        #region Create Product
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

        #region Edit Product
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

        #region Delete Product
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionChecker(PermissionName.DeleteProducts)]
        public async Task Delete(int id)
        {
            await _productServices.DeleteProductAsync(id);
        }
        #endregion

        #region Upload MainImage

        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionChecker(PermissionName.EditProducts)]
        public async Task<IActionResult> UploadImage(int productId, IFormFile file)
        {
            if (file == null || !file.ImageValidate())
            {
                return BadRequest("فرمت تصویر مجاز نیست.");
            }

            var uploadsFolder = Path.Combine(_env.WebRootPath, "ProductImages");
            Directory.CreateDirectory(uploadsFolder);

            var fileName = NameGenerator.GenerateUniqName() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            await _productServices.AddImageAsync(productId, fileName);

            return Json(new { success = true, fileName });
        }
        #endregion

        #region Delete Image
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionChecker(PermissionName.EditProducts)]
        public async Task DeleteImage(int id)
        {
            var image = await _productServices.GetImageByIdAsync(id);

            if (image != null)
            {
                var filePath = Path.Combine(_env.WebRootPath, "ProductImages", image.ImageUrl);
                FileHellper.DeletePath(filePath);
            }

            await _productServices.DeleteImageAsync(id);
        }
    }
        #endregion
}