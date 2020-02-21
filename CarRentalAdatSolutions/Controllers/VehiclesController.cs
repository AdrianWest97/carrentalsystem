using CarRentalAdatSolutions.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace CarRentalAdatSolutions.Controllers
{
    public class VehiclesController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        // GET: Vehicles
        public ActionResult Index()
        {
            if (db.Vehicles.Count() > 0)
            {
                return View(db.Vehicles.Include(x => x.Images).OrderByDescending(x => x.Added).ToList());
            }
            else
            {
                return View();
            }
        }

        // GET: Vehicles/Details/5
        public ActionResult Details(int? id)
        {
            VehicleModelView VehicleModel = new VehicleModelView();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Vehicle vehicle = db.Vehicles
                .Include(x => x.Images)
                .Include(x => x.watchLists)
                .Include(x => x.Dealer)
                .Include(x => x.Reviews)
                .FirstOrDefault(x => x.Id == id);

            if (vehicle == null)
            {
                return HttpNotFound();
            }

            ViewBag.DealerInfo = db.Users.Find(vehicle.Dealer.Id);
            ViewBag.VehicleId = id;
            VehicleModel.vehicles = db.Vehicles
                .ToList()
                .Take(6);

            VehicleModel.vehicle = vehicle;

            return View(VehicleModel);
        }





        ////All dealers
        //// GET: Vehicles/Dealers
        public ActionResult Dealers()
        {
            return View(db.Users.Where(x => x.IsDealer).ToList());
        }

        //watch vehicle
        [HttpPost]
        public JsonResult Watch(FormCollection collection)
        {
            JsonResult result = new JsonResult
            {
                Data = collection["Id"]
            };
            return result;
        }

        // GET: Vehicles/Create
        public ActionResult Create()
        {
         
            if (User.IsInRole("Dealer"))
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        // POST: Vehicles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Vehicle vehicle, HttpPostedFileBase ImagePath, HttpPostedFileBase ImagePath2, HttpPostedFileBase ImagePath3)
        {
            Boolean flag = false;
            if (ModelState.IsValid)
            {
                ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Email == User.Identity.Name);
                if (currentUser != null && currentUser.IsDealer)
                {
                    vehicle.Dealer = currentUser;

                    //if registration number already exits
                    var res = db.Vehicles.FirstOrDefault(x => x.RegNumber == vehicle.RegNumber);
                    if (res == null)
                    {
                        vehicle.Images = new List<VehicleImage>();
                        ConvertToBase64 convertToBase = new ConvertToBase64();
                        if (ImagePath != null)
                        {
                            if (ImagePath.ContentLength > 0)
                            {
                                VehicleImage Img = new VehicleImage
                                {
                                    vehicle = vehicle
                                };
                                vehicle.Images.Add(convertToBase.ConvertImageToByte(Img, ImagePath));
                            }
                        }
                        if (ImagePath2 != null)
                        {
                            if (ImagePath2.ContentLength > 0)
                            {
                                VehicleImage Img = new VehicleImage
                                {
                                    vehicle = vehicle
                                };
                                vehicle.Images.Add(convertToBase.ConvertImageToByte(Img, ImagePath2));
                            }
                        }
                        if (ImagePath3 != null)
                        {
                            if (ImagePath3.ContentLength > 0)
                            {
                                VehicleImage Img = new VehicleImage
                                {
                                    vehicle = vehicle
                                };
                                vehicle.Images.Add(convertToBase.ConvertImageToByte(Img, ImagePath3));
                            }
                        }

                        db.Vehicles.Add(vehicle);
                        db.SaveChanges();
                       
                        flag = true;
                    }
                    else
                    {
                        ViewBag.Error = "Vehicle with registration number " + vehicle.RegNumber + " already exits";
                        flag = false;
                    }
                }
            }
            if (flag)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        // GET: Vehicles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicle vehicle = db.Vehicles.Find(id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            ViewBag.convert = new ConvertToBase64();

            return View(vehicle);
        }

        // POST: Vehicles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Vehicle vehicle, HttpPostedFileBase ImagePath, HttpPostedFileBase ImagePath2, HttpPostedFileBase ImagePath3)
        {

            ConvertToBase64 convertToBase = new ConvertToBase64();

            if (ModelState.IsValid)
            {

                if(vehicle.Images == null)
                {
                    vehicle.Images = new List<VehicleImage>();
                }

      
                if (ImagePath != null)
                {
                    if (ImagePath.ContentLength > 0)
                    {
                        VehicleImage Img = new VehicleImage
                        {
                            vehicle = vehicle
                        };
                        vehicle.Images.Add(convertToBase.ConvertImageToByte(Img, ImagePath));
                    }
                }

                if (ImagePath2 != null)
                {
                    if (ImagePath2.ContentLength > 0)
                    {
                        VehicleImage Img = new VehicleImage
                        {
                            vehicle = vehicle
                        };
                        vehicle.Images.Add(convertToBase.ConvertImageToByte(Img, ImagePath2));
                    }
                }

                if (ImagePath3 != null)
                {
                    if (ImagePath3.ContentLength > 0)
                    {
                        VehicleImage Img = new VehicleImage
                        {
                            vehicle = vehicle
                        };
                        vehicle.Images.Add(convertToBase.ConvertImageToByte(Img, ImagePath3));
                    }
                }

               
                db.Entry(vehicle).State = EntityState.Modified; //update vehicle

                db.SaveChanges(); //save chnages
             

                return RedirectToAction("manage");
            }
            return View(vehicle);
        }

        // GET: Vehicles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicle vehicle = db.Vehicles.Find(id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            db.Vehicles.Remove(vehicle);
            db.SaveChanges();
            return Json("success");
        }


        //get all vehicles created by dealer
        [Authorize(Roles = "Dealer")]
        public ActionResult Manage()
        {
         
            //find user
            var user = db.Users.Find(User.Identity.GetUserId());
            VehicleModelView vehicleModel = new VehicleModelView();
            if (db.Vehicles.Count() > 0)
            {
                //chart data
                vehicleModel.vehicles = db.Vehicles
                       .OrderByDescending(x => x.Added)
                       .Where(x => x.Dealer.Id == user.Id)
                       .ToList();

                vehicleModel.vehicleReports = new List<VehicleReport>();

                //get booking data
                foreach (var v in vehicleModel.vehicles)
                {
                    //find bookings
                    var booking = db.Bookings.Include(x => x.Reciept).FirstOrDefault(x => x.vehicle.Id == v.Id);
                    //get booking deatails
                    VehicleReport report = new VehicleReport();
                    try
                    {
                        report.vehicleName = v.make + " " + v.model;
                        report.TotalCharge = booking.Reciept.TotalCharges;
                        report.NumberOfDays = booking.NumberOfDays;
                      
                    }
                    catch (NullReferenceException)
                    {
                        report.TotalCharge = 0.1;
                        report.NumberOfDays = 0;
                    }
                    vehicleModel.vehicleReports.Add(report);
                }

                VehicleReport report2 = new VehicleReport();

                ViewBag.earnings = report2.Earnings(vehicleModel.vehicles.ToList());
             
                return View(vehicleModel);
            }
            return View();
        }

        //public ActionResult Test()
        //{
        //    return View();
        //}

        [HttpGet]
        public ActionResult AllVehicles()
        {
            ConvertToBase64 convertTo = new ConvertToBase64();

            List<VehicleDataModel> vehicleDataModels = new List<VehicleDataModel>();

            foreach (var v in db.Vehicles)
            {
                String img = "";
                if (v.Images.Count() > 0)
                {
                    img = convertTo.convertToBase64(v.Images.First().ImagePath);
                }
                else
                {
                    img = v.ImageUrl;
                }

                VehicleDataModel dataModel = new VehicleDataModel
                {
                    Brand = v.make,
                    Name = v.make + " " + v.model,
                    RegNumber = v.RegNumber,
                    RentalPrice = string.Format("{0:c}", double.Parse(v.RentalPrice)),
                    year = v.year,
                    id = v.Id,
                    Image = img
                   
                    
                };
               
                vehicleDataModels.Add(dataModel);
            }
          

            JsonResult res = new JsonResult
            {
                ContentEncoding = Encoding.Default,
                ContentType = "application/json",
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = Json(vehicleDataModels),
                MaxJsonLength = Int32.MaxValue
            };
            return res;
        }

        //review vehicle
        [HttpPost]
        public ActionResult Review(Review model)
        {
            if (model != null)
            {
                var vehicle = db.Vehicles.Find(model.VehicleId);
                Debug.WriteLine(Json(model));

                if (vehicle.Reviews == null)
                {
                    //check if user already review
                    vehicle.Reviews = new List<Review>();
                }

               if (vehicle.Reviews != null) { 

                    bool found = vehicle.Reviews.Any(x => x.Email.Equals(model.Email));
                    if (found)
                        return Json("You have already review this vehicle");
                    else
                    {
                        vehicle.Reviews.Add(model);
                        db.SaveChanges();
                        return (Json(model));
                    }
                }
            }
            return Json("Error");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}