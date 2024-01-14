namespace AccountBusinessLayer.Validations.Interfaces
{
    public interface IFileAccountNumberValidator
    {
        bool IsAccountNumberCountIsCorrect(string accountNumber);

        bool IsAccountInitialNumberIsValid(string accountNumber);
    }
}
