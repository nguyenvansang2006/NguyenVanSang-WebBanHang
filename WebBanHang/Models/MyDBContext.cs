

using System.Data.Entity;
using WebBanHang.Context;

namespace WebBanHang.Models
{
    public class MyDBContext:DbContext
    {
        public MyDBContext() : base("name=WebsiteBanHangEntities") { }

        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categoris { get; set; }
    }
}
