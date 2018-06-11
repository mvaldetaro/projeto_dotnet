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
    public class Photo
    {

        public static async Task<Boolean> Publish(FotoViewModel newFoto)
        {
            string access_token = HttpContext.Current.Session["access_token"]?.ToString();
            string userName = HttpContext.Current.Session["userName"].ToString();

            Boolean result = false;

            if (!string.IsNullOrEmpty(access_token))
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Const.ServiceUrl);
                    client.DefaultRequestHeaders.Accept.Clear();

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $"{access_token}");

                    var response = await client.PostAsJsonAsync("/api/Fotos/", newFoto);

                    if (response.IsSuccessStatusCode)
                    {
                        result = true;

                        return result;
                    }

                    return result;
                }
            }

            return result;
        }

    }
}