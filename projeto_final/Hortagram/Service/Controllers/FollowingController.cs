using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Service.Controllers
{
    public class FollowingController : ApiController
    {
        private Entities db = new Entities();

        // GET: api/Followers/5
        public IQueryable<USUARIO> GetFOLLOWINGS(int id)
        {
            USUARIO uSUARIO = db.USUARIO.Find(id);

            return uSUARIO.USUARIO1.AsQueryable();
        }

    }
}
