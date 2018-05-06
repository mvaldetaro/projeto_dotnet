using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SocialNetwork.Web.Bussiness;
using SocialNetwork.Web.ViewModel;

namespace SocialNetwork.Web.Controllers
{
    public class UserController : Controller
    {
        // GET: User 
        // [TESTE] Remover antes de finalizar

        public ActionResult Index()
        {
            return View(UsersActions.GetAllAspNetUsersFromApi());
        }

        // GET: User/Account
        public ActionResult Account()
        {

            if (Session["AccessToken"] != null)
            {
                if(UsersActions.VerifyExistUserAccount((string)Session["user_uid"]) == true) {
                    ViewBag.VerifyExistUserAccount = "TRUE";
                    return View(UsersActions.GetUserFromApiByUid((string)Session["user_uid"]));
                } else
                {
                    ViewBag.VerifyExistUserAccount = "FALSE";
                    return View(UsersActions.GetUserFromApiByUid((string)Session["user_uid"]));
                }

                
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

        }

        // POST: User/CreateOrUpdate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateOrUpdate(Pessoa pessoa)
        {
            try
            {
                if (pessoa.uid != "") { 
                    if (UsersActions.VerifyExistUserAccount(pessoa.uid) == true)
                    {
                        //UPDATE
                        UsersActions.UpdateAccountPessoa(pessoa);
                    }
                    else
                    {
                        //CREATE
                        if (ModelState.IsValid)
                        {
                            UsersActions.CreateAccountPessoa(pessoa);
                        }

                        
                    }
                }

                return RedirectToAction("Account");
            }
            catch
            {
                return View();
            }
        }

        // POST: User/Edit/5
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

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: User/Delete/5
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
