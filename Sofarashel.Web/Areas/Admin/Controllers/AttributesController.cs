using Microsoft.AspNetCore.Mvc;
using Sofarashel.Application.Services.Interfaces;

namespace Sofarashel.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AttributesController : Controller
    {
        private readonly IAttributeFeatureServices _attributeFeatureServices;
        private readonly IProductServices _productServices;

        public AttributesController(
            IAttributeFeatureServices attributeFeatureServices,
            IProductServices productServices)
        {
            _attributeFeatureServices = attributeFeatureServices;
            _productServices = productServices;
        }

        #region Index
        public async Task<IActionResult> Index(string? keyword)
        {
            var attributes = await _attributeFeatureServices.SearchAsync(keyword);
            return Json(attributes);
        }
        #endregion

        #region Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string title, string value)
        {
            if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(value))
            {
                return BadRequest("عنوان و مقدار الزامیه.");
            }

            var attribute = await _attributeFeatureServices.GetOrCreateAsync(title, value);
            return Json(attribute);
        }
        #endregion
        #region Edit
        public async Task<IActionResult> Edit(int id)
        {
            var attribute = await _attributeFeatureServices.GetByIdAsync(id);

            if (attribute == null)
            {
                return NotFound();
            }

            return Json(attribute);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, string title, string value)
        {
            if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(value))
            {
                return BadRequest("عنوان و مقدار الزامیه.");
            }

            var attribute = await _attributeFeatureServices.UpdateAsync(id, title, value);

            if (attribute == null)
            {
                return NotFound();
            }

            return Json(attribute);
        }
        #endregion

        #region Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task Delete(int id)
        {
            await _productServices.RemoveAttributeLinksAsync(id);
            await _attributeFeatureServices.DeleteFromLibraryAsync(id);
        }
        #endregion
    }
}