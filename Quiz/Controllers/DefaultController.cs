using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.SignalR;
using Quiz.Gameplay;
using Quiz.Hubs;

namespace Quiz.Controllers
{
    [System.Web.Mvc.Authorize]
    public class DefaultController : Controller
    {
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
            var model = QuizHub.GetScore();
            return PartialView("_UserLegend", model);
        }

        public async Task<PartialViewResult> PlayerAnswer(int answerId)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<QuizHub>();
            await hubContext.Clients.User(User.Identity.Name).playerAnswer(answerId);
            var model = QuizHub.GetScore();
            return PartialView("_UserLegend", model);
        }
    }
}