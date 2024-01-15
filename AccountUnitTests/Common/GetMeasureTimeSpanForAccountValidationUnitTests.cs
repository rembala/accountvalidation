using AccountBusinessLayer.Common;
using AccountBusinessLayer.Common.Constants;
using AccountBusinessLayer.Validations.Interfaces;
using Moq;

namespace AccountUnitTests.Common
{
    public class GetMeasureTimeSpanForAccountValidationUnitTests
    {
        private Mock<IFileAccountNumberValidator> _fileAccountNumberValidatorMock =
            new Mock<IFileAccountNumberValidator>(MockBehavior.Strict);

        private Mock<IFileAccountNameValidator> _fileAccountNameValidatorMock =
            new Mock<IFileAccountNameValidator>(MockBehavior.Strict);

        [Fact]
        public void GetMeasureTimeSpanForAccountValidation_MeasureAccountValidationShouldMatch_ReturnsMeasuredTimeSpanAccounts()
        {
            var expectedBankAccountValidationKeys = new List<string> {
                AccountValidatorConstants.NameContainsOnlyAlphabeticalCharacters,
                AccountValidatorConstants.NameIsUpperCase,
                AccountValidatorConstants.NumberMustBeSevenOrEight,
                AccountValidatorConstants.NumberMustStartWithThreeOrFour
            };

            var accountName = "Thomas";
            var accountNumber = "32999921";

            _fileAccountNameValidatorMock
                .Setup(method => method.NameContainsOnlyAlphabeticCharacters(accountName))
                .Returns(true);

            _fileAccountNameValidatorMock
                .Setup(method => method.FirstNameIsUppercase(accountName))
                .Returns(true);

            _fileAccountNumberValidatorMock
                .Setup(method => method.IsAccountNumberCountIsCorrect(accountNumber))
                .Returns(true);

            _fileAccountNumberValidatorMock
                .Setup(method => method.AccountInitialNumberIsValid(accountNumber))
                .Returns(true);

            var measureTimeSpanForAccountValidation = new MeasureTimeSpanForAccountValidation(
                _fileAccountNumberValidatorMock.Object,
                _fileAccountNameValidatorMock.Object
            );

            var nameAccountHasErrors = false;
            var accountNumberIsCorrect = false;
            var accountInitialNumberIsCorrect = false;

            measureTimeSpanForAccountValidation.GetMeasureTimeSpanForAccountValidation(
                accountName, accountNumber, out nameAccountHasErrors, out accountNumberIsCorrect, out accountInitialNumberIsCorrect);

            var measureTimeSpanAccountValidator = new MeasureTimeSpanForAccountValidation(_fileAccountNumberValidatorMock.Object, _fileAccountNameValidatorMock.Object);

            var result = measureTimeSpanAccountValidator
                .GetMeasureTimeSpanForAccountValidation(accountName, accountNumber, out nameAccountHasErrors, out accountNumberIsCorrect, out accountInitialNumberIsCorrect);

            foreach (var key in result.Keys)
            {
                Assert.Contains(key, expectedBankAccountValidationKeys);
            }
        }

        [Fact]
        public void GetSlowestAccountValidation_JonasValidationIsSlowest_ShouldReturnJonas()
        {
            var accountValidationTimeSpanByAccount = new Dictionary<string, TimeSpan> {
                { "Thomas", new TimeSpan(0,0, 12) },
                { "Arturas", new TimeSpan(0,0, 15) },
                { "Giedrius", new TimeSpan(0,0, 16) },
                { "Jonas", new TimeSpan(0,0, 17) },
            };

            var measureTimeSpanAccountValidator = new MeasureTimeSpanForAccountValidation(_fileAccountNumberValidatorMock.Object, _fileAccountNameValidatorMock.Object);

            var result = measureTimeSpanAccountValidator.GetSlowestAccountValidation(accountValidationTimeSpanByAccount);
            
            Assert.Equal("Jonas", result.Key);        
        }

        [Fact]
        public void GetFastestAccountValidation_ThomasValidatoinIsFastest_ShouldReturnThomas()
        {
            var accountValidationTimeSpanByAccount = new Dictionary<string, TimeSpan> {
                { "Thomas", new TimeSpan(0,0, 12) },
                { "Arturas", new TimeSpan(0,0, 15) },
                { "Giedrius", new TimeSpan(0,0, 16) },
                { "Jonas", new TimeSpan(0,0, 17) },
            };

            var measureTimeSpanAccountValidator = new MeasureTimeSpanForAccountValidation(_fileAccountNumberValidatorMock.Object, _fileAccountNameValidatorMock.Object);

            var result = measureTimeSpanAccountValidator.GetFastestAccountValidation(accountValidationTimeSpanByAccount);

            Assert.Equal("Thomas", result.Key);
        }
    }
}
