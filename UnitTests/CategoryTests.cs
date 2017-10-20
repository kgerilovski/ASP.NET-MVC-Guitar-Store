using Microsoft.VisualStudio.TestTools.UnitTesting;
using GuitarStore.WebUi.Abstract;
using Moq;
using GuitarStore.WebUi.Entities;
using GuitarStore.WebUi.Controllers;
using System.Collections.Generic;
using System.Linq;
using GuitarStore.WebUi.Models;

namespace UnitTests
{
    [TestClass]
    public class CategoryTests
    {
        [TestMethod]
        public void Can_Create_Categories()
        {
            // Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product {ProductId = 1, Name = "P1", Category = "Bass"},
                new Product {ProductId = 2, Name = "P2", Category = "Electric"},
                new Product {ProductId = 3, Name = "P3", Category = "Acoustic"},
                new Product {ProductId = 4, Name = "P4", Category = "Electric"},
            });
            NavController target = new NavController(mock.Object);

            // Act
            string[] results = ((IEnumerable<string>)target.Menu().Model).ToArray();

            // Assert
            Assert.AreEqual(results.Length, 3);
            Assert.AreEqual(results[0], "Acoustic");
            Assert.AreEqual(results[1], "Bass");
            Assert.AreEqual(results[2], "Electric");
        }

        [TestMethod]
        public void Show_Selected_Category()
        {
            // Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product {ProductId = 1, Name = "P1", Category = "Electric"},
                new Product {ProductId = 4, Name = "P2", Category = "Bass"},
            });
            NavController target = new NavController(mock.Object);
            string categoryToSelect = "Electric";

            // Act
            string result = target.Menu(categoryToSelect).ViewBag.SelectedCategory;

            // Assert
            Assert.AreEqual(categoryToSelect, result);
        }

        [TestMethod]
        public void Generate_Category_Specific_Product_Count()
        {
            // Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product {ProductId = 1, Name = "P1", Category = "Cat1"},
                new Product {ProductId = 2, Name = "P2", Category = "Cat2"},
                new Product {ProductId = 3, Name = "P3", Category = "Cat1"},
                new Product {ProductId = 4, Name = "P4", Category = "Cat2"},
                new Product {ProductId = 5, Name = "P5", Category = "Cat3"}
            });
            ProductController target = new ProductController(mock.Object);
            target.PageSize = 3;

            // Act
            int res1 = ((ProductsListViewModel)target
            .List("Cat1").Model).PagingInfo.TotalItems;
            int res2 = ((ProductsListViewModel)target
            .List("Cat2").Model).PagingInfo.TotalItems;
            int res3 = ((ProductsListViewModel)target
            .List("Cat3").Model).PagingInfo.TotalItems;
            int resAll = ((ProductsListViewModel)target
            .List(null).Model).PagingInfo.TotalItems;

            // Assert
            Assert.AreEqual(res1, 2);
            Assert.AreEqual(res2, 2);
            Assert.AreEqual(res3, 1);
            Assert.AreEqual(resAll, 5);
        }
    }
}
