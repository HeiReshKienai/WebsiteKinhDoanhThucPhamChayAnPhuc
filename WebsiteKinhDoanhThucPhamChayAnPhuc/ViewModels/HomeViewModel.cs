using WebsiteKinhDoanhThucPhamChayAnPhuc.Models;

namespace WebsiteKinhDoanhThucPhamChayAnPhuc.ViewModels {
    public class HomeViewModel {
        public List<Menu> Menus { get; set; }
        public List<Blog> Blogs { get; set; }
        public List<Slider> Sliders { get; set; }
        public List<Product> GiaViProds { get; set; }
        public List<Product> AnLienProds { get; set; }
        public List<Product> DongGoiProds { get; set; }
        public List<Product> HangLonProds { get; set; }
        public List<Product> HangLanhProds { get; set; }
        public List<Product> HangKhoProds { get; set; }

        public Catology GiaViCateProds { get; set; }
        public Catology AnLienCateProds { get; set; }
        public Catology DongGoiCateProds { get; set; }
        public Catology HangLonCateProds { get; set; }
        public Catology HangLanhCateProds { get; set; }
        public Catology HangKhoCateProds { get; set; }

    }
}
