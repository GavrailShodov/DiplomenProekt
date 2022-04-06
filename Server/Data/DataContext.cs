using Microsoft.EntityFrameworkCore;

namespace WebAplicationForServices.Server.Data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(

             new Product
             {
                 Id = 1,
                 Title = "Az sum produkt1",
                 Description = "Az sum deskription 1 dasdasdasdasdsadasdasdasdasads",
                 ImageUrl = "https://images.pexels.com/photos/90946/pexels-photo-90946.jpeg?auto=compress&cs=tinysrgb&dpr=1&w=500",
                 Price = 1.2m,
                 UserId = 1
             },
         new Product
         {
             Id = 2,
             Title = "Usluga - Kosene na treva",
             Description = "Az sum deskription 2 dasdasdasdasdsadasdasdasdasads",
             ImageUrl = "https://napravisam.net/wp-content/uploads/1246.jpg",
             Price = 1.2m,
             UserId = 1
         },
         new Product
         {
             Id = 3,
             Title = "Az sum produkt3",
             Description = "Az sum deskription 3 dasdasdasdasdsadasdasdasdasads",
             ImageUrl = "https://images.pexels.com/photos/90946/pexels-photo-90946.jpeg?auto=compress&cs=tinysrgb&dpr=1&w=500",
             Price = 1.2m,
             UserId = 1
         }
               );
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Order> Orders { get; set; }
    }
}
