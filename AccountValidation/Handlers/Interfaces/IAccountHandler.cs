using AccountPresentationLayer.DTOs;
using AccountValidation.DTOs;

namespace AccountPresentationLayer.Handlers.Interfaces
{
    public interface IAccountHandler
    {
        Task<AccountResponse> HandleAccountRequestAsync(AccountRequest accountRequest);
    }
}
