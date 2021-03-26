using PackagingWholesale.BLL.Interface;
using System;
using System.Collections.Generic;

namespace PackagingWholesale.BLL.Products.Buying.Processor
{
    public class ProductBuyingRequestProcessor
    {
        private IProductBuyingRepository _productBuyingRepository;
        private IProductRepository _productRepository;

        private static T Create<T>(ProductBuyingRequest request) where T : ProductBuyingBase, new()
        {
            return new T()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Date = request.Date
            };
        }
        public ProductBuyingRequestProcessor(IProductBuyingRepository productBuyingRepository,
            IProductRepository productRepository)
        {
            _productBuyingRepository = productBuyingRepository;
            _productRepository = productRepository;
        }

        public ProductBuyingResult BuyProduct(ProductBuyingRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            ProductBought productBought = Create<ProductBought>(request);
            productBought.ProuctId = request.ProductToBuy.Id;

            var result = Create<ProductBuyingResult>(request);
            if (_productRepository.IsProductAvailable(request.ProductToBuy))
            {
                result.PurchaseId = _productBuyingRepository.Save(productBought);
                result.StatusProductCode = ProductBuyingResultCode.Success;
            }
            else
            {
                result.StatusProductCode = ProductBuyingResultCode.GameIsNotAvailable;
            }
                
            return result;
        }
    }
}