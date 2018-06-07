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
    [Authorize]
    public class FotosController : ApiController
    {
        private Entities db = new Entities();

        // GET: api/Fotos
        public IQueryable<FOTO> GetFOTO()
        {
            return db.FOTO;
        }

        // GET: api/Fotos/5
        [ResponseType(typeof(FOTO))]
        public async Task<IHttpActionResult> GetFOTO(int id)
        {
            FOTO fOTO = await db.FOTO.FindAsync(id);
            if (fOTO == null)
            {
                return NotFound();
            }

            return Ok(fOTO);
        }

        // PUT: api/Fotos/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutFOTO(int id, FOTO fOTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != fOTO.FOTO_ID)
            {
                return BadRequest();
            }

            db.Entry(fOTO).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FOTOExists(id))
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

        // POST: api/Fotos
        [ResponseType(typeof(FOTO))]
        public async Task<IHttpActionResult> PostFOTO(FOTO fOTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.FOTO.Add(fOTO);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = fOTO.FOTO_ID }, fOTO);
        }

        // DELETE: api/Fotos/5
        [ResponseType(typeof(FOTO))]
        public async Task<IHttpActionResult> DeleteFOTO(int id)
        {
            FOTO fOTO = await db.FOTO.FindAsync(id);
            if (fOTO == null)
            {
                return NotFound();
            }

            db.FOTO.Remove(fOTO);
            await db.SaveChangesAsync();

            return Ok(fOTO);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FOTOExists(int id)
        {
            return db.FOTO.Count(e => e.FOTO_ID == id) > 0;
        }
    }
}