using AccountBusinessLayer.Validations.Interfaces;
using System.Text.RegularExpressions;

namespace AccountBusinessLayer.Validations
{
    public class FileAccountNameValidator : IFileAccountNameValidator
    {
        public bool FirstNameContainsOnlyAlphabeticCharacters(string accountName)
        {
            var pattern = "^[A-Za-z]+$";

            var isMatch = Regex.IsMatch(accountName, pattern);

            return isMatch;
        }

        public bool FirstNameIsUppercase(string accountName)
        {
            var pattern = "^[A-Z]\\w*";

            var isMatch = Regex.IsMatch(accountName, pattern);

            return isMatch;
        }
    }
}
