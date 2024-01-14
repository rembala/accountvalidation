using AccountBusinessLayer.Helpers.Interfaces;
using AccountBusinessLayer.Validations.Interfaces;
using Microsoft.AspNetCore.Http;

namespace AccountBusinessLayer.Helpers
{
    public class FileAccountHelper : IFileAccountHelper
    {
        private readonly IFileAccountMainValidator _fileAccountValidator;

        public FileAccountHelper(IFileAccountMainValidator fileAccountValidator)
        {
            _fileAccountValidator = fileAccountValidator;
        }

        public async Task<List<string>> GetBankAccountsAsync(IFormFile file)
        {
            try
            {
                var bankAccountInformations = await GetBankAccountsInformationFromFileAsync(file);

                var invalidAccounts = _fileAccountValidator.GetInvalidAccounts(bankAccountInformations);

                return invalidAccounts;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private static async Task<List<string>> GetBankAccountsInformationFromFileAsync(IFormFile file)
        {
            var bankAccountsInformation = new List<string>();

            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                while (reader.Peek() >= 0)
                {
                    var bankAccountUserLine = await reader.ReadLineAsync();

                    if (string.IsNullOrEmpty(bankAccountUserLine))
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
