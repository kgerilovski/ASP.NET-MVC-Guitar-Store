using System.Web.Mvc;

namespace GuitarStore.WebUi.Areas.Chat.Controllers
{
    public class ChatController : Controller
    {
        [Authorize]
        public ActionResult Chat()
        {
            return View();
        }
    }
}