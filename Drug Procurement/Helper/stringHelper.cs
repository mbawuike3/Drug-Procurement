using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Drug_Procurement.Helper
{
    public class StringHelper
    {
        public static bool IsAlphabet(string input)
        {
            string pattern = "^[A-Za-z]+$";
            return Regex.IsMatch(input!, pattern);
        }

        public static bool IsNumbers(string? input)
        {
            string pattern = "^[0-9]+$";
            return Regex.IsMatch(input!, pattern);
        }
        public static bool IsValidEmail(string? input)
        {
            EmailAddressAttribute emailValidator = new();
            return emailValidator.IsValid(input);
        }
        public static bool IsAlphaNumeric(string? input)
        {
            string pattern = "^[a-zA-Z0-9@#$%^&+=]*$";
            return Regex.IsMatch(input!, pattern);
        }
        public static bool IsValidatePrice(string priceString)
        {
            string pattern = @"^\d+(\.\d{1,2})?$";
            return Regex.IsMatch(priceString, pattern);
        }
    }
}
