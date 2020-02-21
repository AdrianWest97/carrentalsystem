namespace CarRentalAdatSolutions.Models
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Web.Mvc;

    /// <summary>
    /// Defines the <see cref="Vehicle" />
    /// </summary>
    public class Vehicle
    {

        [Key]
        public int Id { get; set; }


        [Required, Display(Name = "Vehicle Brand")]
        [StringLength(20, ErrorMessage = "This field is required")]
        public String make { get; set; }



        [Required]
        [Display(Name = "Model")]
        public string model { get; set; }
        public IEnumerable<SelectListItem> Models { get; set; }

        [Required, Display(Name = "Vehicle Year")]
        [StringLength(20, ErrorMessage = "This field is required")]
        public String year { get; set; }

        [Required, Display(Name = "Vehicle Overview")]
        [StringLength(200, ErrorMessage = "Brief details about the vehicle")]
        public String Description { get; set; }

        [Required, Display(Name = "Liscense Plate #")]
        [StringLength(10, ErrorMessage = "This field is required")]
        // [RegularExpression("^[A-Z]{2}[0-9]{2}[A-Z]{2}[0-9]{4}$", ErrorMessage = "Invalid liscense number")]
        public String RegNumber { get; set; }

        [Required]
        [StringLength(7, ErrorMessage = "This field is required")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Must be a valid number")]
        [Display(Name = "Price per day")]
        public String RentalPrice { get; set; }


        [Display(Name = "Fuel type")]
        public string FuelType { get; set; }


        public DateTime Added { get; set; }


        public bool isBooked { get; set; }


        public int Views { get; set; }

        public virtual List<VehicleImage> Images { get; set; }

        public virtual ApplicationUser Dealer { get; set; }

        [Display(Name = "Air Conditioner")]
        public Boolean airCondition { get; set; }

        [Display(Name = "Power steering")]
        public Boolean PowerSteering { get; set; }


        [Display(Name = "CD Player")]
        public Boolean CDPlayer { get; set; }


        [Display(Name = "Driver Air Bag")]
        public Boolean AirBag { get; set; }

        [Display(Name = "Break Assist")]
        public Boolean BreakAssist { get; set; }

        [Required, Display(Name = "Speed (Mph)")]
        public int Speed { get; set; }


        public virtual List<WatchList> watchLists { get; set; }
        public virtual List<Booking> BookingList { get; set; }

        public virtual List<Review> Reviews { get; set; }

        public string ImageUrl { get; set; }
        public Vehicle()
        {
            Added = DateTime.Now;
            //BookingList = new List<Booking>();
            Reviews = new List<Review>();
            Images = new List<VehicleImage>();
        }
    }


    public class VehicleImage
    {
        [Key]
        public int Id { get; set; }
        public byte[] ImagePath { get; set; }
        public int Vehicle_Id { get; set; }

        [ForeignKey("Vehicle_Id")]
        public Vehicle vehicle { get; set; }
    }

    public class VehicleModelView
    {

        public IEnumerable<Vehicle> vehicles { get; set; }

        public List<VehicleReport> vehicleReports { get; set; }
        public virtual Booking Booking { get; set; }

        public Vehicle vehicle { get; set; }
        public VehicleModelView()
        {
            vehicleReports = new List<VehicleReport>();

            vehicles = new List<Vehicle>();
        }
    }

    public class VehicleDataModel
    {
        public String Name { get; set; }
        public string Image { get; set; }
        public String Brand { get; set; }
        public String Dealer { get; set; }
        public String FuelType { get; set; }
        public String RegNumber { get; set; }
        public string year { get; set; }
        public string RentalPrice { get; set; }
        public int id { get; set; }

    }


    public enum FuelType
    {
        gasoline,
        methanol,
        ethanol,
        diesel,
        lpg,
        cng,
        electric
    }
    //public enum TransmissionGearType
    //{
    //    auto,
    //    manual
    //};

    //public enum DriveModeType
    //{
    //    comfort,
    //    auto,
    //    sport,
    //    eco,
    //    manual,
    //    winter
    //};

}
