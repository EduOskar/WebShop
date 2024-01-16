using PhoneNumbers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Models.Validation;
public class ValidPhoneNumberAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        var phoneNumber = value as string;
        if (string.IsNullOrWhiteSpace(phoneNumber))
        {
            return new ValidationResult("Phone number is required.");
        }

        try
        {
            var phoneNumberUtil = PhoneNumbers.PhoneNumberUtil.GetInstance();
            var numberProto = phoneNumberUtil.Parse(phoneNumber, null);
            if (!phoneNumberUtil.IsValidNumber(numberProto))
            {
                return new ValidationResult("Invalid phone number.");
            }
        }
        catch (NumberParseException)
        {
            return new ValidationResult("Invalid phone number format.");
        }

        return ValidationResult.Success!;
    }
}
