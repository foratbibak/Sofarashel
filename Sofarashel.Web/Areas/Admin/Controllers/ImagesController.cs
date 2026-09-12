using Bibaket.Application.Utilities;
using Microsoft.AspNetCore.Mvc;
using Sofarashel.Application.Generator;
using Sofarashel.Application.Security;
using Sofarashel.Application.Services.Interfaces;
using Sofarashel.Ifra.Data.Static;
using Sofarashel.Web.Attributes;

namespace Sofarashel.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ImagesController : Controller
    {
        private readonly IImageServices _imageServices;
        private readonly IProductServices _productServices;
        private readonly IWebHostEnvironment _env;

        public ImagesController(
            IImageServices imageServices,
            IProductServices productServices,
            IWebHostEnvironment env)
        {
            _imageServices = imageServices;
            _productServices = productServices;
            _env = env;
        }

        #region Search
        [PermissionChecker(PermissionName.ManageImages)]

        public async Task<IActionResult> Index(string? keyword)
        {
            var images = await _imageServices.SearchAsync(keyword);
            return Json(images);
        }
        #endregion

        #region Upload
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionChecker(PermissionName.AddImage)]

        public async Task<IActionResult> Upload(IFormFile file)
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

            var image = await _imageServices.UploadAsync(fileName);

            return Json(image);
        }
        #endregion

        #region Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionChecker(PermissionName.DeleteImage)]

        public async Task Delete(int id)
        {
            await _productServices.RemoveImageLinksAsync(id);

            var image = await _imageServices.DeleteFromLibraryAsync(id);

            if (image != null)
            {
                var filePath = Path.Combine(_env.WebRootPath, "ProductImages", image.ImageUrl);
                FileHellper.DeletePath(filePath);
            }
        }
        #endregion
    }
}