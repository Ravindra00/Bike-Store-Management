using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web;

namespace WebApplication1.Attributes
{
    public class CheckCaps : ValidationAttribute
    {
        public CheckCaps() : base("{0} contains invalid character.")
        {
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                byte[] ASCIIValues = Encoding.ASCII.GetBytes(value.ToString());
                foreach (byte b in ASCIIValues)
                {
                    if (b <= 65 || b >= 90)
                    {
                        var errorMessage = FormatErrorMessage(validationContext.DisplayName);
                        return new ValidationResult(errorMessage);

                    }
                }
            }
            return ValidationResult.Success;
        }
    }
}