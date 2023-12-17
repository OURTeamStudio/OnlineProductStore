using Microsoft.EntityFrameworkCore;

namespace OnlineProductStore.Server.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region create categories
            modelBuilder.Entity<Category>().HasData(
                new Category()
                {
                    Id = 1,
                    Name = "Fruits",
                    Url = "fruits"
                },
                new Category()
                {
                    Id = 2,
                    Name = "Vegetables",
                    Url = "vegetables"
                }
            ); ;

            #endregion

            #region create products
            modelBuilder.Entity<Product>().HasData
                (
                    new Product()
                    {
                        Id = 1,
                        Title = "Помидори",
                        Description = "Дуже свіжі",
                        Price = 20,
                        ImageUrl = "https://encrypted-tbn3.gstatic.com/images?q=tbn:ANd9GcRemk0pOj3avWb06RvabQarkPJ-BUaZPIT9UjLWrwM6xL8TyRbj",
                        CategoryId = 2,
                    },
                    new Product()
                    {
                        Id = 2,
                        Title = "Огірочічки",
                        Description = "Смачно капець",
                        Price = 60,
                        ImageUrl = "https://www.fruit-market.com.ua/wp-content/uploads/2020/04/kornishon.jpg",
                        CategoryId = 2,
                    },
                    new Product()
                    {
                        Id = 3,
                        Title = "Помидори",
                        Description = "Дуже свіжі",
                        Price = 20,
                        ImageUrl = "https://encrypted-tbn3.gstatic.com/images?q=tbn:ANd9GcRemk0pOj3avWb06RvabQarkPJ-BUaZPIT9UjLWrwM6xL8TyRbj",
                        CategoryId = 2,
                    },
                    new Product()
                    {
                        Id = 4,
                        Title = "Огірочічки",
                        Description = "Смачно капець",
                        Price = 60,
                        ImageUrl = "https://www.fruit-market.com.ua/wp-content/uploads/2020/04/kornishon.jpg",
                        CategoryId = 2,
                    },
                     new Product()
                     {
                         Id = 5,
                         Title = "Помидори",
                         Description = "Дуже свіжі",
                         Price = 20,
                         ImageUrl = "https://encrypted-tbn3.gstatic.com/images?q=tbn:ANd9GcRemk0pOj3avWb06RvabQarkPJ-BUaZPIT9UjLWrwM6xL8TyRbj",
                         CategoryId = 2,
                     },
                    new Product()
                    {
                        Id = 6,
                        Title = "Огірочічки",
                        Description = "Смачно капець",
                        Price = 60,
                        ImageUrl = "https://www.fruit-market.com.ua/wp-content/uploads/2020/04/kornishon.jpg",
                        CategoryId = 2,
                    }
                );
            #endregion
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

    }
}
