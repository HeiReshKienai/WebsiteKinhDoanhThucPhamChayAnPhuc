using WebsiteKinhDoanhThucPhamChayAnPhuc.Models;

namespace WebsiteKinhDoanhThucPhamChayAnPhuc.ViewModels {
    public class ProductViewModel {

        public List<Menu> Menus { get; set; }
        public List<Blog> Blogs { get; set; }
        public List<Product> Prods { get; set; }
        public String cateName { get; set; }
    }
}


