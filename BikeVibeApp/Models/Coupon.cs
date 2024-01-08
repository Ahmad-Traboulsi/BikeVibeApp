using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BikeVibeApp.Models
{
    public class Coupon
    {
        public int Id { get; set; }
        public string Code { get; set; }

        [Column(TypeName = "decimal(2,1)")]
        public decimal Discount { get; set; }
        public bool IsActive { get; set; }

        [DataType(DataType.Date)]
        [Column(TypeName ="date")]
        public DateTime ExpiryDate { get; set; }
    }
}
