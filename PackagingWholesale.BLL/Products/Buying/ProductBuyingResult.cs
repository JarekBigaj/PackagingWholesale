using System;
using System.Collections.Generic;

namespace PackagingWholesale.BLL.Products.Buying
{
    public enum ProductBuyingResultCode
    {
        Success = 0,
        GameIsNotAvailable = 1
    }
    public class ProductBuyingResult : ProductBuyingBase
    {
        public ProductBuyingResultCode StatusProductCode { get; set; }
        public int? PurchaseId { get; set; }
    }
}