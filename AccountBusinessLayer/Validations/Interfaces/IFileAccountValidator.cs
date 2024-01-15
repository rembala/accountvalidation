
namespace AccountBusinessLayer.Validations.Interfaces
{
    public interface IFileAccountValidator
    {
        List<string> GetInvalidAccounts(List<string> bankAccountsInformation);
    }
}
