using System.Web.Mvc;

namespace Quiz.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default
        public ActionResult Index()
        {
            return View("Index");
        }
    }
}