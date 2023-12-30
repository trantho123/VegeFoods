using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VEGEFOODS.Models.ClientModel
{
    public class OrderViewModel
    {
        [Required(ErrorMessage = "Name is required")]
        public String Name { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public String Address { get; set; }

        [Required(ErrorMessage = "Phone is required")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Invalid phone number.")]
        public String Phone { get; set; }
    }
}