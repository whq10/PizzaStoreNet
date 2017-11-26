using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PizaStore.DAL;
using PizaStore.Models;

namespace PizaStore.Controllers
{
    public class ReportController : Controller
    {
        //
        // GET: /Report/
        private SchoolContext db = new SchoolContext();
        public ActionResult Index()
        {
            int totalPoints = 0;
            var customers = from s in db.Customers
                            select s;
            foreach(Customer c in customers)
            {
                totalPoints += c.PointsAvailable;
            }
            //bruce test
            //totalPoints = 1200;

            ViewBag.TotalPoints = totalPoints;

            return View();
        }

        public ActionResult Month()
        {
            return View();
        }

    }
}
