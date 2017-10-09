using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GuitarStore.Domain.Entities;

namespace UnitTests
{
    [TestClass]
    public class ShippingDetailsTests
    {
        [TestMethod]
        public void ShippingDetails_Are_Truthy()
        {
            // Arrange
            ShippingDetails shippingDetails = new ShippingDetails();
            shippingDetails.Name = "Gosho";
            shippingDetails.Address1 = "Pernik 176";
            shippingDetails.Address2 = "Telerik Academy";
            shippingDetails.City = "Sofia";
            shippingDetails.Country = "Bulgaria";
            shippingDetails.Zip = "1309";

            // Act

            // Assert
            Assert.AreEqual(shippingDetails.Name, "Gosho");
            Assert.AreEqual(shippingDetails.Address1, "Pernik 176");
            Assert.AreEqual(shippingDetails.Address2, "Telerik Academy");
            Assert.AreEqual(shippingDetails.City, "Sofia");
            Assert.AreEqual(shippingDetails.Country, "Bulgaria");
            Assert.AreEqual(shippingDetails.Zip, "1309");
        }
    }
}
