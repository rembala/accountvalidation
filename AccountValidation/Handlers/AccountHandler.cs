using AccountBusinessLayer.Helpers.Interfaces;
using AccountPresentationLayer.DTOs;
using AccountPresentationLayer.Handlers.Interfaces;
using AccountValidation.DTOs;

namespace AccountPresentationLayer.Handlers
{
    public class AccountHandler : IAccountHandler
    {
        private readonly IFileAccountHelper _fileAccountHelper;
        private readonly ILogger<AccountHandler> _logger;

        public AccountHandler(IFileAccountHelper fileAccountHelper, ILogger<AccountHandler> logger)
        {
            _fileAccountHelper = fileAccountHelper;
            _logger = logger;
        }

        public async Task<AccountResponse> HandleAccountRequestAsync(AccountRequest accountRequest)
        {
			try
			{
                var invalidAccounts = await _fileAccountHelper.GetInvalidAccountsAsync(accountRequest.AccountInformationFile);

                return new AccountResponse { FileValid = !invalidAccounts.Any(), invalidLines = invalidAccounts };
            }
			catch (Exception ex)
			{
                _logger.Log(LogLevel.Error, $"Unexpected error occured, {ex.Message}");
				throw;
			}
        }
    }
}
