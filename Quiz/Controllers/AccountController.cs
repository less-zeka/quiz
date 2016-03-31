using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Quiz.Models;
using Quiz.Infrastructure;

namespace Quiz.Controllers
{
    public class AccountController : Controller
    {

        public ViewResult Login()
        {
            var identifier = "SuperQuiz";//Request.QueryString["Identifier"];
            if (string.IsNullOrEmpty(identifier))
            {
                identifier = Guid.NewGuid().ToString();
            }

            var loginModel = new LoginModel
            {
                Identifier = identifier
            };
            return View(loginModel);
        }

        [HttpPost]
        [ActionName("Login")]
        public ActionResult PostLogin(LoginModel loginModel)
        {
            if (!ModelState.IsValid)
            {
                return View(loginModel);
            }
            Response.SetAuthCookie(loginModel.Name, true, $"{loginModel.Identifier}");
            return RedirectToAction("Index", "Default");
        }

        [ActionName("LogOff")]
        public ActionResult PostSignOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();

            // clear authentication cookie
            var cookie1 = new HttpCookie(FormsAuthentication.FormsCookieName, "") {Expires = DateTime.Now.AddYears(-1)};
            Response.Cookies.Add(cookie1);

            // clear session cookie (not necessary for your current problem but i would recommend you do it anyway)
            var cookie2 = new HttpCookie("ASP.NET_SessionId", "") {Expires = DateTime.Now.AddYears(-1)};
            Response.Cookies.Add(cookie2);

            FormsAuthentication.RedirectToLoginPage();
            return RedirectToAction("Index", "Default");
        }
    }
}