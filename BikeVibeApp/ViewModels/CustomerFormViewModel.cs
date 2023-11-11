using BikeVibeApp.Models;

namespace BikeVibeApp.ViewModels
{
    public class CustomerFormViewModel
    {
        public Customer Customer{ get; set; }
        public List<MembershipType> MembershipTypes { get; set; }
    }
}
