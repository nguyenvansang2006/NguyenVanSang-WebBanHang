using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Control.Model
{
    public class MyDBContext:DbContext
    {
        public MyDBContext() : base("name=WebsiteBanHangEntities") { }

        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
