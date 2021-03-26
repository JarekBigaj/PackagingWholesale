using PackagingWholesale.BLL.Interface;
using System;
using System.Collections.Generic;

namespace PackagingWholesale.BLL.Products.Buying.Processor
{
    public class ProductBuyingRequestProcessor
    {
        private IProductBuyingRepository _productBuyingRepository;
        public ProductBuyingRequestProcessor(IProductBuyingRepository productBuyingRepository)
        {
            _productBuyingRepository = productBuyingRepository;
        }

        public ProductBuyingResult BuyProduct(ProductBuyingRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            ProductBought productBought = new ProductBought();
            productBought.FirstName = request.FirstName;
            productBought.LastName = request.LastName;
            productBought.Email = request.Email;
            productBought.Date = request.Date;

            _productBuyingRepository.Save(productBought);

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