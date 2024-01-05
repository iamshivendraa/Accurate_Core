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

        
    }
}
