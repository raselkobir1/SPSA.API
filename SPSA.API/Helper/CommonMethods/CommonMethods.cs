using FluentValidation.Results;
using System.Text.RegularExpressions;

namespace SPSA.API.Helper.CommonMethods
{
    public static class CommonMethods
    {
        public static string GetRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789"; // Characters to choose from
            var random = new Random();
            string randomString = new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray()); // Generate the random string

            return randomString;
        }
        public static DateTime GetCurrentTime()
        {
            return DateTime.UtcNow.Add(TimeSpan.FromHours(6));
        }
        public static List<string> ConvertFluentErrorMessages(List<ValidationFailure> errors)
        {
            List<string> errorsMessages = new List<string>();
            foreach (var failure in errors)
            {
                errorsMessages.Add(failure.ErrorMessage);
            }
            return errorsMessages;
        }

        public static bool ValidateUsingRegex(string emailAddress)
        {
            var pattern = @"^[a-zA-Z0-9](?!.*[._-]{2})[a-zA-Z0-9._-]*[a-zA-Z0-9]@[a-zA-Z0-9-]+(\.[a-zA-Z0-9-]+)*\.[a-zA-Z]{2,6}$";

            var regex = new Regex(pattern);
            return regex.IsMatch(emailAddress);
        }
    }
}
