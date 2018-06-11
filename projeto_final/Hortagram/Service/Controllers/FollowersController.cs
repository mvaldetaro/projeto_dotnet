﻿using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace Service.Controllers
{
    public class FollowersController : ApiController
    {
        private Entities db = new Entities();

        // GET: api/Followers/5
        public IQueryable<USUARIO> GetFOLLOWERS(int id)
        {
            USUARIO uSUARIO = db.USUARIO.Find(id);

            return uSUARIO.USUARIO2.AsQueryable();
        }

    }
}