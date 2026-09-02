using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Sofarashel.Domain.ViewModels.Products
{
    public class ProductAttributeViewModel
    {
        [DisplayName("عنوان ویژگی")]
        [Required(ErrorMessage = "لطفا {0} را وارد فرمایید")]
        public string Title { get; set; }

        [DisplayName("مقدار")]
        [Required(ErrorMessage = "لطفا {0} را وارد فرمایید")]
        public string Value { get; set; }
    }
}