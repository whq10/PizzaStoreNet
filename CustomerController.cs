using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PizaStore.DAL;
using PagedList;
using PizaStore.Models;
using System.Data.Entity.Infrastructure;
using System.Net;


namespace PizaStore.Controllers
{
    public class CustomerController : Controller
    {
        static string cardNumber = null;
        static DateTime registerDate;


        private SchoolContext db = new SchoolContext();
        // GET: /Customer/

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

            var customers = from s in db.Customers
                          select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                customers = customers.Where(s => s.LastName.Contains(searchString)
                                       || s.FirstMidName.Contains(searchString)
                                       || s.Email.Contains(searchString)
                                       || s.CardNumber.Contains(searchString)
                                       || s.PhoneNumber.Contains(searchString)
                                       );
            }
            switch (sortOrder)
            {
                case "name_desc":
                    customers = customers.OrderByDescending(s => s.LastName);
                    break;
                case "Date":
                    customers = customers.OrderBy(s => s.Birthdate);
                    break;
                case "date_desc":
                    customers = customers.OrderByDescending(s => s.Birthdate);
                    break;
                default:  // Name ascending 
                    customers = customers.OrderBy(s => s.LastName);
                    break;
            }

            int pageSize = 20;
            int pageNumber = (page ?? 1);
            return View(customers.ToPagedList(pageNumber, pageSize));

        }


        // GET: Customer/Create
        public ActionResult Create()
        {
            System.Random rand = new Random();
            double num = System.Math.Round(rand.NextDouble() * 1000000, 6);
            int num_i = Convert.ToInt32(num);
            cardNumber = num_i.ToString();
            ViewBag.CardNumber = cardNumber;
            registerDate = System.DateTime.Now.Date;
            ViewBag.RegisterDate = registerDate;
            return View();
        }

        // POST: Client/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LastName, FirstMidName, Birthdate, Email, Postcode, CardNumber, RegisterDate, Status")]Customer customer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    customer.PointsAvailable = 100;//A new customer will get 100 point credits
                    customer.CardNumber = cardNumber;     
                    customer.RegisterDate = registerDate;
                    customer.Status = "Active";
                    db.Customers.Add(customer);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(customer);
        }

        // GET: Client/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
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
            var customerToUpdate = db.Customers.Find(id);
            if (TryUpdateModel(customerToUpdate, "",
               new string[] { "LastName", "FirstMidName", "Birthdate", "Email", "PostCode" }))
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
            return View(customerToUpdate);
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
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Student/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                Customer customer = db.Customers.Find(id);
                db.Customers.Remove(customer);
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

        // GET: Customer/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }
    }
}
