using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CarRentalAdatSolutions.Models
{
    public class WatchList
    {
        public int Id { get; set; }

        public string userId { get; set; }
        [Required, ForeignKey(name:"userId")]
        public ApplicationUser watchCustomer { get; set; }

        public Vehicle vehicle { get; set; }
    }
}