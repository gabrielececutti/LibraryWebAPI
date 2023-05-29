using System.Text.RegularExpressions;

namespace LibraryModel
{
    public class IsbnValidator
    {
        public static bool IsValid(string isbn)
        {
            if (string.IsNullOrEmpty(isbn)) return false;
            isbn = isbn.Replace(" ", "").Replace("-", "");
            if (isbn.Length == 10)
            {
                var regex = new Regex(@"^\d{9}[\d|X]$");
                return regex.IsMatch(isbn);
            }
            else if (isbn.Length == 13)
            {
                var regex = new Regex(@"^\d{13}$");
                return regex.IsMatch(isbn);
            }
            return false; 
        }
    }
}
