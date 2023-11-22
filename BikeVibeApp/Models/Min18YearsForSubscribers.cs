using System.ComponentModel.DataAnnotations;

namespace BikeVibeApp.Models
{
    public class Min18YearsForSubscribers : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var customer = (Customer)validationContext.ObjectInstance;

            if(customer.MembershipTypeId <= 1)
            {
                return ValidationResult.Success;
            }

            if(customer.BirthDate == null)
            {
                return new ValidationResult("Birthdate is required");
            }


            var age = DateTime.Today.Year - customer.BirthDate.Year;
            return (age > 18) 
                ? ValidationResult.Success 
                : new ValidationResult("You must be at least 18 years old to subscribe as a member");
        }
    }
}
