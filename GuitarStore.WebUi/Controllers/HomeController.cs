using System.Web.Mvc;

namespace GuitarStore.WebUi.Controllers
{
    public class HomeController : Controller
    {
        [OutputCache(Duration = 300, VaryByParam = "none")]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Welcome to our Guitar Store!";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Our contacts:";

            return View();
        }
    }
}