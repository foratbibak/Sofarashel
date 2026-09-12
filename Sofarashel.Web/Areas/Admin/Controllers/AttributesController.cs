using Microsoft.AspNetCore.Mvc;
using Sofarashel.Application.Services.Interfaces;
using Sofarashel.Domain.ViewModels.Products;
using Sofarashel.Ifra.Data.Static;
using Sofarashel.Web.Attributes;

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
        [PermissionChecker(PermissionName.ManageAttributes)]

        public async Task<IActionResult> Index(string? keyword)
        {
            var attributes = await _attributeFeatureServices.SearchAsync(keyword);
            return Json(attributes);
        }
        #endregion

        #region Create
        [HttpPost]
        [ValidateAntiForgeryToken]

        [PermissionChecker(PermissionName.AddAttribute)]

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
        [PermissionChecker(PermissionName.EditAttribute)]

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
        [PermissionChecker(PermissionName.EditAttribute)]

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
        [PermissionChecker(PermissionName.DeleteAttribute)]

        public async Task Delete(int id)
        {
            await _productServices.RemoveAttributeLinksAsync(id);
            await _attributeFeatureServices.DeleteFromLibraryAsync(id);
        }
        #endregion
    }
}