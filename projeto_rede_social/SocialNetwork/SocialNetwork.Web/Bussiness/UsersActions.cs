using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using SocialNetwork.Web.ViewModel;

namespace SocialNetwork.Web.Bussiness
{
    public class UsersActions
    {

        public static string API_URL = "http://localhost:53905/api/AspNetUsers";

        private static Pessoa GetAspNetUserFromApi(string Email)
        {

            Pessoa p = new Pessoa();

            return p;
        }


        public static List<Pessoa> GetAllAspNetUsersFromApi()
        {

            var resultList = new List<AspNetUser>();

            try {
                var client = new HttpClient();

                //client.DefaultRequestHeaders.Clear();

                //var mediaType = new MediaTypeWithQualityHeaderValue("application/json");
                //client.DefaultRequestHeaders.Accept.Add(mediaType);

                var getDataTask = client.GetAsync("http://localhost:53905/api/AspNetUsers")
                    .ContinueWith(response => {
                        var result = response.Result;

                        var content = result.Content;

                        if (result.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            var readResult = result.Content.ReadAsAsync<List<AspNetUser>>();
                            readResult.Wait();

                            resultList = readResult.Result;
                        }
                    });

                getDataTask.Wait();

                var pessoasList = new List<Pessoa>();

                foreach (AspNetUser u in resultList)
                {
                    pessoasList.Add(AspUserToPessoa(u));
                }

                return pessoasList;

            } catch( Exception ex) {
                throw ex;
            }
            
        }


        public static Pessoa AspUserToPessoa(AspNetUser user)
        {
            var p = new Pessoa();

            p.Id = user.Id;
            p.Email = user.Email;
            p.UserName = user.UserName;

            return p;
        }
    }
}