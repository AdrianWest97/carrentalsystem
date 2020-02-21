using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
namespace CarRentalAdatSolutions.Models

{
    public class Billing
    {
        public int Id { get; set; }
        public String State { get; set; }

        [Required]
        [Display(Name = "Street Line")]
        public String StrLine1 { get; set; }

        [Display(Name = "Street Line 2 (Optional)")]
        public String StrLine2 { get; set; }

        [Required]
        [Display(Name = "Country")]
        public String Country { get; set; }

        [Display(Name = "ZIP")]
        public String ZIP { get; set; }
    }
}