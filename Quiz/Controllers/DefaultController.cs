using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.SignalR;
using Quiz.Gameplay;
using Quiz.Hubs;
using Quiz.Infrastructure;

namespace Quiz.Controllers
{
    [System.Web.Mvc.Authorize]
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
            var model = QuizHub.GetUsersByIdentifier(HttpContext.GetCurrentLogonUserIdentifier());
            return PartialView("_UserLegend", model);
        }

        public async Task<PartialViewResult> PlayerAnswer(int answerId)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<QuizHub>();
            await hubContext.Clients.User(User.Identity.Name).playerAnswer(answerId);
            //return PartialView("_PlayerAnswer");
            var model = QuizHub.GetScore();
            return PartialView("_Score", model);
        }

        public PartialViewResult Score()
        {
            var model = QuizHub.GetScore();
            return PartialView("_Score", model);
        }
    }
}