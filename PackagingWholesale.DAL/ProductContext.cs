
using Microsoft.EntityFrameworkCore;
using PackagingWholesale.BLL.Products;

namespace PackagingWholesale.DAL
{
    public class ProductContext:DbContext
    {
        public DbSet<Product> Products { get; set; }

        public DbSet<PackagingWholesaleWarehouseStatus> PackagingWholesaleWarehouseStatus { get; set; }
        public ProductContext(DbContextOptions<ProductContext>options) :base(options)
        { 
        }
    }
}
