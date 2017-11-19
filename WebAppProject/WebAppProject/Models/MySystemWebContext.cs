using System.Data.Entity;

namespace WebAppProject.Models
{
    public class MySystemWebContext : DbContext
    {
        public MySystemWebContext() : base("name=MySystemWebContext")
        { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Provider> Providers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Stock> Stocks { get; set; }
    }
}