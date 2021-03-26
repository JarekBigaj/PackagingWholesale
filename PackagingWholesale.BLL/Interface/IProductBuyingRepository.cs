using PackagingWholesale.BLL.Products.Buying;

namespace PackagingWholesale.BLL.Interface
{
    public interface IProductBuyingRepository
    {
        public void Save(ProductBought productBought);
    }
}
