using System;
using Xunit;
using PackagingWholesale.BLL.Products.Buying;


namespace PackagingWholesale.BLL.Tests
{
    public class ProductBuyingRequestProcessorTests
    {
        private ProductBuyingRequestProcessor _processor;
        private ProductBuyingRequest _request;
        public ProductBuyingRequestProcessorTests()
        {
            _processor = new ProductBuyingRequestProcessor();

            //Arrange
            _request = new ProductBuyingRequest()
            {
                FirstName = "Jarek",
                LastName = "Bigaj",
                Email = "jarek@example.com",
                Date = DateTime.Now
            };
        }

        [Fact]
        public void ShouldReturnProductBuyingResultWithRequestValues()
        {

            //Act
            ProductBuyingResult result = _processor.BuyProduct(_request);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(_request.FirstName, result.FirstName);
            Assert.Equal(_request.LastName, result.LastName);
            Assert.Equal(_request.Email, result.Email);
            Assert.Equal(_request.Date, result.Date);
        }

        [Fact]
        public void ShouldThrowExceptionIfResultIsNull()
        {
            var exception = Assert.Throws<ArgumentNullException>(
                    () => _processor.BuyProduct(null)
            );

            Assert.Equal("request", exception.ParamName);
        }

        [Fact]
        public void ShouldReturnStatusTrueWhenSendedCorrectValues()
        {
            //Act
            ProductBuyingResult result = _processor.BuyProduct(_request);

            //Assert
            Assert.Equal(true, result.IsStatusOk);
            Assert.Equal(0, result.Errors.Count);

        }
    }
}
