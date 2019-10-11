using Microsoft.EntityFrameworkCore;
 
namespace ecommerce.Models
{
    public class ProductContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public ProductContext(DbContextOptions options) : base(options) { }
        public DbSet<Customer> Customers {get;set;}
        public DbSet<Product> Products {get;set;}

        public DbSet<Transaction> Transactions {get;set;}
    }
}
