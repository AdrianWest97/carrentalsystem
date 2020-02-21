using CarRentalAdatSolutions.Models;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;

namespace CarRentalAdatSolutions.Controllers
{
    public class WatchController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [HttpPost]
        public ActionResult Watch(int? id, string q)
        {
            //check database if vehicle has already been added to watchlist by user
            //find current user
            ApplicationUser customer = db.Users.First(x => x.Email == User.Identity.Name);
            Vehicle vehicle = db.Vehicles.Find(id);
            WatchList watch = null;
            JsonResult result = new JsonResult();
            Debug.WriteLine(q);

            if (customer != null)
            {
                if (vehicle != null)
                {
                    if (q.Equals("watch"))
                    {
                        if (db.watches.FirstOrDefault(x => x.vehicle.Id == id) != null)
                        {
                            WatchList w = db.watches.FirstOrDefault(x => x.vehicle.Id == id);
                            if (w.watchCustomer.Id == customer.Id)
                            {
                                //user is already watching this vehicle
                                result.Data = "User already is watching";
                            }
                        }
                        else
                        {
                            watch = new WatchList { vehicle = vehicle, watchCustomer = customer };

                            if (vehicle.watchLists == null)
                            {
                                vehicle.watchLists = new List<WatchList>();
                            }

                            vehicle.watchLists.Add(watch);
                            //save to database
                            result.Data = "Added to watch list";
                        }
                    }
                    else if (q.Equals("unwatch"))
                    {
                        if (db.watches.FirstOrDefault(x => x.vehicle.Id == id && x.watchCustomer.Id == customer.Id) != null)
                            watch = db.watches.FirstOrDefault(x => x.vehicle.Id == id && x.watchCustomer.Id == customer.Id);
                        vehicle.watchLists.Remove(watch);
                        db.watches.Remove(watch);
                        result.Data = "unwatch success";
                    }
                    try
                    {
                        db.Entry(vehicle).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                    catch (DbEntityValidationException ex)
                    {
                        foreach (var eve in ex.EntityValidationErrors)
                        {
                            Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                                eve.Entry.Entity.GetType().Name, eve.Entry.State);
                            foreach (var ve in eve.ValidationErrors)
                            {
                                Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                    ve.PropertyName, ve.ErrorMessage);
                            }
                        }
                    }
                }
            }
            return result;
        }
    }
}