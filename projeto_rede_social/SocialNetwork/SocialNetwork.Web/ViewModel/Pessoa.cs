using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialNetwork.Web.ViewModel
{
    public class Pessoa : AspNetUser
    {
        public string uid { get; set; }
        public string nome { get; set; }
        public string nickname { get; set; }
        public DateTime brithday { get; set; }
        public string bio { get; set; }
        public string sexo { get; set; }
        public string site { get; set; }
        public string photo_url { get; set; }
    }
}