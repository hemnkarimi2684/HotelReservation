using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Extensions.BusinessExtensions
{
    public static class StringExtensions
    {
        private static readonly string _symbols = "!@#$%^&*";

        public static bool IsValidText(this string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return false;

            if (text.Length < 3)
                return false;

            foreach (char c in text)
            {
                if (_symbols.Contains(c))
                    return false;
            }

            return true;
        }

        public static void ToTitleCase(this string text)
        {
            var strings = text.Trim().Split(' ');

            var result = string.Empty;

            foreach(string s in strings)
            {
                var uppC = char.ToUpper(s[0]).ToString();

                result = s.Substring(1);

                result = result.Insert(0,uppC);
            }

            Console.WriteLine(result);
        }
    }
}
