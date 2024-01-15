using AccountBusinessLayer.Common.Interfaces;
using AccountBusinessLayer.Validations.Interfaces;
using System.Text;

namespace AccountBusinessLayer.Validations
{
    public class FileAccountMessageValidator : IFileAccountMessageValidator
    {
        private readonly IMeasureTimeSpanForAccountValidation _measureTimeSpanForAccountValidation;

        public FileAccountMessageValidator(IMeasureTimeSpanForAccountValidation measureTimeSpanForAccountValidation)
        {
            _measureTimeSpanForAccountValidation = measureTimeSpanForAccountValidation;
        }

        public string GetErrorMessageIfAccountIsInvalid(string bankAccount)
        {
            var splittedBySpaceBankAccount = bankAccount.Split(' ');

            var accountName = splittedBySpaceBankAccount[0];

            var accountNumber = splittedBySpaceBankAccount[1];

            bool nameAccountHasErrors, accountNumberIsCorrect, accountInitialNumberIsCorrect;

            var measuredAccountValidatationTimeSpan = _measureTimeSpanForAccountValidation.GetMeasureTimeSpanForAccountValidation(
                accountName, accountNumber, out nameAccountHasErrors, out accountNumberIsCorrect, out accountInitialNumberIsCorrect);

            var fastestValidation = _measureTimeSpanForAccountValidation.GetFastestAccountValidation(measuredAccountValidatationTimeSpan);
            var slowestValidation = _measureTimeSpanForAccountValidation.GetSlowestAccountValidation(measuredAccountValidatationTimeSpan);

            DisplayBankAccountMeasureTimes(bankAccount, fastestValidation, slowestValidation);

            var finalErrorMessage = GetErrorMessageBasedOnAccountValidation(nameAccountHasErrors, accountNumberIsCorrect, accountInitialNumberIsCorrect);

            return finalErrorMessage;
        }

        private void DisplayBankAccountMeasureTimes(string bankAccount, KeyValuePair<string, TimeSpan> fastestValidation, KeyValuePair<string, TimeSpan> slowestValidation)
        {
            Console.WriteLine($"Fastest validation is when {fastestValidation.Key} for `{bankAccount}`");

            Console.WriteLine($"Slowest validation is when {slowestValidation.Key} for `{bankAccount}`");
        }

        private string GetErrorMessageBasedOnAccountValidation(bool nameAccountHasErrors, bool accountNumberIsCorrect, bool accountInitialNumberIsCorrect)
        {
            var accountNumberText = "Account number";

            var messageErrorBuilder = new StringBuilder();

            var accountNumberHasErrors = !accountNumberIsCorrect || !accountInitialNumberIsCorrect;

            if (nameAccountHasErrors)
            {
                //TODO:
                // Place constants somewhere else
                const string accountNameText = "Account name";

                messageErrorBuilder.Append(accountNameText);
            }

            if (accountNumberHasErrors)
            {
                if (nameAccountHasErrors)
                {
                    var replacement = "a";
                    accountNumberText = $",{string.Concat(replacement, accountNumberText.Substring(1))}";
                }

                messageErrorBuilder.Append(accountNumberText);
            }

            var finalErrorMessage = messageErrorBuilder.Length > 0 ? $"{messageErrorBuilder.ToString()} - not valid for" :
                string.Empty;

            return finalErrorMessage;
        }
    }
}
