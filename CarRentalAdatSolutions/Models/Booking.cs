using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace CarRentalAdatSolutions.Models
{
    public class Booking
    {
        [Key]
        public int Id { get; set; }

        //Customer making booking
        public String userId { get; set; }
        [Required, ForeignKey(name: "userId")]
        public ApplicationUser user { get; set; }
        //the vehicle that is been booked
        public int VehicleId { get; set; }
        [Required, ForeignKey(name: "VehicleId")]
        public virtual Vehicle vehicle { get; set; }
        
        //number of days been booked for
        public int NumberOfDays { get; set; }

        [Required]
        [Display(Name = "Pickup Date")]
        public DateTime PickUpDate { get; set; }

        [Required]
        [Display(Name = "Return Date")]
        public DateTime ReturnDate { get; set; }

        [Required]
        [Display(Name = "Pickup Address")]
        public String PickUpLocationAddress { get; set; }
        [Required]
        [Display(Name = "Return Address")]
        public String ReturnLocationAddress { get; set; }

        [ScaffoldColumn(false)]
        public Reciept Reciept { get; set; }


        //billing information
        public virtual Billing BillingAddress { get; set; }

       //when the booking was created
        public DateTime created { get; set; }
        public Booking()
        {
            created = DateTime.Now;
            Reciept = new Reciept();
            //vehicle = new Vehicle();
        }
    }


 
    public class Bookingvalidation
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public Booking BookingVal(Booking booking, ApplicationUser user)
        {
            Vehicle vehicle = db.Vehicles.Find((booking.VehicleId));
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
            return booking;
        }
    }



    //review booking details
    public class BookingOverview
    {
        public string ChargeWithoutTax { get; set; }
        public String TotalCharge { get; set; }
        public String Tax { get; set; }
        public String VehicleName { get; set; }
        public int NumberOfDays { get; set; }
        public String PickUpAddress { get; set; }
        public String ReturnAddress { get; set; }
        public String Dates { get; set; }
    }

}

