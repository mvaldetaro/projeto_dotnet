using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SocialNetwork.Web.Models;
using SocialNetwork.Web.Models.Account;
using SocialNetwork.Web.ViewModel;
using SocialNetwork.Web.Helpers;

namespace SocialNetwork.Web.Controllers
{
    [Authorize]
    public class TimelineController : Controller
    {

        private TokenHelper _tokenHelper;
        // GET: Timeline
        public ActionResult Index(LoginViewModel model)
        {
            if(Session["AccessToken"] != null)
            {
                User user_logado = new User();
                user_logado.Email = model.Email;
                return View(user_logado);
            } else
            {
                return RedirectToAction("Login", "Account");
            }
        }
    }
}