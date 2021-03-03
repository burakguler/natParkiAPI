using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ParkiAPI.Models
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class NationalPark

    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public string location { get; set; }
        public string area { get; set; }
        public DateTime created { get; set; }
        public DateTime listingDate { get; set; }


    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
