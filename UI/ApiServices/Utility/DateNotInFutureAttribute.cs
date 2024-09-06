using System.ComponentModel.DataAnnotations;

namespace ApiServices.Utility;

public class DateNotInFutureAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is DateTime dateOfBirth && dateOfBirth > DateTime.Today)
        {
            return new ValidationResult(ErrorMessage ?? "Date of Birth cannot be in the future.");
        }
        return ValidationResult.Success;
    }
}
