using System;
using System.Collections.Generic;

namespace PackagingWholesale.BLL.Products.Buying
{
    public class ProductBuyingRequestProcessor
    {
        public ProductBuyingRequestProcessor()
        {
        }

        public ProductBuyingResult BuyProduct(ProductBuyingRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var result = new ProductBuyingResult();

            result.FirstName = request.FirstName;
            result.LastName = request.LastName;
            result.Email = request.Email;
            result.Date = request.Date;
            result.IsStatusOk = true;
            result.Errors = new List<string>();

            return result;
        }
    }
}