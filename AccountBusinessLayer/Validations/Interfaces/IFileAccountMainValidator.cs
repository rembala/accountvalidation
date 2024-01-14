
namespace AccountBusinessLayer.Validations.Interfaces
{
    public interface IFileAccountMainValidator
    {
        List<string> GetInvalidAccounts(List<string> bankAccountsInformation);
    }
}
