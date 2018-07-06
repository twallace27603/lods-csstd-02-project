using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;

namespace CSSTDSolution.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(): base()
        {
            var testType = int.Parse(ConfigurationManager.AppSettings["TestType"]);
            var advanced = (testType == 3) || (testType == 12) || (testType > 32);
            ViewBag.TestType = testType;
            ViewBag.Advanced = advanced;
            if(advanced)
            {
                ViewBag.StorageConnection = ConfigurationManager.AppSettings["StorageConnection"];
                ViewBag.SQLConnection = ConfigurationManager.AppSettings["SQLConnection"];
                ViewBag.MySQLConnection = ConfigurationManager.AppSettings["MySQLConnection"];
                ViewBag.CosmosDBConnection = ConfigurationManager.AppSettings["CosmosDBConnection"];
                ViewBag.SearchConnection = ConfigurationManager.AppSettings["SearchConnection"];

            }
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Storage()
        {
            return View();
        }
        public ActionResult Relational()
        {
            return View();
        }
        public ActionResult NoSQL()
        {
            return View();
        }

 
    }
}