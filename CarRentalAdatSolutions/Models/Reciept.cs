using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CarRentalAdatSolutions.Models
{
    //Create a user reciept
    public class Reciept
    {
        [Key]
        public int Id { get; set; }
        public double ChargeAmount { get; set; }
        public double TAX { get; set; }

        //TAX + ChargeAmount
        public double TotalCharges { get; set; }

        public Reciept()
        {
            ChargeAmount = 0;
            TotalCharges = 0;
            TAX = 0.0;
        }
    }
}