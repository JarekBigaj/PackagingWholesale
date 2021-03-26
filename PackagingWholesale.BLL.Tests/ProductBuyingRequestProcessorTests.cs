using System;
using Xunit;
using PackagingWholesale.BLL.Products.Buying;
using PackagingWholesale.BLL.Products.Buying.Processor;
using PackagingWholesale.BLL.Interface;
using Moq;

namespace PackagingWholesale.BLL.Tests
{
    public class ProductBuyingRequestProcessorTests
    {
        private Mock<IProductBuyingRepository> _repositoryMock;
        private ProductBuyingRequestProcessor _processor;
        private ProductBuyingRequest _request;
        public ProductBuyingRequestProcessorTests()
        {
            _repositoryMock = new Mock<IProductBuyingRepository>();

            _processor = new ProductBuyingRequestProcessor(_repositoryMock.Object);

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

        [Fact]
        public void ShouldSaveBoughtGame()
        {
            //Arrange
            ProductBought savedProductBought = null;

            _repositoryMock.Setup(x => x.Save(It.IsAny<ProductBought>()))
                .Callback<ProductBought>(product =>
                {
                    savedProductBought = product;
                });

            //Act
            _processor.BuyProduct(_request);
            _repositoryMock.Verify(x => x.Save(It.IsAny<ProductBought>()), Times.Once);

            //Assert
            Assert.NotNull(savedProductBought);
            Assert.Equal(_request.FirstName, savedProductBought.FirstName);
            Assert.Equal(_request.LastName, savedProductBought.LastName);
            Assert.Equal(_request.Email, savedProductBought.Email);
            Assert.Equal(_request.Date, savedProductBought.Date);
        }
    }
}
