using System;
using Xunit;
using PackagingWholesale.BLL.Products.Buying;
using PackagingWholesale.BLL.Products.Buying.Processor;
using PackagingWholesale.BLL.Interface;
using Moq;
using PackagingWholesale.BLL.Products;

namespace PackagingWholesale.BLL.Tests
{
    public class ProductBuyingRequestProcessorTests
    {
        private Mock<IProductBuyingRepository> _repositoryMock;
        private Mock<IProductRepository> _repositoryProductMock;

        private bool _isProductAvailable = true;

        private ProductBuyingRequestProcessor _processor;
        private ProductBuyingRequest _request;
        public ProductBuyingRequestProcessorTests()
        {
            _repositoryMock = new Mock<IProductBuyingRepository>();
            _repositoryProductMock = new Mock<IProductRepository>();

            //Arrange
            _request = new ProductBuyingRequest()
            {
                FirstName = "Jarek",
                LastName = "Bigaj",
                Email = "jarek@example.com",
                Date = DateTime.Now,
                ProductToBuy = new Product() { Id = 3 }
            };

            _repositoryProductMock.Setup(x => x.IsProductAvailable(_request.ProductToBuy))
                .Returns(() => { return _isProductAvailable; });

            _processor = new ProductBuyingRequestProcessor(_repositoryMock.Object ,
                _repositoryProductMock.Object);
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
            Assert.Equal(ProductBuyingResultCode.Success, result.StatusProductCode);

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
            Assert.Equal(_request.ProductToBuy.Id, savedProductBought.ProuctId);
        }
        
        [Fact]
        public void ShouldNotSaveBoughtProductIfProductIsNotAvailable()
        {
            _isProductAvailable = false;
            _processor.BuyProduct(_request);
            _isProductAvailable = true;

            _repositoryMock.Verify(x => x.Save(It.IsAny<ProductBought>()), Times.Never);
        }

        [Theory]
        [InlineData(ProductBuyingResultCode.Success, true)]
        [InlineData(ProductBuyingResultCode.GameIsNotAvailable, false)]
        public void ShouldReturnExpectedResultCode
            (ProductBuyingResultCode expectedResultCode, bool isProductAvailable)
        {
            //Arrange
            _isProductAvailable = isProductAvailable;

            //Act
            var result = _processor.BuyProduct(_request);

            //Assert
            Assert.Equal(expectedResultCode, result.StatusProductCode);

        }

        [Theory]
        [InlineData(11,true)]
        [InlineData(null,false)]
        public void ShouldReturnExpectedBoughtProductId(int? expectedPurchaseId, bool isProductAvailable)
        {
            //Arrange
            _isProductAvailable = isProductAvailable;
            if(isProductAvailable)
            {
                _repositoryMock.Setup(x => x.Save(It.IsAny<ProductBought>()))
                    .Returns(11);
            }

            //Act
            var result = _processor.BuyProduct(_request);

            Assert.Equal(expectedPurchaseId, result.PurchaseId);
        }
    }
}
