using AccountBusinessLayer.Validations;

namespace AccountUnitTests.ValidationUnitTests
{
    public class FileAccountNumberValidatorUnitTests
    {
        [Theory]
        [InlineData("32999921", false)]
        [InlineData("3293982", true)]
        [InlineData("8293982ga", false)]
        [InlineData("329a982", false)]
        [InlineData("329398.", false)]
        [InlineData("3113902", true)]
        [InlineData("3113902p", true)]
        public void IsAccountNumberCountIsCorrect_AccountCountNumberTestCases_ReturnsAppropriateResult(string bankAccountNumber, bool expectedResult)
        {
            var fileAccountNumberValidator = new FileAccountNumberValidator();

            var result = fileAccountNumberValidator.IsAccountNumberCountIsCorrect(bankAccountNumber);

            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData("32999921", true)]
        [InlineData("4293982", true)]
        [InlineData("58293982ga", false)]
        [InlineData("18293982ga", false)]
        [InlineData("98293982ga", false)]
        [InlineData("a8293982ga", false)]
        [InlineData("28293982ga", false)]
        public void AccountInitialNumberIsValid_AccountInitialNumberTestCases_ReturnsAppropriateResult(string bankAccountNumber, bool expectedResult)
        {
            var fileAccountNumberValidator = new FileAccountNumberValidator();

            var result = fileAccountNumberValidator.AccountInitialNumberIsValid(bankAccountNumber);

            Assert.Equal(expectedResult, result);
        }
    }
}
