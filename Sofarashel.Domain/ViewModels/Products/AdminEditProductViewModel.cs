using Sofarashel.Domain.Models.Categories;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Sofarashel.Domain.ViewModels.Products
{
    public class AdminEditProductViewModel
    {
        public int Id { get; set; }

        [DisplayName("عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد فرمایید")]
        public string Title { get; set; }

        [DisplayName("توضیحات")]
        public string? Description { get; set; }

        [DisplayName("دسته‌بندی‌ها")]
        [Required(ErrorMessage = "لطفا حداقل یک {0} را انتخاب فرمایید")]
        public List<int> CategoryIds { get; set; } = new();

        public IEnumerable<Category>? CategoryOptions { get; set; }

        [DisplayName("ویژگی‌ها")]
        public List<ProductAttributeViewModel> Attributes { get; set; } = new();

        [DisplayName("عکس‌های انتخاب‌شده از کتابخونه")]
        public List<int> ImageIds { get; set; } = new();

        public int? MainImageId { get; set; }

        public List<ProductImageViewModel> ExistingImages { get; set; } = new();
    }
}