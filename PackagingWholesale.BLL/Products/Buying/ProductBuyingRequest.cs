using PackagingWholesale.BLL.Interface;
using System;

namespace PackagingWholesale.BLL.Products.Buying
{
    public class ProductBuyingRequest : ProductBuyingBase
    {
        public Product ProductToBuy { get; set; }
    }
}