
using AccountBusinessLayer.Validations;

namespace AccountUnitTests.ValidationUnitTests
{
    public class FileAccountNameValidatorUnitTests
    {
        [Theory]
        [InlineData("Thomas", true)]
        [InlineData("Richard", true)]
        [InlineData("XAEA-12", false)]
        [InlineData("Rose", true)]
        [InlineData("Bob", true)]
        [InlineData("michael", true)]
        [InlineData("Rob", true)]
        [InlineData("aAEA#", false)]
        [InlineData("aAEA.", false)]
        [InlineData("aAEA@", false)]
        [InlineData("aAEA*", false)]
        [InlineData("aAEA5", false)]
        public void NameContainsOnlyAlphabeticCharacters_AccountNameOnlyAlphabeticalTestCases_ReturnsAppropriateResult(string bankAccountName, bool expectedResult)
        {
            var fileAccountNameValidator = new FileAccountNameValidator();

            var result = fileAccountNameValidator.NameContainsOnlyAlphabeticCharacters(bankAccountName);

            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData("Thomas", true)]
        [InlineData("Richard", true)]
        [InlineData("Rose", true)]
        [InlineData("Bob", true)]
        [InlineData("michael", false)]
        [InlineData("Rob", true)]
        public void FirstNameIsUppercase_FirstLetterOfAccountNameIsUpperCaseTestCases_ReturnsAppropriateResult(string bankAccountName, bool expectedResult)
        {
            var fileAccountNameValidator = new FileAccountNameValidator();

            var result = fileAccountNameValidator.FirstNameIsUppercase(bankAccountName);

            Assert.Equal(expectedResult, result);
        }
    }
}
