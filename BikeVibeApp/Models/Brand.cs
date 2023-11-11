using System.ComponentModel.DataAnnotations;

namespace BikeVibeApp.Models
{
    public class Brand
    {
        public short Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }
    }
}
