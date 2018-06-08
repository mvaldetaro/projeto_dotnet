using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Service.Controllers
{
    public class FotosAmigosController : ApiController
    {
        private Entities db = new Entities();

        // GET: api/FotosAmigos/5
        public IQueryable<FOTO> GetFotosAmigos(int id)
        {
            USUARIO uSUARIO = db.USUARIO.Find(id);

            List<FOTO> FOTOS_AMIGOS = new List<FOTO>();

            List<USUARIO> AMIGOS = uSUARIO.USUARIO1.ToList();

            foreach (var user in AMIGOS)
            {
                for (var i = 0; i < user.FOTO.Count; i++)
                {
                    FOTO Foto = user.FOTO.ElementAt(i) ;
                    FOTOS_AMIGOS.Add(Foto);
                }

            }

            return FOTOS_AMIGOS.AsQueryable() ;
        }
    }
}
