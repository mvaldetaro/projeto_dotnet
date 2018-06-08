using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Service.Models;

namespace Service.Controllers
{
    //[Authorize]
    public class UsuariosController : ApiController
    {
        private Entities db = new Entities();

        // GET: api/Usuarios
        public IQueryable<USUARIO> GetUSUARIO()
        {
            return db.USUARIO;
        }

        // GET: api/Usuarios/5
        [ResponseType(typeof(USUARIO))]
        public async Task<IHttpActionResult> GetUSUARIO(int id)
        {
            USUARIO uSUARIO = await db.USUARIO.FindAsync(id);
            if (uSUARIO == null)
            {
                return NotFound();
            }

            return Ok(uSUARIO);
        }

        // PUT: api/Usuarios/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutUSUARIO(int id, USUARIO uSUARIO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != uSUARIO.USUARIO_ID)
            {
                return BadRequest();
            }

            db.Entry(uSUARIO).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!USUARIOExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Usuarios
        [ResponseType(typeof(USUARIO))]
        public async Task<IHttpActionResult> PostUSUARIO(USUARIO uSUARIO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.USUARIO.Add(uSUARIO);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = uSUARIO.USUARIO_ID }, uSUARIO);
        }

        // DELETE: api/Usuarios/5
        [ResponseType(typeof(USUARIO))]
        public async Task<IHttpActionResult> DeleteUSUARIO(int id)
        {
            USUARIO uSUARIO = await db.USUARIO.FindAsync(id);
            if (uSUARIO == null)
            {
                return NotFound();
            }

            db.USUARIO.Remove(uSUARIO);
            await db.SaveChangesAsync();

            return Ok(uSUARIO);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool USUARIOExists(int id)
        {
            return db.USUARIO.Count(e => e.USUARIO_ID == id) > 0;
        }
    }
}