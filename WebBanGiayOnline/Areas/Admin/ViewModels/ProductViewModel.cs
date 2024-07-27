using System.Collections.Generic;
using WebBanGiayOnline.Models.EF;

namespace WebBanGiayOnline.Areas.Admin.ViewModels
{
    public class ProductViewModel
    {
        public Product Product { get; set; }
        public List<ProductDetailViewModel> ProductDetails { get; set; }
        public List<ProductDetailImage> ProductDetailImages { get; set; }
    }

    public class ProductDetailViewModel
    {
        public int ProductDetailId { get; set; }
        public int ColorId { get; set; }
        public int SizeId { get; set; }
        public int Stock { get; set; }
    }

}