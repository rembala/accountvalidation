namespace AccountBusinessLayer.Validations.Interfaces
{
    public interface IFileAccountNameValidator
    {
        public bool NameContainsOnlyAlphabeticCharacters(string accountName);

        public bool FirstNameIsUppercase(string accountName);
    }
}
