using eCommerce.Domain;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Infrastructure;

public class eCommerceDbContext : DbContext
{
    public eCommerceDbContext()
    {

    }
    public eCommerceDbContext(DbContextOptions<eCommerceDbContext> options) : base(options)
    {

    }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Invoice> Invoices { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Role>  Roles { get; set; }
    //public DbSet<InvoiceItem> InvoiceItems { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(@"Data Source =  (localdb)\MSSQLLocalDB; Database=CAEcomDbApr26; Integrated Security = True; Trust Server Certificate = True");
        }
    }
}
