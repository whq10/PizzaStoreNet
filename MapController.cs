using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PizaStore.DAL;
using PizaStore.Models;

namespace PizaStore.Controllers
{
    public class MapController : Controller
    {

        private SchoolContext db = new SchoolContext();

        public ActionResult Index()
        {
            var customers = from s in db.Customers
                            select s;
            int total = 0;
            foreach (Customer c in customers)
            {
                total++;
            }
            ViewBag.Total = total;

            return View();
        }

        public ActionResult Test()
        {
            return View();
        }

        public ActionResult Test1()
        {
            return View();
        }

    }
}
