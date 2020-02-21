using CarRentalAdatSolutions.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;

[assembly: OwinStartupAttribute(typeof(CarRentalAdatSolutions.Startup))]

namespace CarRentalAdatSolutions
{
    public partial class Startup
    {
 

        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);



            ApplicationDbContext context = null ;
            SeedData(context);
        }

        public void SeedData(ApplicationDbContext context)
        {

            
            context = new ApplicationDbContext();
            //seed data

            if (!context.Roles.Any(r => r.Name == "Dealer"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Dealer" };

                manager.Create(role);
            }

            if (!context.Users.Any(u => u.UserName == "test@gmail.com"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser { UserName = "test@gmail.com", FullName = "Admin", Email = "test@gmail.com", IsDealer = true };

                manager.Create(user, "feb151997AZ!");
                manager.AddToRole(user.Id, "Dealer");
            }

            Vehicle vehicle1;
            Vehicle vehicle2;
            Vehicle vehicle3;

            if (!context.Vehicles.Any() || context.Vehicles.Count() < 0)
            {
                var cardealer = context.Users.FirstOrDefault(x => x.Email == "test@gmail.com");

                vehicle1 = new Vehicle()
                {
                    AirBag = true,
                    airCondition = true,
                    BreakAssist = true,
                    CDPlayer = true,
                    PowerSteering = true,
                    RentalPrice = "999",
                    Description = "lorem Ipsum is simply dummy text of the printing and typesetting industry. ",
                    Dealer = cardealer,
                    isBooked = false,
                    FuelType = "Petrol",
                    year = "2019",
                    ImageUrl = "https://media.zigcdn.com/media/model/2018/Oct/front-1-4-left-115973862_930x620.jpg",
                    make = "Suzuki",
                    model = "maruti suzuki-swift",
                    RegNumber = "DRVVH5"
                };
                context.Vehicles.Add(vehicle1);

                vehicle2 = new Vehicle()
                {
                    AirBag = true,
                    airCondition = true,
                    BreakAssist = true,
                    CDPlayer = true,
                    PowerSteering = true,
                    RentalPrice = "300",
                    Description = "lorem Ipsum is simply dummy text of the printing and typesetting industry.",
                    Dealer = cardealer,
                    isBooked = false,
                    FuelType = "Petrol",
                    year = "2014",
                    ImageUrl = "https://img-ik.cars.co.za/images/2019/1/Datsun%20Go%20review/tr:n-news_large/DSC_8939.jpg",
                    make = "Datsun",
                    model = "Datsun Go 1.2 Lux",
                    RegNumber = "DD12345"
                };
                context.Vehicles.Add(vehicle2);

                vehicle3 = new Vehicle()
                {
                    AirBag = true,
                    airCondition = true,
                    BreakAssist = true,
                    CDPlayer = true,
                    PowerSteering = true,
                    RentalPrice = "300",
                    Description = "lorem Ipsum is simply dummy text of the printing and typesetting industry.",
                    Dealer = cardealer,
                    isBooked = false,
                    FuelType = "Petrol",
                    year = "2018",
                    ImageUrl = "https://images.unsplash.com/photo-1563720223185-11003d516935?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=crop&w=1050&q=80",
                    make = "Land Rover",
                    model = "suv",
                    RegNumber = "SUV6789"
                };
                context.Vehicles.Add(vehicle3);
            }

            try
            {
                // Your code...
                // Could also be before try if you know the exception occurs in SaveChanges

                context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
            context.Dispose();
        }//end seed
    }
}