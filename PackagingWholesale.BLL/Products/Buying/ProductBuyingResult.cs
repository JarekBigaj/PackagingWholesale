using System;
using System.Collections.Generic;

namespace PackagingWholesale.BLL.Products.Buying
{
    public class ProductBuyingResult : ProductBuyingBase
    {
        public bool IsStatusOk { get; set; }
        public List<string> Errors { get; set; }
    }
}