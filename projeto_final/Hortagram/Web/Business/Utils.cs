using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using Web.Models;

namespace Web.Business
{
    public class Utils
    {

        public static async Task<int> UserId()
        {
            string access_token = HttpContext.Current.Session["access_token"]?.ToString();
            string userName = HttpContext.Current.Session["userName"].ToString();

            int id = 0;

            if (!string.IsNullOrEmpty(access_token))
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Const.ServiceUrl);
                    client.DefaultRequestHeaders.Accept.Clear();

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $"{access_token}");

                    var response = await client.GetAsync("/api/Usuarios/");

                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();

                        List<UsuarioViewModel> users = new List<UsuarioViewModel>();

                        users = JsonConvert.DeserializeObject<List<UsuarioViewModel>>(responseContent);

                        foreach (var user in users)
                        {
                            if (user.EMAIL == userName)
                            {
                                id = user.USUARIO_ID;
                            }
                        }

                        return id;
                    }

                    return id;
                }
            }

            return id;
        }
    }
}