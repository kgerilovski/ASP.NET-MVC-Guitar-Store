using GuitarStore.Domain.Abstract;
using GuitarStore.Domain.Entities;
using GuitarStore.WebUi.Controllers;
using GuitarStore.WebUi.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;
using System.Web.Mvc;

namespace UnitTests
{
    [TestClass]
    public class CartTests
    {
        [TestMethod]
        public void Can_Add_New_Lines()
        {
            // Arrange
            Product p1 = new Product { ProductId = 1, Name = "P1" };
            Product p2 = new Product { ProductId = 2, Name = "P2" };
            Cart cart = new Cart();

            // Act
            cart.AddItem(p1, 1);
            cart.AddItem(p2, 1);
            CartLine[] results = cart.Lines.ToArray();

            // Assert
            Assert.AreEqual(results.Length, 2);
            Assert.AreEqual(results[0].Product, p1);
            Assert.AreEqual(results[1].Product, p2);
        }

        [TestMethod]
        public void Can_Add_Quantity_For_Existing_Lines()
        {
            // Arrange
            Product p1 = new Product { ProductId = 1, Name = "P1" };
            Product p2 = new Product { ProductId = 2, Name = "P2" };
            Cart cart = new Cart();

            // Act
            cart.AddItem(p1, 1);
            cart.AddItem(p2, 2);
            cart.AddItem(p1, 5);
            CartLine[] results = cart.Lines.OrderBy(c => c.Product.ProductId).ToArray();

            // Assert
            Assert.AreEqual(results.Length, 2);
            Assert.AreEqual(results[0].Quantity, 6);
            Assert.AreEqual(results[1].Quantity, 2);
        }

        [TestMethod]
        public void Can_Remove_Line()
        {
            // Arrange
            Product p1 = new Product { ProductId = 1, Name = "P1" };
            Product p2 = new Product { ProductId = 2, Name = "P2" };
            Product p3 = new Product { ProductId = 3, Name = "P3" };
            Cart cart = new Cart();
            cart.AddItem(p1, 1);
            cart.AddItem(p2, 3);
            cart.AddItem(p3, 5);
            cart.AddItem(p2, 1);

            // Act
            cart.RemoveLine(p2);

            // Assert
            Assert.AreEqual(cart.Lines.Where(c => c.Product == p2).Count(), 0);
            Assert.AreEqual(cart.Lines.Count(), 2);
        }

        [TestMethod]
        public void Calculate_Cart_Total()
        {
            // Arrange
            Product p1 = new Product { ProductId = 1, Name = "P1", Price = 1999M };
            Product p2 = new Product { ProductId = 2, Name = "P2", Price = 2499M };
            Cart cart = new Cart();

            // Act
            cart.AddItem(p1, 1);
            cart.AddItem(p2, 1);
            cart.AddItem(p1, 3);
            decimal result = cart.ComputeTotalValue();

            // Assert
            Assert.AreEqual(result, 10495M);
        }

        [TestMethod]
        public void Can_Clear_Contents()
        {
            // Arrange
            Product p1 = new Product { ProductId = 1, Name = "P1", Price = 1999M };
            Product p2 = new Product { ProductId = 2, Name = "P2", Price = 2499M };
            Cart target = new Cart();
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);

            // Act
            target.Clear();

            // Assert
            Assert.AreEqual(target.Lines.Count(), 0);
        }

        [TestMethod]
        public void Can_Add_To_Cart()
        {
            // Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product {ProductId = 1, Name = "P1", Category = "Bass"},
            }.AsQueryable());
            Cart cart = new Cart();
            CartController cartController = new CartController(mock.Object, null);

            // Act
            cartController.AddToCart(cart, 1, null);

            // Assert
            Assert.AreEqual(cart.Lines.Count(), 1);
            Assert.AreEqual(cart.Lines.ToArray()[0].Product.ProductId, 1);
        }

        [TestMethod]
        public void Can_Remove_From_Cart()
        {
            // Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product {ProductId = 1, Name = "P1", Category = "Bass"},
            }.AsQueryable());
            Cart cart = new Cart();
            CartController cartController = new CartController(mock.Object, null);

            // Act
            cartController.RemoveFromCart(cart, 1, null);

            // Assert
            Assert.AreEqual(cart.Lines.Count(), 0);
        }

        [TestMethod]
        public void Adding_Product_To_Cart_Goes_To_Cart_Screen()
        {
            // Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product {ProductId = 1, Name = "P1", Category = "Bass"},
            }.AsQueryable());
            Cart cart = new Cart();
            CartController cartController = new CartController(mock.Object, null);

            // Act
            RedirectToRouteResult result = cartController.AddToCart(cart, 2, "myUrl");

            // Assert
            Assert.AreEqual(result.RouteValues["action"], "Index");
            Assert.AreEqual(result.RouteValues["returnUrl"], "myUrl");
        }

        [TestMethod]
        public void Can_View_Cart_Contents()
        {
            // Arrange
            Cart cart = new Cart();
            CartController cartController = new CartController(null, null);

            // Act
            CartIndexViewModel result = (CartIndexViewModel)cartController.Index(cart, "myUrl").ViewData.Model;
            
            // Assert
            Assert.AreSame(result.Cart, cart);
            Assert.AreEqual(result.ReturnUrl, "myUrl");
        }

        [TestMethod]
        public void Cannot_Checkout_Empty_Cart()
        {
            // Arrange
            Mock<IOrderProcessor> mock = new Mock<IOrderProcessor>();
            Cart cart = new Cart();
            ShippingDetails shippingDetails = new ShippingDetails();
            CartController cartController = new CartController(null, mock.Object);

            // Act
            ViewResult result = cartController.Checkout(cart, shippingDetails);

            // Assert
            mock.Verify(m => m.ProcessOrder(It.IsAny<Cart>(), It.IsAny<ShippingDetails>()),
            Times.Never());
            Assert.AreEqual("", result.ViewName);
            Assert.AreEqual(false, result.ViewData.ModelState.IsValid);
        }

        [TestMethod]
        public void Cannot_Checkout_Invalid_ShippingDetails()
        {
            // Arrange
            Mock<IOrderProcessor> mock = new Mock<IOrderProcessor>();
            Cart cart = new Cart();
            cart.AddItem(new Product(), 1);
            CartController cartController = new CartController(null, mock.Object);
            cartController.ModelState.AddModelError("error", "error");

            // Act - try to checkout
            ViewResult result = cartController.Checkout(cart, new ShippingDetails());

            // Assert
            mock.Verify(m => m.ProcessOrder(It.IsAny<Cart>(), It.IsAny<ShippingDetails>()),
            Times.Never());
            Assert.AreEqual("", result.ViewName);
            Assert.AreEqual(false, result.ViewData.ModelState.IsValid);
        }

        [TestMethod]
        public void Can_Checkout_And_Submit_Order()
        {
            // Arrange
            Mock<IOrderProcessor> mock = new Mock<IOrderProcessor>();
            Cart cart = new Cart();
            cart.AddItem(new Product(), 1);
            CartController cartController = new CartController(null, mock.Object);

            // Act
            ViewResult result = cartController.Checkout(cart, new ShippingDetails());

            // Assert
            mock.Verify(m => m.ProcessOrder(It.IsAny<Cart>(), It.IsAny<ShippingDetails>()),
            Times.Once());
            Assert.AreEqual("Completed", result.ViewName);
            Assert.AreEqual(true, result.ViewData.ModelState.IsValid);
        }

        [TestMethod]
        public void Summary()
        {
            CartController controller = new CartController(null, null);
            Cart cart = new Cart();

            PartialViewResult result = controller.Summary(cart) as PartialViewResult;

            Assert.IsNotNull(result);
        }
    }
}
