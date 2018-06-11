using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Web.Models;
using Web.Business;
using System.Net.Http.Headers;

namespace Web.Controllers
{
    public class AccountController : Controller
    {

        // GET: Account/Register
        public ActionResult Register()
        {
            return View();
        }

        // POST: Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Const.AuthUrl);

                    var response = await client.PostAsJsonAsync("api/Account/Register", model);

                    if (response.IsSuccessStatusCode)
                    {

                        //REGISTRAR NO SERVICE

                        var c = new HttpClient();
                        c.BaseAddress = new Uri(Const.ServiceUrl);

                        UsuarioViewModel newUser = new UsuarioViewModel();

                        string[] nome = model.Email.Split(new string[] { "@" }, StringSplitOptions.None);

                        newUser.NOME = nome[0];
                        newUser.EMAIL = model.Email;

                        var res = await c.PostAsJsonAsync("api/Usuarios", newUser);

                        if (res.IsSuccessStatusCode)
                        {
                            return RedirectToAction("Login");

                        }
                        else
                        {
                            return View("Error");
                        }

                    }
                    else
                    {
                        return View("Error");
                    }
                }
            }

            return View();
        }

        // GET: Account/Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var data = new Dictionary<string, string>
                {
                    { "grant_type", "password" },
                    { "username", model.Username },
                    { "password", model.Password }
                };

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Const.AuthUrl);

                    using (var requestContent = new FormUrlEncodedContent(data))
                    {
                        var response = await client.PostAsync("/Token", requestContent);

                        if (response.IsSuccessStatusCode)
                        {
                            var responseContent = await response.Content.ReadAsStringAsync();

                            var resData = JObject.Parse(responseContent);

                            Session.Add("access_token", resData["access_token"]);
                            Session.Add("userName", resData["userName"]);

                            return RedirectToAction("Index", "Timeline");
                        }

                        return View("ERROR");
                    }
                }
            }
            return View();
        }



        // POST: Account/Logout
        public async Task<ActionResult> Logout()
        {
            string access_token = Session["access_token"]?.ToString();

            if (!string.IsNullOrEmpty(access_token))
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Const.AuthUrl);
                    client.DefaultRequestHeaders.Accept.Clear();

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $"{access_token}");


                    HttpContent content = null;


                    var response = await client.PostAsync("api/Account/Logout", content);

                    if (response.IsSuccessStatusCode)
                    {
                        Session.Clear();
                        return RedirectToAction("Login", "Account");
                    }

                    return View("ERROR");

                }
            }

            return View("ERROR");

        }

    }
}