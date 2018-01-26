                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EAtmMvcSirajulApp.Models;

namespace EAtmMvcSirajulApp.Controllers
{
   // [Authorize]
    public class EatmAccountController : Controller
    {

        //private EAtmMvcSirajulAppContext db = new EAtmMvcSirajulAppContext();
       
        
        private ApplicationDbContext db = new ApplicationDbContext();

       

        // GET: EatmAccountModels
        // [Authorize]
        public ActionResult Index()
        {
           
            if (Session["sessionModel"] !=null)
            {
                // return Content("session is not set");
                return View(db.EatmAccounts.ToList());
              
            }
            return RedirectToAction("CustomerLogin");

        }

        // GET: EatmAccountModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EatmAccountModel eatmAccountModel = db.EatmAccounts.Find(id);
            if (eatmAccountModel == null)
            {
                return HttpNotFound();
            }
            return View(eatmAccountModel);
        }

        // GET: EatmAccountModels/Create
      //  [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: EatmAccountModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CardNumber,PinNumber,Balance")] EatmAccountModel eatmAccountModel)
        {
            if (ModelState.IsValid)
            {
                db.EatmAccounts.Add(eatmAccountModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(eatmAccountModel);
        }

        // GET: EatmAccountModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EatmAccountModel eatmAccountModel = db.EatmAccounts.Find(id);
            if (eatmAccountModel == null)
            {
                return HttpNotFound();
            }
            return View(eatmAccountModel);
        }

        // POST: EatmAccountModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CardNumber,PinNumber,Balance")] EatmAccountModel eatmAccountModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eatmAccountModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(eatmAccountModel);
        }

        // GET: EatmAccountModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EatmAccountModel eatmAccountModel = db.EatmAccounts.Find(id);
            if (eatmAccountModel == null)
            {
                return HttpNotFound();
            }
            return View(eatmAccountModel);
        }

        // POST: EatmAccountModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EatmAccountModel eatmAccountModel = db.EatmAccounts.Find(id);
            db.EatmAccounts.Remove(eatmAccountModel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }



        //=======================================
        

       // [AllowAnonymous]
        public ActionResult CustomerLogin(string returnUrl)
        {
            if (IsLogged())
            {
                EatmAccountModel sesModel = (EatmAccountModel)Session["sessionEAccount"];
               return RedirectToAction("CustomerDetails", "EatmAccount", new { Id = sesModel.Id });
            }
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult CustomerLogin(EatmAccountModel model, string returnUrl)
        {
           
           
            if (ModelState.IsValid)
            {
                  if (IsValidCustomer(model))
                  {
                     //Session["currentCutomerCardNumber"] = model.CardNumber;
                      model = GetAccountByCardNumber(model.CardNumber);
                      Session["sessionEAccount"] = model;
                    EatmAccountModel  sesModel=  (EatmAccountModel)Session["sessionEAccount"];
                    //  Session["IsLogin"] =true;
                    //return RedirectToLocal(returnUrl);
                     // return RedirectToAction("CustomerDetails","EatmAccount",new{Id= Session["currentCutomerCardNumber"].ToString()} );
                      return RedirectToAction("CustomerDetails","EatmAccount",new{Id= sesModel.Id} );//.Success("success message");
                  }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                }

            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

       

        public bool IsValidCustomer(EatmAccountModel model)
        {
            var card = model.CardNumber;
            var pin = model.PinNumber;
        
            var account =
                db.EatmAccounts.FirstOrDefault(x => x.CardNumber.Equals(card) && x.PinNumber.Equals(pin));

            if (account != null)
                return true;
            else
                return false;
        }

        public EatmAccountModel GetAccountByCardNumber(int card)

        {
            var account =db.EatmAccounts.FirstOrDefault(x => x.CardNumber.Equals(card));

            return account;
        }

        



        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("CustomerDetails", "EatmAccount");
            }
        }
        //=======================================


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult CustomerDetails(int? id)
        {
            if (IsLogged())
            {
                var sesModel2 = (EatmAccountModel)Session["sessionEAccount"];
                 id =sesModel2.Id;

               
                var eatmAccountModel = db.EatmAccounts.Find(id);
                if (eatmAccountModel == null)
                {
                    return HttpNotFound();
                }
                return View(eatmAccountModel);
            }
            else
            {
               return  RedirectToAction("CustomerLogin");
            }
        }

        public bool IsLogged()
        {
            return Session["sessionEAccount"]!= null;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CustomerDetails([Bind(Include = "Id,Balance")] EatmAccountModel eatmAccountModel)
        {
           
            if (ModelState.IsValid)
            {
                //var ent = db.Set<Ingredient>().SingleOrDefault(o => o.id == input.id)
                var ent = db.EatmAccounts.SingleOrDefault(o => o.Id.Equals(eatmAccountModel.Id));
                if (ent != null)
                {
                    ent.Balance = ent.Balance - eatmAccountModel.Balance;
                    //update or add transaction
                    var transaction = new TransactionModel();
                    transaction.EatmAccountModelId = ent.Id;
                    transaction.WithdrawalAmount = eatmAccountModel.Balance;
                    transaction.TransactionDate = DateTime.Now;
                    db.Transactions.Add(transaction);
                    // db.Transactions.AddOrUpdate(transaction);
                    // db.SaveChanges();
                    db.Entry(ent).State = EntityState.Modified;
                    db.SaveChanges();
                    // return RedirectToAction();
                    return View(ent);
                }

                //  db.EatmAccounts.FirstOrDefault(eatmAccountModel);
               
            }
            return View(eatmAccountModel);
        }


    }
}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   