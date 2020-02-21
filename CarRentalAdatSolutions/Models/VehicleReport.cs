using System;
using System.Collections.Generic;

namespace CarRentalAdatSolutions.Models
{
    public class VehicleReport
    {
        public string vehicleName { get; set; }
        public double TotalCharge { get; set; }
        public int NumberOfDays { get; set; }

        public VehicleReport()
        {
            TotalCharge = 0;
        }

        public List<double> Earnings(List<Vehicle> vehicles)
        {
            double yearlyEarning = 0.0;
            double montlyEarning = 0.0;

            List<double> earnings = new List<double>();

            //this year
            foreach (var v in vehicles)
            {
                //get book list
                foreach (var b in v.BookingList)
                {
                    //get current year
                    if (b.created.Year == DateTime.Now.Year)
                    {
                        yearlyEarning += b.Reciept.TotalCharges;
                    }
                }
            }
            montlyEarning = yearlyEarning / 12;
            earnings.Add(yearlyEarning);
            earnings.Add(montlyEarning);

            return earnings;
        }
    }
}