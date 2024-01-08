using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BikeVibeApp.Models
{
    public class Rental
    {
        private byte? _rentaldays;
        public Rental()
        {
            this.RentalDate = DateTime.Now;
        }
        public Rental(Customer customer)
        {
            this.Customer = customer;
            this.CustomerId = customer.Id;
            this.RentalDate = DateTime.Now;
        }

        [HiddenInput]
        public int Id { get; set; }

        [HiddenInput]
        public int CustomerId { get; set; }

        [ValidateNever]
        public Customer? Customer { get; set; }

        [Required]
        public int BicycleId { get; set; }

        [ValidateNever]
        public Bicycle Bicycle { get; set; }


        [NotMapped]
        [ValidateNever]
        public string CouponCode { get; set; }

        [NotMapped]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public byte RentalDays   // property
        {
            
            get {
                if (_rentaldays.HasValue)
                    return _rentaldays.Value;

                if(ExpectedReturnDate.HasValue && RentalDate.HasValue)
                    return (byte)(ExpectedReturnDate.Value - RentalDate.Value).TotalDays;

                return 0;
            }   
            set { _rentaldays = value; }  // set method
        }

        public bool withCoupon { get; set; }

        [DataType(DataType.Date)]
        public DateTime? RentalDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? ExpectedReturnDate { get; set; }

        public bool isReturned { get; set; } //we can ignore this property but for performance reasons it is preffered to use it (index etc)
        public DateTime? ActualReturnDate { get; set; }
    }
}
