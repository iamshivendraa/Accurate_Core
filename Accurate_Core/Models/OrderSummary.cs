using System;
using System.Collections.Generic;

namespace Accurate_Core.Models
{
    public partial class OrderSummary
    {
        public int Id { get; set; }
        public string StockNumber { get; set; } = null!;
        public string Vehicle { get; set; } = null!;
        public string Vin { get; set; } = null!;
        public string Location { get; set; } = null!;
        public string Price { get; set; } = null!;
        public bool? Matched { get; set; }
        public string? Grade { get; set; }
        public string? GradePrice { get; set; }
    }
}
