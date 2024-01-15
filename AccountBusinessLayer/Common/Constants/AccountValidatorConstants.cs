
namespace AccountBusinessLayer.Common.Constants
{
    public struct AccountValidatorConstants
    {
        public const string NameContainsOnlyAlphabeticalCharacters = "Account should consist of alphabetic characters";
        public const string NameIsUpperCase = "Name is uppercase";
        public const string NumberMustBeSevenOrEight = "Account number must be 7 or 8 + 'p'";
        public const string NumberMustStartWithThreeOrFour = "Valid account number must start with a digit 3 or 4";
    }
}
