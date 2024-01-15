using AccountBusinessLayer.Helpers.Interfaces;
using AccountPresentationLayer.DTOs;
using AccountPresentationLayer.Handlers.Interfaces;
using AccountValidation.DTOs;

namespace AccountPresentationLayer.Handlers
{
    public class AccountHandler : IAccountHandler
    {
        private readonly IFileAccountHelper _fileAccountHelper;

        public AccountHandler(IFileAccountHelper fileAccountHelper)
        {
            _fileAccountHelper = fileAccountHelper;
        }

        public async Task<AccountResponse> HandleAccountRequestAsync(AccountRequest accountRequest)
        {
			try
			{
                var invalidAccounts = await _fileAccountHelper.GetInvalidAccountsAsync(accountRequest.AccountInformationFile);

                return new AccountResponse { FileValid = !invalidAccounts.Any(), invalidLines = invalidAccounts };
            }
			catch
			{

				throw;
			}
        }
    }
}
