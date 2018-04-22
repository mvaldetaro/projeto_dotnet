using SocialNetwork.Web.Models.Account;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SocialNetwork.Web.Controllers
{
    public class AccountController : Controller
    {
        private HttpClient _client;


        public AccountController()
        {
            _client = new HttpClient();

            _client.BaseAddress = new Uri("https://localhost:44333/");
            _client.DefaultRequestHeaders.Accept.Clear();

            var mediaType = new MediaTypeWithQualityHeaderValue("application/json");
            _client.DefaultRequestHeaders.Accept.Add(mediaType);
            
        }


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
            if(ModelState.IsValid)
            {
                var response = await _client.PostAsJsonAsync("api/Account/Register", model);

                if(response.IsSuccessStatusCode)
                {

                } else
                {

                }
            }

            return View(model);
        }

        protected override void Dispose(bool disposing)
        {

            if(disposing && _client != null)
            {
                _client.Dispose();
                _client = null;
            }

            base.Dispose(disposing);
        }

    }
}