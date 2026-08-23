using Microsoft.AspNetCore.Mvc;
using Sofarashel.Application.Services.Interfaces;

namespace Sofarashel.Web.Controllers
{
    public class GalleryController : Controller
    {
        private readonly ICategoryServices _categoryServices;
        private readonly IProductServices _productServices;

        public GalleryController(ICategoryServices categoryServices, IProductServices productServices)
        {
            _categoryServices = categoryServices;
            _productServices = productServices;
        }

        #region Index
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryServices.GetRootCategoriesAsync();
            return Json(categories);
        }
        #endregion

        #region SubCategories
        public async Task<IActionResult> GetSubCategories(int id)
        {
            var categories = await _categoryServices.GetSubCategoriesAsync(id);
            return Json(categories);
        }
        #endregion

        #region Products
        public async Task<IActionResult> GetProducts(int id)
        {
            var products = await _productServices.GetProductsByCategoryAsync(id);
            return Json(products);
        }
        #endregion

        #region GetProduct
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _productServices.GetSingleProductAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return Json(product);
        }
        #endregion
    }
}