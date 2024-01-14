using AccountBusinessLayer.Validations.Interfaces;
using System.Text;

namespace AccountBusinessLayer.Validations
{
    public class FileAccountMainValidator : IFileAccountMainValidator
    {
        private readonly IFileAccountNumberValidator _fileAccountNumberValidator;

        private readonly IFileAccountNameValidator _fileAccountNameValidator;

        public FileAccountMainValidator(IFileAccountNumberValidator fileAccountNumberValidator,
                                        IFileAccountNameValidator fileAccountNameValidator)
        {
            _fileAccountNumberValidator = fileAccountNumberValidator;
            _fileAccountNameValidator = fileAccountNameValidator;
        }

        public List<string> GetInvalidAccounts(List<string> bankAccountsInformation)
        {
            var messageBuilder = new List<string>();

            for (int incrementor = 0; incrementor < bankAccountsInformation.Count; incrementor++)
            {
                var iteratedItem = bankAccountsInformation[incrementor];

                var accountErrorMessage = GetErrorMessageIfAccountIsInvalid(iteratedItem);

                if (!string.IsNullOrEmpty(accountErrorMessage))
                {
                    var errorMessage = @$"{accountErrorMessage} {incrementor + 1} line `{iteratedItem}`";

                    messageBuilder.Add(errorMessage);
                }
            }

            return messageBuilder;
        }

        private string GetErrorMessageIfAccountIsInvalid(string iteratedItem)
        {
            const string accountNameText = "Account name";
            var accountNumberText = "Account number";

            var messageErrorBuilder = new StringBuilder();

            var nameIsValid = true;

            var accountNumberIsValid = true;

            var splittedBySpaceBankAccount = iteratedItem.Split(' ');

            var accountName = splittedBySpaceBankAccount[0];

            var accountNumber = splittedBySpaceBankAccount[1];

            var firstNameContainsAlphabeticCharacters = _fileAccountNameValidator.FirstNameContainsOnlyAlphabeticCharacters(accountName);

            var firstNameIsUpperCase = _fileAccountNameValidator.FirstNameIsUppercase(accountName);

            if (!firstNameContainsAlphabeticCharacters || !firstNameIsUpperCase)
            {
                nameIsValid = false;
            }

            var accountNumberIsCorrect = _fileAccountNumberValidator.IsAccountNumberCountIsCorrect(accountNumber);

            var accountInitialNumberIsCorrect = _fileAccountNumberValidator.IsAccountInitialNumberIsValid(accountNumber);

            if (!accountNumberIsCorrect || !accountInitialNumberIsCorrect)
            {
                accountNumberIsValid = false;
            }

            if (!nameIsValid)
            {
                messageErrorBuilder.Append(accountNameText);
            }

            if (!accountNumberIsValid)
            {
                if (!nameIsValid)
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
