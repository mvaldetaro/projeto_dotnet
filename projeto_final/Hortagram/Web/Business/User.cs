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
    public class User
    {

        

        public static async Task<Boolean> Update(UsuarioViewModel usuario)
        {
            string access_token = HttpContext.Current.Session["access_token"]?.ToString();
            string userName = HttpContext.Current.Session["userName"].ToString();

            int uid = usuario.USUARIO_ID;

            Boolean result = false;

            if (!string.IsNullOrEmpty(access_token))
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Const.ServiceUrl);
                    client.DefaultRequestHeaders.Accept.Clear();

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $"{access_token}");

                    var response = await client.PutAsJsonAsync("/api/Usuarios/" + uid, usuario);

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



        public static async Task<List<FotoViewModel>> FotosAmigos(int uid)
        {
            string access_token = HttpContext.Current.Session["access_token"]?.ToString();
            List<FotoViewModel> fotosUsuariosSeguidos = new List<FotoViewModel>();

            if (!string.IsNullOrEmpty(access_token))
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Const.ServiceUrl);
                    client.DefaultRequestHeaders.Accept.Clear();

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $"{access_token}");


                    var response = await client.GetAsync("/api/FotosAmigos/" + uid);

                    if (response.IsSuccessStatusCode)
                    {

                        var responseContent = await response.Content.ReadAsStringAsync();
                        fotosUsuariosSeguidos = JsonConvert.DeserializeObject<List<FotoViewModel>>(responseContent);

                        return fotosUsuariosSeguidos;
                    }

                    return fotosUsuariosSeguidos;
                }
            }

            return fotosUsuariosSeguidos;
        }



        public static async Task<List<UsuarioViewModel>> GetAll()
        {

            string access_token = HttpContext.Current.Session["access_token"]?.ToString();
            List<UsuarioViewModel> usuarios = new List<UsuarioViewModel>();

            if (!string.IsNullOrEmpty(access_token))
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Const.ServiceUrl);
                    client.DefaultRequestHeaders.Accept.Clear();

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $"{access_token}");

                    var response = await client.GetAsync("/api/Usuarios");

                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();

                        usuarios = JsonConvert.DeserializeObject<List<UsuarioViewModel>>(responseContent);

                        return usuarios;
                    }

                    return usuarios;
                }
            }

            return usuarios;
        }

    }
}