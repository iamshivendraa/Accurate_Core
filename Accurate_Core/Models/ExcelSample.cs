using System.ComponentModel.DataAnnotations;

namespace Accurate_Core.Models
{
    public class ExcelSample
    {
        [Key]
        public int Id {  get; set; }
        [Required]
        public string stockNum {  get; set; }
        [Required]
        public string description {  get; set; }
        [Required]
        public string price {  get; set; }
        [Required]
        public string taxCode {  get; set; }
    }
}
