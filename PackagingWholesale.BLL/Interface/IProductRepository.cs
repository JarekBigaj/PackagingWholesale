
using PackagingWholesale.BLL.Products;

namespace PackagingWholesale.BLL.Interface
{
    public interface IProductRepository
    {
        bool IsProductAvailable(Product product);
    }
}
