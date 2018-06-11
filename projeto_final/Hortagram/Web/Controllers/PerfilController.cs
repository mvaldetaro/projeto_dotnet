using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Web.Business;
using Web.Models;

namespace Web.Controllers
{
    public class PerfilController : Controller
    {

        public async Task<ActionResult> Index()
        {
            string access_token = Session["access_token"]?.ToString();
            string userName = Session["userName"].ToString();

            int userId = await Utils.UserId();

            if (!string.IsNullOrEmpty(access_token))
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Const.ServiceUrl);
                    client.DefaultRequestHeaders.Accept.Clear();

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $"{access_token}");

                    var response = await client.GetAsync("/api/Usuarios/" + userId);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();

                        UsuarioViewModel user = new UsuarioViewModel();

                        user = JsonConvert.DeserializeObject<UsuarioViewModel>(responseContent);

                        ViewBag.FullName = user.NOME + " " + user.SOBRENOME;

                        return View(user);
                    }

                    return View("Error");
                }
            }

            return View();
        }


        // GET: Perfil/Edit
        public async Task<ActionResult> Edit()
        {
            string access_token = Session["access_token"]?.ToString();
            string userName = Session["userName"].ToString();

            int userId = await Utils.UserId();

            if (!string.IsNullOrEmpty(access_token))
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Const.ServiceUrl);
                    client.DefaultRequestHeaders.Accept.Clear();

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $"{access_token}");

                    var response = await client.GetAsync("/api/Usuarios/"+ userId);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();

                        UsuarioViewModel user = new UsuarioViewModel();

                        user = JsonConvert.DeserializeObject<UsuarioViewModel>(responseContent);

                        ViewBag.FullName = user.NOME + " " + user.SOBRENOME;

                        return View(user);
                    }

                    return View("Error");
                }
            }

            return View();
        }

        // POST: Perfil/Edit/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "USUARIO_ID,NOME,SOBRENOME,FOTO_URL,EMAIL,TELEFONE,ANIVERSARIO")] UsuarioViewModel Usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    if (Request.Files.Count > 0)
                    {
                        var file = Request.Files[0];

                        if(file.FileName != null && file.ContentLength > 0) {
                            Usuario.FOTO_URL = Blob.UploadBlob(file);
                        }
                    }

                    await Business.User.Update(Usuario);

                    return RedirectToAction("Index");

                }

                return View();
            }
            catch
            {
                return View();
            }
        }


        public async Task<ActionResult> Friend(int id)
        {
            string access_token = Session["access_token"]?.ToString();
            string userName = Session["userName"].ToString();

            int userId = id;

            if (!string.IsNullOrEmpty(access_token))
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Const.ServiceUrl);
                    client.DefaultRequestHeaders.Accept.Clear();

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $"{access_token}");

                    var response = await client.GetAsync("/api/Usuarios/" + userId);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();

                        UsuarioViewModel user = new UsuarioViewModel();

                        user = JsonConvert.DeserializeObject<UsuarioViewModel>(responseContent);

                        ViewBag.FullName = user.NOME + " " + user.SOBRENOME;

                        return View(user);
                    }

                    return View("Error");
                }
            }

            return View();
        }
    }
}
