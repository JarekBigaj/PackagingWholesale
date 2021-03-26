using PackagingWholesale.BLL.Interface;
using PackagingWholesale.BLL.Products;
using System.Collections.Generic;
using System.Linq;

namespace PackagingWholesale.DAL.Repository
{
    public class ProductRepository : IProductRepository
    {
        private ProductContext _productContext;
        public ProductRepository(ProductContext productContext)
        {
            _productContext = productContext;
        }
        public IEnumerable<Product> GetAll()
        {
            return _productContext.Products.ToList();
        }

        public bool IsProductAvailable(Product product)
        {
            var packagingWholesaleWarehouseStatus = _productContext
                .PackagingWholesaleWarehouseStatus
                .FirstOrDefault(x => x.ProductId == product.Id);

            if (packagingWholesaleWarehouseStatus == null)
                return false;

            return packagingWholesaleWarehouseStatus.Amount > 0;
        }
    }
}
