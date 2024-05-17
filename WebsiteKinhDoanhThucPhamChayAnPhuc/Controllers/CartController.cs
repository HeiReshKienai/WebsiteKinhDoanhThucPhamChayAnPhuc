using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebsiteKinhDoanhThucPhamChayAnPhuc.Models;

using WebsiteKinhDoanhThucPhamChayAnPhuc.ViewModels;

namespace WebsiteKinhDoanhThucPhamChayAnPhuc.Controllers {
    public class CartController : Controller {
        private readonly WebsiteKinhDoanhThucPhamChayAnPhucContext _context;

        private const string CartSession = "CartSession";
        private const string CartSession2 = "CartSession2";
       
        public CartController(WebsiteKinhDoanhThucPhamChayAnPhucContext context ) {
            _context = context;
           
        }
        public async Task<IActionResult> Index() {
            var menus = await _context.Menus.Where(m => m.Hide == 0).OrderBy(m =>
            m.Order).ToListAsync();
            var blogs = await _context.Blogs.Where(m => m.Hide == 0).OrderBy(m =>
            m.Order).Take(2).ToListAsync();

            var cart = HttpContext.Session.GetString(CartSession);

            var list = new List<CartItem>();
            if (!string.IsNullOrEmpty(cart)) {
                list = JsonConvert.DeserializeObject<List<CartItem>>(cart);

            }


            var cart2 = HttpContext.Session.GetString(CartSession2);

            var carts = await _context.Carts.Where(m => m.Hide == 0).OrderBy(m =>
            m.Datebegin).ToListAsync();


            var cartdetail = await _context.CartDetails.Where(m => m.Hide == 0).ToListAsync();



            var  list2 = new List<CartHistory>();
            if (!string.IsNullOrEmpty(cart2)) {
                list2 = JsonConvert.DeserializeObject<List<CartHistory>>(cart2);
            }



            var cartViewModel = new CartViewModel {
                Menus = menus,
                Blogs = blogs,
                CartItems = list,


                Carts= carts,
                CartHistorys =list2,
                CartDetails =cartdetail

            };
            return View(cartViewModel);
        }

        public IActionResult AddItem(int ProductId, int Quantity) {
            var product = _context.Products.Find(ProductId);
            var cart = HttpContext.Session.GetString(CartSession);
            if (!string.IsNullOrEmpty(cart)) {
                var list = JsonConvert.DeserializeObject<List<CartItem>>(cart);
                if (list.Exists(x => x.Product.IdPro == ProductId)) {
                    foreach (var item in list) {
                        if (item.Product.IdPro == ProductId) {
                            item.Quantity += Quantity;
                        }
                    }
                } else {
                    var item = new CartItem();
                    item.Product = product;
                    item.Quantity = Quantity;
                    list.Add(item);
                }
                HttpContext.Session.SetString(CartSession, JsonConvert.SerializeObject(list));
            } else {
                var item = new CartItem();
                item.Product = product;
                item.Quantity = Quantity;
                var list = new List<CartItem>();
                list.Add(item);
                HttpContext.Session.SetString(CartSession, JsonConvert.SerializeObject(list));
            }
            return RedirectToAction("Index");
        }

        public IActionResult DeleteAll() {
            HttpContext.Session.Remove(CartSession);
            return Json(new {
                status = true
            });
        }
        public IActionResult Delete(int id) {
            var sessionCart =JsonConvert.DeserializeObject<List<CartItem>>(HttpContext.Session.GetString(CartSession));
            sessionCart.RemoveAll(x => x.Product.IdPro == id);
            HttpContext.Session.SetString(CartSession,JsonConvert.SerializeObject(sessionCart));
            return Json(new {
                status = true
            });
        }
        public IActionResult Update(string cartModel) {
            var jsonCart = JsonConvert.DeserializeObject<List<CartItem>>(cartModel);
            var sessionCart =JsonConvert.DeserializeObject<List<CartItem>>(HttpContext.Session.GetString(CartSession));
            foreach (var item in sessionCart) {
                var jsonItem = jsonCart.SingleOrDefault(x => x.Product.IdPro ==item.Product.IdPro);
                if (jsonItem != null) {
                    item.Quantity = jsonItem.Quantity;
                }
            }
            HttpContext.Session.SetString(CartSession,JsonConvert.SerializeObject(sessionCart));
            return Json(new {
                status = true
            });

        }
        [HttpGet]
        public async Task<IActionResult> Payment() {
            var menus = await _context.Menus.Where(m => m.Hide == 0).OrderBy(m =>
            m.Order).ToListAsync();
            var blogs = await _context.Blogs.Where(m => m.Hide == 0).OrderBy(m =>
            m.Order).Take(2).ToListAsync();
            var cart = HttpContext.Session.GetString(CartSession);
            var list = new List<CartItem>();
            if (!string.IsNullOrEmpty(cart)) {
                list = JsonConvert.DeserializeObject<List<CartItem>>(cart);
            }
            var cartViewModel = new CartViewModel {
                Menus = menus,
                Blogs = blogs,
                CartItems = list
            };
            return View(cartViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Payment(string PaymentMethod) {
            var order = new Cart();
            order.Hide = 0;
            order.Datebegin = DateTime.Now;
            var users = new User();
            if (User.Identity.IsAuthenticated) {
                string username = User.Identity.Name;
                if (username != null) users = await _context.Users.FirstOrDefaultAsync(m =>
                m.Username == username);
            }
            order.IdUsers = users.IdUsers;
            try {
                _context.Carts.Add(order);
                _context.SaveChanges();
                var id = order.IdCart;
                var cart =
                JsonConvert.DeserializeObject<List<CartItem>>(HttpContext.Session.GetString(CartSession));
                foreach (var item in cart) {
                    var detail = new CartDetail();
                    detail.IdPro = item.Product.IdPro;
                    detail.IdCart = id;
                    detail.SoldNum = item.Quantity;
                    detail.Hide = 0;
                    _context.CartDetails.Add(detail);
                    _context.SaveChanges();
                }
            } catch (Exception ex) {
                throw;
            }
            if (PaymentMethod == "COD") {
                // Thực hiện các hành động cần thiết cho thanh toán COD
                return Redirect("/hoan-thanh");
            } else if (PaymentMethod == "VNPAY") {
                // Thực hiện các hành động cần thiết cho thanh toán VNPAY
                return Redirect("/hoan-thanh");
            }
            return View("Error");

        }

        public async Task<IActionResult> Success() {
            var menus = await _context.Menus.Where(m => m.Hide == 0).OrderBy(m =>
            m.Order).ToListAsync();
            var blogs = await _context.Blogs.Where(m => m.Hide == 0).OrderBy(m =>
            m.Order).Take(2).ToListAsync();
            var cartViewModel = new CartViewModel {
                Menus = menus,
                Blogs = blogs
            };
            return View(cartViewModel);
        }



   






    }
}