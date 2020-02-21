using CarRentalAdatSolutions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarRentalAdatSolutions.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            //check database
            if (db.Vehicles != null && db.Vehicles.Count()>0)
            {
                return View(db.Vehicles.OrderByDescending(x => x.Added).Take(6).ToList());
            }

            return View();
        }

        //get ProfilePhoto
        public ActionResult GetPhoto(int id)
        {
            var user = db.Users.Find(id);

            ConvertToBase64 convert = new ConvertToBase64();
        
                return File(user.ProfilePhoto, "image/jpeg");
            
           
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Servies()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }


        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }

 

}