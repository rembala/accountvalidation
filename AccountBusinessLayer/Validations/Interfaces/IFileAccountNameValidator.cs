namespace AccountBusinessLayer.Validations.Interfaces
{
    public interface IFileAccountNameValidator
    {
        public bool FirstNameContainsOnlyAlphabeticCharacters(string accountName);

        public bool FirstNameIsUppercase(string accountName);
    }
}
