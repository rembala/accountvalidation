using AccountBusinessLayer.Helpers.Interfaces;
using AccountBusinessLayer.Validations.Interfaces;
using Microsoft.AspNetCore.Http;

namespace AccountBusinessLayer.Helpers
{
    public class FileAccountHelper : IFileAccountHelper
    {
        private readonly IFileAccountValidator _fileAccountValidator;

        public FileAccountHelper(IFileAccountValidator fileAccountValidator)
        {
            _fileAccountValidator = fileAccountValidator;
        }

        public async Task<List<string>> GetInvalidAccountsAsync(IFormFile file)
        {
            try
            {
                var bankAccountsInformation = await GetBankAccountsInformationFromFileAsync(file);

                var invalidAccounts = _fileAccountValidator.GetInvalidAccounts(bankAccountsInformation);

                return invalidAccounts;
            }
            catch 
            {
                throw;
            }
        }

        private async Task<List<string>> GetBankAccountsInformationFromFileAsync(IFormFile file)
        {
            var bankAccountsInformation = new List<string>();

            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                while (reader.Peek() >= 0)
                {
                    var bankAccountUserLine = await reader.ReadLineAsync();

                    if (string.IsNullOrWhiteSpace(bankAccountUserLine))
                    {
                        continue;
                    }

                    bankAccountsInformation.Add(bankAccountUserLine);
                }
            }

            return bankAccountsInformation;
        }
    }
}
