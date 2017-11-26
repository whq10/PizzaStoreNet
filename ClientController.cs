using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Data;
using System.Data.Entity;

using System.Net;


using PizaStore.DAL;
using PizaStore.Models;
using PagedList;
using System.Data.Entity.Infrastructure;

namespace PizaStore.Controllers
{
    public class ClientController : Controller
    {
        private SchoolContext db = new SchoolContext();
        // GET: /Client/

        //public ActionResult Index()
        //{
        //    return View(db.Clients.ToList());
        //}

        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var clients = from s in db.Clients
                          select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                clients = clients.Where(s => s.LastName.Contains(searchString)
                                       || s.FirstMidName.Contains(searchString)
                                       || s.Tel.Contains(searchString)
                                       || s.StoreNum.Contains(searchString)
                                       );
            }
            switch (sortOrder)
            {
                case "name_desc":
                    clients = clients.OrderByDescending(s => s.LastName);
                    break;
                case "Date":
                    clients = clients.OrderBy(s => s.MembershipStartDate);
                    break;
                case "date_desc":
                    clients = clients.OrderByDescending(s => s.MembershipStartDate);
                    break;
                default:  // Name ascending 
                    clients = clients.OrderBy(s => s.LastName);
                    break;
            }

            int pageSize = 20;
            int pageNumber = (page ?? 1);
            return View(clients.ToPagedList(pageNumber, pageSize));
            
        }


        // GET: Client/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Client/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LastName, FirstMidName, MembershipStartDate, MembershipEndDate, Tel, StoreNum, Province, City, ActivationState")]Client client)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Clients.Add(client);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(client);
        }

        // GET: Client/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: Client/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var clientToUpdate = db.Clients.Find(id);
            if (TryUpdateModel(clientToUpdate, "",
               new string[] { "LastName", "FirstMidName", "MembershipStartDate", "MembershipEndDate", "ActivationState" }))
            {
                try
                {
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            return View(clientToUpdate);
        }

        // GET: Client/Delete/5
        public ActionResult Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: Student/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                Client client = db.Clients.Find(id);
                db.Clients.Remove(client);
                db.SaveChanges();
            }
            catch (RetryLimitExceededException/* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
            return RedirectToAction("Index");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        // GET: Student/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

    }
}
