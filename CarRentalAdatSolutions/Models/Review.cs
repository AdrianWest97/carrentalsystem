using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CarRentalAdatSolutions.Models
{
    public class Review
    {
        public int Id { get; set; }

        public int VehicleId { get; set; }
        public string Reviews { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public DateTime Created { get; set; }
        public double Rating { get; set; }

        public Review()
        {
            Created = DateTime.Now;
        }
  
    }
}