

using Microsoft.EntityFrameworkCore;
using PackagingWholesale.BLL.Products;
using PackagingWholesale.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace PackagingWholesale.DAL.Tests
{
    public class ProductRepositoryTests
    {
        [Fact]
        public void ShouldGetAllProduct()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ProductContext>()
                .UseInMemoryDatabase(databaseName: "ShouldGetAllProduct")
                .Options;

            var storedList = new List<Product>
            {
                new Product() {Name = "M"},
                new Product() {Name = "O"},
                new Product() {Name = "P"}
            };

            using (var context = new ProductContext(options))
            {
                foreach(var game in storedList)
                {
                    context.Add(game);
                    context.SaveChanges();
                }
            }

            //Act
            List<Product> actualListProducts;
            using (var context = new ProductContext(options))
            {
                var repository = new ProductRepository(context);
                actualListProducts = repository.GetAll().ToList();
            }

            //Assert
            Assert.Equal(storedList.Count, actualListProducts.Count);
        }

        [Fact]
        public void ShouldCallIsProductAvaliableWithFalse()
        {
            //Arrange
            var date = new DateTime(2020, 1, 25);

            var options = new DbContextOptionsBuilder<ProductContext>()
                .UseInMemoryDatabase(databaseName: "ShouldCallIsProductAvaliableWithFalse")
                .Options;

            Product product1 = new Product { Id = 1, Name = "K" };
            Product product2 = new Product { Id = 2, Name = "L" };
            Product product3 = new Product { Id = 3, Name = "M" };

            using (var context = new ProductContext(options))
            {
                context.Products.Add(product1);
                context.Products.Add(product2);
                context.Products.Add(product3);

                context.PackagingWholesaleWarehouseStatus
                    .Add(new PackagingWholesaleWarehouseStatus { Id = 1, ProductId = 1, Amount = 0 });
                context.PackagingWholesaleWarehouseStatus
                    .Add(new PackagingWholesaleWarehouseStatus { Id = 2, ProductId = 2, Amount = 8 });
                context.PackagingWholesaleWarehouseStatus
                    .Add(new PackagingWholesaleWarehouseStatus { Id = 3, ProductId = 3, Amount = 1 });

                context.SaveChanges();
            }


            using (var context = new ProductContext(options))
            {
                var repository = new ProductRepository(context);
                //Act
                var status1 = repository.IsProductAvailable(product1);
                var status2 = repository.IsProductAvailable(product2);
                var status3 = repository.IsProductAvailable(product3);

                //Assert
                Assert.False(status1);
                Assert.True(status2);
                Assert.True(status3);
            }

        }
    }
}
