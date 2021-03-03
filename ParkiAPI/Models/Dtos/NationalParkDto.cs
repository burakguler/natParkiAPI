using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ParkiAPI.Models
{
    public class NationalParkDto
    {
      
        public int Id { get; set; }
        public string name { get; set; }
        public string location { get; set; }
        public float area { get; set; }
        public DateTime created { get; set; }
        public DateTime listingDate { get; set; }

    }
}
