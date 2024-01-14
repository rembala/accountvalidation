using Microsoft.AspNetCore.Http;

namespace AccountBusinessLayer.Helpers.Interfaces
{
    public interface IFileAccountHelper
    {
        Task<List<string>> GetBankAccountsAsync(IFormFile file);
    }
}
