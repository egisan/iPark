using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using iPark.DAL;


namespace iPark.Models
{
    public class Garage
    {
        public int Id { get; set; }
        [Required]
        [StringLength(30, ErrorMessage = "The Last {0} length can't be more {1} signs", MinimumLength = 3)]
        public string Name { get; set; }

        [Range(1, Int32.MaxValue, ErrorMessage = "The number of {0} must be between {1} and {2}")]
        public int Capacity { get; set; }


        public int GetFreeSpaces(GarageContext context, int garageId)
        {
            int result = 0;
           // result = context.Parkings
            return result;
        }

    }
}