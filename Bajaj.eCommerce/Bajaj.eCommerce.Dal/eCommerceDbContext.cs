using Bajaj.eCommerce.Entities;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Bajaj.eCommerce.Dal;

public class eCommerceDbContext:DbContext
{
    public eCommerceDbContext()
    {

    }
    public eCommerceDbContext(DbContextOptions<eCommerceDbContext> options) : base(options)
    {

    }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set;} 
    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Invoice> Invoices { get; set; }
    public DbSet<InvoiceItem> InvoiceItems { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(@"Data Source =  (localdb)\MSSQLLocalDB; Database=BajajEcomDbApr26; Integrated Security = True; Trust Server Certificate = True");
        }
    }
}
