using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Accurate_Core.Models
{
    public partial class OrderSummary
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Stock Number is required.")]
        public string StockNumber { get; set; } = null!;

        [Required(ErrorMessage = "Vehicle is required.")]
        public string Vehicle { get; set; } = null!;

        [Required(ErrorMessage = "VIN is required.")]
        public string Vin { get; set; } = null!;

        [Required(ErrorMessage = "Location is required.")]
        public string Location { get; set; } = null!;

        [Required(ErrorMessage = "Price is required.")]
        public string Price { get; set; } = null!;

        public bool? Matched { get; set; }

        public string? Grade { get; set; }

        public string? GradePrice { get; set; }
        public List<SelectListItem> GetGradeOptions()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Value = "GRADE 1", Text = "GRADE 1" },
                new SelectListItem { Value = "GRADE 2", Text = "GRADE 2" },
                new SelectListItem { Value = "GRADE 3", Text = "GRADE 3" }
            };
        }
    }

   
}
