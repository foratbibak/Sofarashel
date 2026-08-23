using Sofarashel.Domain.Models.Categories;
using Sofarashel.Domain.Models.Products;
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

        [DisplayName("جنس")]
        public string? Material { get; set; }

        [DisplayName("نوع پارچه")]
        public string? FabricType { get; set; }

        [DisplayName("رنگ")]
        public string? Color { get; set; }

        [DisplayName("سبک")]
        public string? Style { get; set; }

        [DisplayName("طول")]
        public decimal? Length { get; set; }

        [DisplayName("عرض")]
        public decimal? Width { get; set; }

        [DisplayName("ارتفاع")]
        public decimal? Height { get; set; }

        [DisplayName("دسته‌بندی‌ها")]
        [Required(ErrorMessage = "لطفا حداقل یک {0} را انتخاب فرمایید")]
        public List<int> CategoryIds { get; set; } = new();

        public IEnumerable<Category>? CategoryOptions { get; set; }

        public string? MainImage { get; set; }

        public IEnumerable<ProductImage>? Images { get; set; }
    }
}