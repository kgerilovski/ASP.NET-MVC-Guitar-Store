using Microsoft.VisualStudio.TestTools.UnitTesting;
using GuitarStore.WebUi.Entities;
using GuitarStore.WebUi.Controllers;
using Moq;
using GuitarStore.WebUi.Abstract;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace UnitTests
{
    [TestClass]
    public class AdminTests
    {
        [TestMethod]
        public void Index()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product {ProductId = 1, Name = "P1"},
                new Product {ProductId = 2, Name = "P2"},
                new Product {ProductId = 3, Name = "P3"},
            });
            AdminController adminController = new AdminController(mock.Object);

            ViewResult result = adminController.Index() as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Create()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product {ProductId = 1, Name = "P1"},
                new Product {ProductId = 2, Name = "P2"},
                new Product {ProductId = 3, Name = "P3"},
            });
            AdminController adminController = new AdminController(mock.Object);

            ViewResult result = adminController.Create() as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Index_Contains_All_Products()
        {
            // Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product {ProductId = 1, Name = "P1"},
                new Product {ProductId = 2, Name = "P2"},
                new Product {ProductId = 3, Name = "P3"},
            });
            AdminController adminController = new AdminController(mock.Object);

            // Action
            Product[] result = ((IEnumerable<Product>)adminController.Index().
            ViewData.Model).ToArray();

            // Assert
            Assert.AreEqual(result.Length, 3);
            Assert.AreEqual("P1", result[0].Name);
            Assert.AreEqual("P2", result[1].Name);
            Assert.AreEqual("P3", result[2].Name);
        }

        [TestMethod]
        public void Can_Edit_Product()
        {
            // Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product {ProductId = 1, Name = "P1"},
                new Product {ProductId = 2, Name = "P2"},
                new Product {ProductId = 3, Name = "P3"},
            });
            AdminController adminController = new AdminController(mock.Object);

            // Act
            Product p1 = adminController.Edit(1).ViewData.Model as Product;
            Product p2 = adminController.Edit(2).ViewData.Model as Product;
            Product p3 = adminController.Edit(3).ViewData.Model as Product;

            // Assert
            Assert.AreEqual(1, p1.ProductId);
            Assert.AreEqual(2, p2.ProductId);
            Assert.AreEqual(3, p3.ProductId);
        }

        [TestMethod]
        public void Cannot_Edit_Nonexistent_Product()
        {
            // Arrange - create the mock repository
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product {ProductId = 1, Name = "P1"},
                new Product {ProductId = 2, Name = "P2"},
                new Product {ProductId = 3, Name = "P3"},
            });
            AdminController adminController = new AdminController(mock.Object);

            // Act
            Product result = (Product)adminController.Edit(4).ViewData.Model;

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Can_Save_Valid_Changes()
        {

            // Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            AdminController adminController = new AdminController(mock.Object);
            Product product = new Product { Name = "Test" };

            // Act
            ActionResult result = adminController.Edit(product);

            // Assert
            mock.Verify(m => m.SaveProduct(product));
            Assert.IsNotInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void Cannot_Save_Invalid_Changes()
        {
            // Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            AdminController adminController = new AdminController(mock.Object);
            Product product = new Product { Name = "Test" };
            adminController.ModelState.AddModelError("error", "error");

            // Act
            ActionResult result = adminController.Edit(product);

            // Assert
            mock.Verify(m => m.SaveProduct(It.IsAny<Product>()), Times.Never());
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void Can_Delete_Valid_Products()
        {
            // Arrange
            Product product = new Product { ProductId = 2, Name = "Test" };
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product {ProductId = 1, Name = "P1"},
                product,
                new Product {ProductId = 3, Name = "P3"},
            });
            AdminController adminController = new AdminController(mock.Object);
            // Act - delete the product
            adminController.Delete(product.ProductId);

            // Assert
            mock.Verify(m => m.DeleteProduct(product.ProductId));
        }
    }
}
