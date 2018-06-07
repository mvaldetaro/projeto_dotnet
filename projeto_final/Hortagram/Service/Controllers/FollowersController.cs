using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace Service.Controllers
{
    public class FollowersController : ApiController
    {
        private Entities db = new Entities();

        // GET: api/Followers
        [ResponseType(typeof(Seguidores_Result))]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Followers/5
        [ResponseType(typeof(Seguidores_Result))]
        public IQueryable<Seguidores_Result> Get(int id)
        {

            return db.Seguidores(id).AsQueryable();
        }

    }
}
