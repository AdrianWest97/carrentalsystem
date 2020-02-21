using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Web;

namespace CarRentalAdatSolutions.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }


        public System.Data.Entity.DbSet<CarRentalAdatSolutions.Models.Vehicle> Vehicles { get; set; }
        public System.Data.Entity.DbSet<CarRentalAdatSolutions.Models.VehicleImage> VehicleImages { get; set; }
        public System.Data.Entity.DbSet<CarRentalAdatSolutions.Models.Booking> Bookings { get; set; }
        public System.Data.Entity.DbSet<CarRentalAdatSolutions.Models.Address> Addresses { get; set; }
        public System.Data.Entity.DbSet<CarRentalAdatSolutions.Models.WatchList> watches { get; set; }
        public System.Data.Entity.DbSet<CarRentalAdatSolutions.Models.Review> Reviews { get; set; }
        public System.Data.Entity.DbSet<CarRentalAdatSolutions.Models.Billing> BillingAddresses { get; set; }


    }
}