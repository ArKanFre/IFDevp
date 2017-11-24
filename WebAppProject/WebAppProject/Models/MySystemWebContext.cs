using System.Data.Entity;

namespace WebAppProject.Models
{
    public class MySystemWebContext : DbContext
    {
        public MySystemWebContext() : base("name=MySystemWebContext")
        {
            // A CADA MUDANÇA O DATABASE SERÁ APAGADO E CRIAREMOS OUTRO
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<MySystemWebContext>());
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Provider> Providers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Pedidos { get; set; }
        public DbSet<DetailsOrder> DetailsOrders { get; set; }

        public System.Data.Entity.DbSet<WebAppProject.Models.User> Users { get; set; }
    }
}