using System.ComponentModel;

namespace Sofarashel.Domain.ViewModels.Products
{
    public class ProductImageViewModel
    {
        public int ImageId { get; set; }

        [DisplayName("آدرس تصویر")]
        public string ImageUrl { get; set; }

        [DisplayName("تصویر اصلی؟")]
        public bool IsMain { get; set; }
    }
}