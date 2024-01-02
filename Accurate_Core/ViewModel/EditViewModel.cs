using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using Accurate_Core.Models;

namespace Accurate_Core.ViewModel
{
    public class EditViewModel
    {
        public List<OrderSummary> OrderList { get; set; }
        public OrderSummary OrderSummary { get; set; }

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
