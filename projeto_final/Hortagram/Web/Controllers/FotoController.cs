using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Web.Business;
using Web.Models;

namespace Web.Controllers
{
    public class FotoController : Controller
    {
        // GET: Foto
        public ActionResult Index()
        {
            return View();
        }

        // GET: Foto/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Foto/Create
        public async Task<ActionResult> Create()
        {
            FotoViewModel newFoto = new FotoViewModel();

            newFoto.USUARIO_ID = await Utils.UserId();

            return View(newFoto);
        }

        // POST: Foto/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(FotoViewModel newFoto)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    if (Request.Files.Count > 0)
                    {
                        var file = Request.Files[0];

                        if (file.FileName != null && file.ContentLength > 0)
                        {
                            newFoto.FOTO_URL = Blob.UploadBlob(file);
                        }
                    }

                    //POST API

                    await Photo.Publish(newFoto);

                    return RedirectToAction("Index", "Timeline");

                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Foto/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Foto/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Foto/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Foto/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
