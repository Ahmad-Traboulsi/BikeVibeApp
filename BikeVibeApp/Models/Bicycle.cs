using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BikeVibeApp.Models
{
    
    public class Bicycle
    {
        [HiddenInput]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name Field is Required", AllowEmptyStrings =false)]
        public string Name { get; set; }

        [Range(5, 15)]
        [Display(Name = "Rental Rate (Daily)")]
        public double DailyRentalRate { get; set; }

        [RegularExpression("12|16|20|24|26|27.5|29")]
        public string Size { get; set; }

        [Display(Name="Date Added")]
        public DateTime Dateadded { get; set; }

        [Range(0,30)]
        
        [Display(Name = "Number In Stock")]
        public short NumberInStock { get; set; }

        [Required]
        [Display(Name = "Bicycle Type")]
        public byte BicycleTypeId { get; set; }
        public BicycleType? BicycleType { get; set; }

        [Required]
        [Display(Name = "Bicycle Brand")]
        public short BrandId { get; set; }
        public Brand? Brand { get; set; }
    }
}
