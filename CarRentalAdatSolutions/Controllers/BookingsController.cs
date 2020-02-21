using CarRentalAdatSolutions.Models;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace CarRentalAdatSolutions.Controllers
{
 

    public class BookingsController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        [Authorize]
        // GET: Bookings
        public ActionResult Index()
        {
            var user = db.Users.Find(User.Identity.GetUserId());

            var result = db.Bookings
                .Include(x => x.vehicle)
                .Include(x => x.Reciept)
                .Where(x => x.userId == user.Id)
                .OrderByDescending(x => x.created)
                .ToList();
            return View(result);
        }


        [Authorize]

        // GET: Booking/Details/5
        public ActionResult Details(int id)
        {
            
            return View();
        }

        //POST: Booking/Book
        [HttpPost]
        public ActionResult Book(Booking booking)
        {
           
            ApplicationUser user = db.Users.Include(x => x.Bookings).FirstOrDefault(x => x.Id == booking.userId);

            Vehicle vehicle = db.Vehicles.Find(booking.VehicleId);

            //calulate number days
            TimeSpan difference = booking.ReturnDate - booking.PickUpDate;
            if (difference.Days == 0)
            { booking.NumberOfDays = 1; }
            else
            {
                booking.NumberOfDays = difference.Days;
            }
            double amount = Int32.Parse(vehicle.RentalPrice) * booking.NumberOfDays;
            double tax_amount = amount * 0.03; //3% of amount
            booking.Reciept.ChargeAmount = amount;
            booking.Reciept.TAX = tax_amount;
            booking.Reciept.TotalCharges = (amount + tax_amount);
            booking.vehicle = vehicle;
            booking.user = user;
            vehicle.isBooked = true;

            JsonResult result = new JsonResult();

            db.Entry(vehicle).State = EntityState.Modified;

            user.Bookings.Add(booking);

           int res =  db.SaveChanges();

            //send email
            if(res > 0)
            {
                //send email
                SendMail email = new SendMail();
                if (email.SendEmail(user, vehicle, booking))
                {
                    result.Data = "success";
                }
            }
            

            return Json(result);
        }



        public ActionResult OverView(Booking booking)
        {
            ////find vehicle by vehicle ID
            ApplicationUser user = db.Users.Include(x => x.Bookings).FirstOrDefault(x => x.Id == booking.userId);

            Bookingvalidation bookingvalidation = new Bookingvalidation();

            Booking booking1 =  bookingvalidation.BookingVal(booking, user);
      
            BookingOverview overview = new BookingOverview
            {
                ChargeWithoutTax = String.Format("{0:c}",booking1.Reciept.ChargeAmount),
                Tax = String.Format("{0:c}", booking1.Reciept.TAX),
                TotalCharge = String.Format("{0:c}", booking1.Reciept.TotalCharges),
                ReturnAddress = booking1.ReturnLocationAddress,
                PickUpAddress = booking1.PickUpLocationAddress,
                NumberOfDays = booking1.NumberOfDays,
                VehicleName = booking1.vehicle.make + " " + booking1.vehicle.model,
                Dates = "("+booking1.PickUpDate.ToString("MMM dd")+"- "+booking1.ReturnDate.ToString("MMM dd, yyyy")+")"
            };

            JsonResult res = new JsonResult
            {
                ContentEncoding = Encoding.Default,
                ContentType = "application/json",
                Data = Json(overview),
            };

            return Json(overview);
        }
      

        [Authorize]
        // GET: Booking/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Booking/Edit/5
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

        [Authorize]
        // GET: Booking/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Booking/Delete/5
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


        [HttpPost]
        public ActionResult EstimateCharges(int vid, string fromdate, string todate)
        {
            //calculate Number of days
            //calulate number days
            TimeSpan difference = DateTime.Parse(todate) - DateTime.Parse(fromdate);
            int numdays;
            if (difference.Days == 0)
            { numdays = 1; }
            else
            {
                numdays = difference.Days;
            }
            //charge
            var vehicle = db.Vehicles.Find(vid);
            double charge = Double.Parse(vehicle.RentalPrice) * numdays;
            double tax = charge * 0.03;
            double total = tax + charge;
            return Json(new { charge=string.Format("{0:C}",charge),TAX= string.Format("{0:C}", tax), Total= string.Format("{0:C}", total), Days=numdays});

        }
  
    }

 
}