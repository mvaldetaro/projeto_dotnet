using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Service.Controllers
{
    [EnableCors(origins: "http://localhost:63083", headers: "*", methods: "*")]
    public class FollowController : ApiController
    {
        private Entities db = new Entities();
        // POST: api/Follow/?uidm={uidm}&uids={uids}
        public void Post([FromUri]int uidm, [FromUri]int uids)
        {
            db.Seguir(uidm, uids);
        }
    }
}
