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
    //[Authorize]
    public class TimelineController : Controller
    {
        // GET: Timeline
        public ActionResult Index()
        {
            if(Session["AccessToken"] != null)
            {   
                Pessoa user = new Pessoa();
                user.uid = (string)Session["user_uid"];
                user.Email = (string)Session["user_email"];
                return View(user);
            } else
            {
                return RedirectToAction("Login", "Account");
            }
        }
    }
}