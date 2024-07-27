using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanGiayOnline.Areas.Admin.ViewModels;
using WebBanGiayOnline.Models;
using WebBanGiayOnline.Models.EF;

namespace WebBanGiayOnline.Areas.Admin.Controllers
{
    public class ProductsController : Controller
    {        
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/Products
        public ActionResult Index(int? page)
        {
            IEnumerable<Product> items = db.Products.OrderByDescending(x => x.ProductId);
            // Strart Tạo phân trang trong quản lý danh sách sản phẩm 
            var pageSize = 10;
            if (page == null)
            {
                page = 1;
            }
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            items = items.ToPagedList(pageIndex, pageSize);
            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;
            // End Tạo phân trang trong quản lý danh sách sản phẩm
            return View(items);
        }
        //public ActionResult Add()
        //{
        //    var model = new ProductViewModel
        //    {
        //        ProductDetails = new List<ProductDetail>() // Khởi tạo danh sách để tránh lỗi null
        //    };
        //    ViewBag.ProductCategory = new SelectList(db.ProductCategories.ToList(), "Id", "ProductCategoryName");
        //    ViewBag.ProductGender = new SelectList(db.ProductGenders.ToList(), "Id", "ProductGenderName");
        //    ViewBag.Color = new SelectList(db.Colors.ToList(), "ColorId", "ColorName");
        //    ViewBag.Size = new SelectList(db.Sizes.ToList(), "SizeId", "SizeName");
        //    return View(model);
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Add(ProductViewModel viewModel)
        //{
        //    return View(viewModel);
        //}

        public ActionResult Add()
        {
            var viewModel = new ProductViewModel
            {
                Product = new Product(),
                ProductDetails = new List<ProductDetailViewModel>() // Khởi tạo ProductDetails
            };
            ViewBag.ProductCategory = new SelectList(db.ProductCategories.ToList(), "Id", "ProductCategoryName");
            ViewBag.ProductGender = new SelectList(db.ProductGenders.ToList(), "Id", "ProductGenderName");
            ViewBag.Colors = new SelectList(db.Colors, "ColorId", "ColorName");
            ViewBag.Sizes = new SelectList(db.Sizes, "SizeId", "SizeName");
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(ProductViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(viewModel.Product);
                db.SaveChanges();

                foreach (var detail in viewModel.ProductDetails)
                {
                    var productDetail = new ProductDetail
                    {
                        ProductId = viewModel.Product.ProductId,
                        ColorId = detail.ColorId,
                        SizeId = detail.SizeId,
                        Stock = detail.Stock
                    };
                    db.ProductDetails.Add(productDetail);
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductCategory = new SelectList(db.ProductCategories.ToList(), "Id", "ProductCategoryName");
            ViewBag.ProductGender = new SelectList(db.ProductGenders.ToList(), "Id", "ProductGenderName");
            ViewBag.Colors = new SelectList(db.Colors, "ColorId", "ColorName");
            ViewBag.Sizes = new SelectList(db.Sizes, "SizeId", "SizeName");
            return View(viewModel);
        }
    }
}