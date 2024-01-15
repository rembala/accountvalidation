using AccountPresentationLayer.DTOs;
using AccountPresentationLayer.Handlers.Interfaces;
using AccountValidation.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace AccountValidation.Controllers
{ 
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountHandler _accountHandler;

        public AccountController( IAccountHandler accountHandler)
        {
            _accountHandler = accountHandler;
        }

        [HttpPost]
        public async Task<AccountResponse> ValidateAccountInformationAsync([FromForm] AccountRequest accountRequest)
        {
            var response = await _accountHandler.HandleAccountRequestAsync(accountRequest);

            return response;
        }
    }
}
