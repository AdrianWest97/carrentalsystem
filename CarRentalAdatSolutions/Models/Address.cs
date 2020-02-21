using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CarRentalAdatSolutions.Models
{
    public class Address
    {
        public int Id { get; set; }

        [Display(Name = "State")]
        public String State { get; set; }

        [Display(Name = "Street Line")]
        public String StrLine1 { get; set; }

        [Display(Name = "Street Line 2")]
        public String StrLine2 { get; set; }
       
        [Display(Name = "Country")]
        public String Country { get; set; }

        [Display(Name = "ZIP")]
        public String ZIP { get; set; }

        [Required]
        public virtual ApplicationUser User { get; set; }
    }
}