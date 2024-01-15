using AccountBusinessLayer.Validations.Interfaces;

namespace AccountBusinessLayer.Validations
{
    public class FileAccountValidator : IFileAccountValidator
    {
        private readonly IFileAccountMessageValidator _fileAccountMessageVaidator;

        public FileAccountValidator(IFileAccountMessageValidator fileAccountMessageVaidator)
        {
            _fileAccountMessageVaidator = fileAccountMessageVaidator;
        }

        public List<string> GetInvalidAccounts(List<string> bankAccountsInformation)
        {
            var errorMessageBuilder = new List<string>();

            for (int incrementor = 0; incrementor < bankAccountsInformation.Count; incrementor++)
            {
                var bankAccount = bankAccountsInformation[incrementor];

                var accountErrorMessage = _fileAccountMessageVaidator.GetErrorMessageIfAccountIsInvalid(bankAccount);

                if (!string.IsNullOrEmpty(accountErrorMessage))
                {
                    var errorMessage = @$"{accountErrorMessage} {incrementor + 1} line `{bankAccount}`";

                    errorMessageBuilder.Add(errorMessage);
                }
            }

            return errorMessageBuilder;
        }
    }
}
