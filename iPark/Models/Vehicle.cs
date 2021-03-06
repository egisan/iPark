﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace iPark.Models
{
    public class Vehicle
    {
        public int Id { get; set; }

        [Required]
        [StringLength(7, ErrorMessage = "{0}: Can't be more than {1} characters", MinimumLength =6)]
        public string RegNo { get; set; }

        [Required]
        public EnumEntities.Colors Color { get; set; }

        [Required]
        public EnumEntities.Vtypes VehicleType { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "{0}: Can't be less than {2} or more than {1} characters", MinimumLength = 2)]
        public string Make { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "{0}: Can't be less than {2} or more than {1} characters", MinimumLength =3)]
        public string Model { get; set; }

        [Range(2, 12, ErrorMessage = "The number of {0} must be between {1} and {2}")]
        public int Wheels { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime CheckIn { get; set; }

        [DataType(DataType.DateTime)]
        // [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? CheckOut { get; set; }
    }
}