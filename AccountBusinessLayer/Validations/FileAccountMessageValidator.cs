using AccountBusinessLayer.Common.Constants;
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

            var accountValidatationResult = _measureTimeSpanForAccountValidation.GetAccountValidationResultWithMeasurements(accountName, accountNumber);

            var fastestValidation = _measureTimeSpanForAccountValidation.GetFastestAccountValidation(accountValidatationResult.timeSpanByValidation);

            var slowestValidation = _measureTimeSpanForAccountValidation.GetSlowestAccountValidation(accountValidatationResult.timeSpanByValidation);

            DisplayBankAccountMeasuredTimes(bankAccount, fastestValidation, slowestValidation);

            var finalErrorMessage = GetErrorMessageBasedOnAccountValidation(accountValidatationResult.accountNameHasErrors, accountValidatationResult.accountNumberHasErrors);

            return finalErrorMessage;
        }

        private void DisplayBankAccountMeasuredTimes(string bankAccount, KeyValuePair<string, TimeSpan> fastestValidation, KeyValuePair<string, TimeSpan> slowestValidation)
        {
            Console.WriteLine($"Fastest validation is when {fastestValidation.Key} for `{bankAccount}`");

            Console.WriteLine($"Slowest validation is when {slowestValidation.Key} for `{bankAccount}`");
        }

        private string GetErrorMessageBasedOnAccountValidation(bool accountNameHasErrors, bool accountNumberHasErrors)
        {
            var accountNumberText = AccountValidatorConstants.AccountNumber;

            var messageErrorBuilder = new StringBuilder();

            if (accountNameHasErrors)
            {
                const string accountNameText = AccountValidatorConstants.AccountName;

                messageErrorBuilder.Append(accountNameText);
            }

            if (accountNumberHasErrors)
            {
                if (accountNameHasErrors)
                {
                    accountNumberText = $",{string.Concat("a", accountNumberText.Substring(1))}";
                }

                messageErrorBuilder.Append(accountNumberText);
            }

            var finalErrorMessage = messageErrorBuilder.Length > 0 
                ? $"{messageErrorBuilder.ToString()} - not valid for" 
                : string.Empty;

            return finalErrorMessage;
        }
    }
}
