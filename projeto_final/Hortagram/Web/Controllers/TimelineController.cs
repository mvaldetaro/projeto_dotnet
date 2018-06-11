using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Web.Business;
using Web.Models;

namespace Web.Controllers
{
    public class TimelineController : Controller
    {
        // GET: Timeline
        public async Task<ActionResult> Index()
        {

            string access_token = Session["access_token"]?.ToString();
            string userName = Session["userName"].ToString();

            int userId = await Utils.UserId();

            TimelineViewModel timeline = new TimelineViewModel();

            if (!string.IsNullOrEmpty(access_token))
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Const.ServiceUrl);
                    client.DefaultRequestHeaders.Accept.Clear();

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $"{access_token}");

                    var response = await client.GetAsync("/api/Usuarios/" + userId);

                    //var response = await client.GetAsync("/api/Usuarios/1");

                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();

                        UsuarioViewModel user = new UsuarioViewModel();

                        user = JsonConvert.DeserializeObject<UsuarioViewModel>(responseContent);

                        timeline.Usuario = user;
                        timeline.FotosAmigos = await Business.User.FotosAmigos(userId);
                        
                        timeline.FotosAmigos = timeline.FotosAmigos.Reverse().ToList();
                        timeline.Seguidores = user.USUARIO2.ToList();
                        timeline.Seguindo = user.USUARIO1.ToList();

                        ViewBag.FullName = user.NOME + " " + user.SOBRENOME;
                        ViewBag.Uid = userId;
                        ViewBag.Url = Const.ServiceUrl;

                        // Trabalhar Aqui

                        return View(timeline);
                    }

                    return View("Error");
                }
            }

            return RedirectToAction("Login", "Account", null);
        }



        public async Task<ActionResult> Friend(int id)
        {

            string access_token = Session["access_token"]?.ToString();
            string userName = Session["userName"].ToString();

            int userId = id;

            TimelineViewModel timeline = new TimelineViewModel();

            if (!string.IsNullOrEmpty(access_token))
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Const.ServiceUrl);
                    client.DefaultRequestHeaders.Accept.Clear();

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $"{access_token}");

                    var response = await client.GetAsync("/api/Usuarios/" + userId);

                    //var response = await client.GetAsync("/api/Usuarios/1");

                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();

                        UsuarioViewModel user = new UsuarioViewModel();

                        user = JsonConvert.DeserializeObject<UsuarioViewModel>(responseContent);

                        timeline.Usuario = user;
                        timeline.FotosAmigos = await Business.User.FotosAmigos(userId);

                        timeline.FotosAmigos = timeline.FotosAmigos.Reverse().ToList();
                        timeline.Seguidores = user.USUARIO2.ToList();
                        timeline.Seguindo = user.USUARIO1.ToList();

                        ViewBag.FullName = user.NOME + " " + user.SOBRENOME;
                        ViewBag.Uid = userId;
                        ViewBag.Url = Const.ServiceUrl;

                        // Trabalhar Aqui

                        return View(timeline);
                    }

                    return View("Error");
                }
            }

            return RedirectToAction("Login", "Account", null);
        }
    }
}