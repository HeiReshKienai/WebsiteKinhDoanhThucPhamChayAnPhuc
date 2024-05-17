using WebsiteKinhDoanhThucPhamChayAnPhuc.Models;

namespace WebsiteKinhDoanhThucPhamChayAnPhuc.ViewModels {
    public class CartViewModel {
        public List<Menu> Menus { get; set; }
        public List<Blog> Blogs { get; set; }
        public List<CartItem> CartItems { get; set; }
        public List<CartDetail> CartDetails { get; set; }
        public List<CartHistory> CartHistorys { get; set; }
        public List<Cart> Carts { get; set; }




    }
}

