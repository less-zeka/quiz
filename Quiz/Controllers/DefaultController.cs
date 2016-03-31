using System.Web.Mvc;
using Quiz.Gameplay;
using Quiz.Infrastructure;

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
            var model = GameMaster.CurrentQuestion;
            return PartialView("_Question", model);
        }

        public PartialViewResult UserLegend()
        {
            var model = Hubs.QuizHub.GetUsersByIdentifier(HttpContext.GetCurrentLogonUserIdentifier());
            return PartialView("_UserLegend", model);
        }

        public PartialViewResult PlayerAnswer(int answerId)
        {
            return PartialView("_PlayerAnswer");
        }
    }
}