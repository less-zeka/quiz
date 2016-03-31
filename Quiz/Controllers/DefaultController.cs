using System.Web.Mvc;
using Quiz.Gameplay;
using Quiz.Infrastructure;
using Quiz.Models;

namespace Quiz.Controllers
{
    [Authorize]
    public class DefaultController : Controller
    {
        // GET: Default
        public ActionResult Index()
        {
            return View("Index");
        }

        public PartialViewResult NextQuestion()
        {
            var model = GameMaster.GetNextQuestion();
            return PartialView("_Question", model);
        }

        public PartialViewResult UserLegend()
        {
            var model = Hubs.QuizHub.GetUsersByIdentifier(HttpContext.GetCurrentLogonUserIdentifier());
            return PartialView("_UserLegend", model);
        }
    }
}