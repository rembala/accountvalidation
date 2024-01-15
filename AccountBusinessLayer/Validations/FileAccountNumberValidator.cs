using AccountBusinessLayer.Validations.Interfaces;
using System.Text.RegularExpressions;

namespace AccountBusinessLayer.Validations
{
    public class FileAccountNumberValidator : IFileAccountNumberValidator
    {
        public bool IsAccountNumberCountIsCorrect(string accountNumber)
        {
            const int accountNumberCountWithoutSymbol = 7;

            const int accountNumberCountWithSymbol = 8;

            const char specificAccountNumberSymbol = 'p';

            var pattern = "^[0-9]+$";

            var isOnlyNumbers = Regex.IsMatch(accountNumber, pattern);

            bool acountNumbersLengthIsCorrect = accountNumber.Length == accountNumberCountWithoutSymbol;

            if (acountNumbersLengthIsCorrect && isOnlyNumbers)
            {
                return true;
            }

            var lastSymbol = accountNumber[accountNumber.Length - 1];

            if (lastSymbol == specificAccountNumberSymbol && accountNumber.Length == accountNumberCountWithSymbol) {
                return true;
            }

            return false;
        }

        public bool AccountInitialNumberIsValid(string accountNumber)
        {
            var pattern = @"^[3,4]\w+";

            var isMatch = Regex.IsMatch(accountNumber, pattern);

            return isMatch;
        }
    }
}
