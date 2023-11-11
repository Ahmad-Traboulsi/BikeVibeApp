using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace BikeVibeApp.Models
{
    public class Customer
    {
        [HiddenInput]
        public int Id { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [Display(Name= "Subscribe To News Letter")]
        public Boolean IsSubscribedToNewsLetter { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Birthdate")]
        public DateTime BirthDate { get; set; }

        [Display(Name = "Membership Type")]
        public int MembershipTypeId { get; set; }
        
        public MembershipType? MembershipType { get; set; }



    }
}
