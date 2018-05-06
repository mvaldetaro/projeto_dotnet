using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using Newtonsoft.Json;
using SocialNetwork.Web.ViewModel;
using SocialNetwork.Web.BussinessModel;

namespace SocialNetwork.Web.Bussiness
{
    public class UsersActions
    {
        public static HttpClient httpClient = new HttpClient();
        public static string API_URL = "http://localhost:53905/api";

        // GET ALL

        public static List<Pessoa> GetAllAspNetUsersFromApi()
        {

            var resultList = new List<AspNetUser>();

            try {
                var client = new HttpClient();

                var getDataTask = client.GetAsync(API_URL + "/AspNetUsers")
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

        public static List<Pessoa> GetAllUsersFromApi()
        {

            var resultList = new List<Pessoa>();

            try
            {
                var client = new HttpClient();

                var getDataTask = client.GetAsync(API_URL + "/Users")
                    .ContinueWith(response => {
                        var result = response.Result;

                        if (result.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            var readResult = result.Content.ReadAsAsync<List<Pessoa>>();
                            readResult.Wait();

                            resultList = readResult.Result;
                        }
                    });

                getDataTask.Wait();

                var pessoasList = new List<Pessoa>();

                foreach (Pessoa p in resultList)
                {
                    pessoasList.Add(p);
                }

                return pessoasList;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        // GET ASP_USER_API

        public static Pessoa GetUserFromApi(string email)
        {
            var pessoa = new Pessoa();

            foreach (AspNetUser u in GetAllAspNetUsersFromApi())
            {
                if (u.Email == email)
                {
                    pessoa = AspUserToPessoa(u);
                }  
            }

            return pessoa;
        }

        // GET USER_API

        public static Pessoa GetUser(string uid)
        {
            try
            {
                var client = new HttpClient();
                var user = new Pessoa();

                var getDataTask = client.GetAsync(API_URL + "/Users/" + uid)
                    .ContinueWith(response => {
                        var result = response.Result;

                        if (result.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            var readResult = result.Content.ReadAsAsync<Pessoa>();
                            readResult.Wait();

                            user = readResult.Result;
                        }
                    });

                getDataTask.Wait();

                return user;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static AspNetUser GetAspUser(string id)
        {
            try
            {
                var client = new HttpClient();
                var user = new AspNetUser();

                var getDataTask = client.GetAsync(API_URL + "/AspNetUsers/" + id)
                    .ContinueWith(response => {
                        var result = response.Result;

                        if (result.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            var readResult = result.Content.ReadAsAsync<AspNetUser>();
                            readResult.Wait();

                            user = readResult.Result;
                        }
                    });

                getDataTask.Wait();

                return user;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Mount PESSOA MODEL VIEW

        public static Pessoa GetUserFromApiByUid(string uid)
        {
            var pessoa = new Pessoa();

            if (VerifyExistUserAccount(uid))
            {

                var log_user = GetAspUser(uid);
                pessoa = GetUser(uid);

                pessoa.Email = log_user.Email;
                pessoa.UserName = log_user.UserName;
            }
            else
            {
                
                pessoa = AspUserToPessoa(GetAspUser(uid));
    
            }

            return pessoa;
        }

        public static Boolean VerifyExistUserAccount(string uid)
        {
            if(GetAllUsersFromApi().Count > 0)
            {
                foreach (Pessoa u in GetAllUsersFromApi())
                {
                    var UID = u.uid;

                    var strUID = uid;

                    if (u.uid == uid)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static Boolean CreateAccountPessoa(Pessoa pessoa)
        {
            
            var result = httpClient.PostAsJsonAsync(API_URL + "/Users", PessoaToUserAccountModel(pessoa)).Result;

            if (result.StatusCode == System.Net.HttpStatusCode.Created)
            {    
                return true;
            } else
            {
                return false;
            }
        }

        public static Boolean UpdateAccountPessoa(Pessoa pessoa)
        {

            var result = httpClient.PutAsJsonAsync(API_URL + "/Users/" + pessoa.uid, PessoaToUserAccountModel(pessoa)).Result;

            if (result.StatusCode == System.Net.HttpStatusCode.Created)
            {
                return true;
            }
            else
            {
                return false;
            }

            return false;
        }


        public static Pessoa AspUserToPessoa(AspNetUser user)
        {
            var p = new Pessoa();

            p.Id = user.Id;
            p.uid = user.Id;
            p.Email = user.Email;
            p.UserName = user.UserName;

            return p;
        }


        public static UserAccountModel PessoaToUserAccountModel(Pessoa pessoa)
        {
            var uam = new UserAccountModel();

            uam.bio = pessoa.bio;
            uam.brithday = pessoa.brithday;
            uam.nickname = pessoa.nickname;
            uam.nome = pessoa.nome;
            uam.photo_url = pessoa.photo_url;
            uam.sexo = pessoa.sexo;
            uam.site = pessoa.site;
            uam.uid = pessoa.uid;
            
            return uam;
        }
    }
}

