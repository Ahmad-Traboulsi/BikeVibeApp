using BikeVibeApp.Models;

namespace BikeVibeApp.ViewModels
{
    public class RentalFormViewModel
    {
        public Rental Rental { get; set; }
        public List<BicycleType> BicycleTypes { get; set; }
    }
}
