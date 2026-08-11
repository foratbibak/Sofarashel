using Microsoft.AspNetCore.Mvc;
using Sofarashel.Application.Services.Interfaces;

namespace Sofarashel.Web.Controllers
{
    public class GalleryController : Controller
    {
        private readonly ICategoryServices _categoryServices;

        public GalleryController(ICategoryServices categoryServices)
        {
            _categoryServices = categoryServices;
        }

        #region GetAll
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryServices.GetRootCategoriesAsync();
            return Json(categories);
        }
        #endregion

        #region GetSubCategories
        public async Task<IActionResult> GetSubCategories(int id)
        {
            var categories = await _categoryServices.GetSubCategoriesAsync(id);
            return Json(categories);
        }
        #endregion

        #region GetProducts
        public async Task<IActionResult> GetProducts(int id)
        {
            var products = await _categoryServices.GetProductsByParentAsync(id);
            return Json(products);
        }
        #endregion

        #region GetProduct
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _categoryServices.GetSingleProductAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return Json(product);
        }
        #endregion
    }
}