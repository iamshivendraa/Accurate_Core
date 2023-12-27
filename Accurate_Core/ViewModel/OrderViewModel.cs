using Accurate_Core.Models;

namespace Accurate_Core.ViewModel
{
    public class OrderViewModel
    {
        public List<ExcelSample> ExcelData { get; set; }
        public List<ExcelSample> DbData { get; set; }

        // Constructor to initialize properties
        public OrderViewModel()
        {
            ExcelData = new List<ExcelSample>();
            DbData = new List<ExcelSample>();
        }
    }
}
