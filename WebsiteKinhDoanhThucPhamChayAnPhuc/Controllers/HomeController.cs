using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebsiteKinhDoanhThucPhamChayAnPhuc.Models;
using WebsiteKinhDoanhThucPhamChayAnPhuc.ViewModels;

namespace WebsiteKinhDoanhThucPhamChayAnPhuc.Controllers {
    public class HomeController : Controller {
        private readonly WebsiteKinhDoanhThucPhamChayAnPhucContext _context;
        public HomeController(WebsiteKinhDoanhThucPhamChayAnPhucContext context) {
            _context = context;
        }
        public async Task<IActionResult> Index() {
            var menus = await _context.Menus.Where(m => m.Hide == 0).OrderBy(m => m.Order).ToListAsync();
            var blogs = await _context.Blogs.Where(m => m.Hide == 0).OrderBy(m => m.Order).Take(2).ToListAsync();
            var slides = await _context.Sliders.Where(m => m.Hide == 0).OrderBy(m => m.Order).ToListAsync();

            var giavi_prods = await _context.Products.Where(m => m.Hide == 0 && m.IdCat == 1).OrderBy(m => m.Order).Take(3).ToListAsync();
            var giavi_cate_prods = await _context.Catologies.Where(m => m.IdCat == 1).FirstOrDefaultAsync();

            var anlien_prods = await _context.Products.Where(m => m.Hide == 0 && m.IdCat == 2).OrderBy(m => m.Order).Take(3).ToListAsync();
            var anlien_cate_prods = await _context.Catologies.Where(m => m.IdCat == 2).FirstOrDefaultAsync();

            var donggoi_prods = await _context.Products.Where(m => m.Hide == 0 && m.IdCat == 3).OrderBy(m => m.Order).Take(3).ToListAsync();
            var donggoi_cate_prods = await _context.Catologies.Where(m => m.IdCat == 3).FirstOrDefaultAsync();

            var hanglon_prods = await _context.Products.Where(m => m.Hide == 0 && m.IdCat == 4).OrderBy(m => m.Order).Take(3).ToListAsync();
            var hanglon_cate_prods = await _context.Catologies.Where(m => m.IdCat == 4).FirstOrDefaultAsync();

            var hanglanh_prods = await _context.Products.Where(m => m.Hide == 0 && m.IdCat == 5).OrderBy(m => m.Order).Take(3).ToListAsync();
            var hanglanh_cate_prods = await _context.Catologies.Where(m => m.IdCat == 5).FirstOrDefaultAsync();

            var hangkho_prods = await _context.Products.Where(m => m.Hide == 0 && m.IdCat == 6).OrderBy(m => m.Order).Take(3).ToListAsync();
            var hangkho_cate_prods = await _context.Catologies.Where(m => m.IdCat == 6).FirstOrDefaultAsync();


            var viewModel = new HomeViewModel {
                Menus = menus,
                Blogs = blogs,
                Sliders = slides,

                GiaViProds =giavi_prods,
                AnLienProds =anlien_prods,
                DongGoiProds = donggoi_prods,
                HangLonProds = hanglon_prods,
                HangLanhProds = hanglanh_prods,
                HangKhoProds = hangkho_prods,

                GiaViCateProds = giavi_cate_prods,
                AnLienCateProds = anlien_cate_prods,
                DongGoiCateProds = donggoi_cate_prods,
                HangLonCateProds = hanglon_cate_prods,
                HangLanhCateProds =hanglanh_cate_prods,
                HangKhoCateProds = hangkho_cate_prods,

                
            };
            return View(viewModel);
        }
        public async Task<IActionResult> _MenuPartial() {
            return PartialView();
        }
        public async Task<IActionResult> _BlogPartial() {
            return PartialView();
        }
        public async Task<IActionResult> _SlidePartial() {
            return PartialView();
        }
        public async Task<IActionResult> _ProductPartial() {
            return PartialView();
        }
    }
}
