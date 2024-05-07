using System.Text.RegularExpressions;

namespace SrsBsnsChallenge.Server.Utils
{
    public class ValidationUtils
    {
        public static bool IsValidEmail(string email)
        {
            // Check if email is empty
            if (string.IsNullOrEmpty(email))
            {
                return false;
            }

            // Simple email format check using regex
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(email);
        }

        public static bool IsValidString(string itemString)
        {
            // Check if name is empty
            if (string.IsNullOrEmpty(itemString))
            {
                return false;
            }

            return true;
        }
    }
}
