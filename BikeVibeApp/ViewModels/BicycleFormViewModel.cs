using BikeVibeApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BikeVibeApp.ViewModels
{
    public class BicycleFormViewModel
    {
        public Bicycle Bicycle { get; set; }
        public SelectList Brands { get; set; }
        public SelectList BicycleTypes { get; set;}
    }
}
