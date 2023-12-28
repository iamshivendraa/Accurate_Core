using System;
using System.Collections.Generic;

namespace Accurate_Core.Models
{
    public partial class ExcelDatum
    {
        public int Id { get; set; }
        public string StockNum { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Price { get; set; } = null!;
        public string TaxCode { get; set; } = null!;
    }
}
