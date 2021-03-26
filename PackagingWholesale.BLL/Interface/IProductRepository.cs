
using PackagingWholesale.BLL.Products;
using System.Collections.Generic;

namespace PackagingWholesale.BLL.Interface
{
    public interface IProductRepository
    {
        bool IsProductAvailable(Product product);

        IEnumerable<Product> GetAll();
    }
}
