using Microsoft.AspNetCore.Mvc;

namespace BikeVibeApp.Models
{
    public class MembershipType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal SignUpFee { get; set; }
        [HiddenInput]
        public Int16 Discount { get; set; }
    }
}
