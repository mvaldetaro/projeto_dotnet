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
                User user = new User();
                user.Email = (string)Session["user_email"];
                ViewBag.response = Session["responseContent"];
                return View(user);
            } else
            {
                return RedirectToAction("Login", "Account");
            }
        }
    }
}