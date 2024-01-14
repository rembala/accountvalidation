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
        private readonly ILogger<AccountController> _logger;
        private readonly IAccountHandler _accountHandler;

        public AccountController(ILogger<AccountController> logger, IAccountHandler accountHandler)
        {
            _logger = logger;
            _accountHandler = accountHandler;
        }

        [HttpPost]
        public async Task<AccountResponse> AccountInformationValidation([FromForm] AccountRequest accountRequest)
        {
            var response = await _accountHandler.HandleAccountRequestAsync(accountRequest);

            return response;
        }
    }
}
