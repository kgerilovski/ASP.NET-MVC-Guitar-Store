using Microsoft.VisualStudio.TestTools.UnitTesting;
using GuitarStore.WebUi.Areas.Chat.Controllers;
using System.Web.Mvc;

namespace UnitTests
{
    [TestClass]
    public class ChatTests
    {
        [TestMethod]
        public void Chat()
        {
            // Arrange
            ChatController controller = new ChatController();

            // Act
            ViewResult result = controller.Chat() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
