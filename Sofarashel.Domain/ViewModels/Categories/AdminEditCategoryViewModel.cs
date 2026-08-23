using Sofarashel.Domain.Models.Categories;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Sofarashel.Domain.ViewModels.Categories
{
    public class AdminEditCategoryViewModel
    {
        public int Id { get; set; }

        [DisplayName("عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد فرمایید")]
        public string Title { get; set; }

        [DisplayName("توضیحات")]
        public string? Description { get; set; }

        [DisplayName("آیا یک دسته‌بندی است؟")]
        public bool IsCategory { get; set; }

        [DisplayName("دسته اصلی")]
        public int? ParentId { get; set; }

        public string? MainImage { get; set; }

        public IEnumerable<Category>? ParentCategories { get; set; }
    }
}