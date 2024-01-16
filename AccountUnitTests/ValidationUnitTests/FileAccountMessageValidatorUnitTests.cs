using AccountBusinessLayer.Common.Interfaces;
using AccountBusinessLayer.Validations;
using Moq;

namespace AccountUnitTests.ValidationUnitTests
{
    public class FileAccountMessageValidatorUnitTests
    {
        private Mock<IMeasureTimeSpanForAccountValidation> _measureTimeSpanForAccountValidation =
            new Mock<IMeasureTimeSpanForAccountValidation>(MockBehavior.Strict);

        [Fact]
        public void GetErrorMessageIfAccountIsInvalid_NameAccountHasError_ShouldReturnErrorMessage()
        {
            string bankAccount = "thomas 32999921";

            _measureTimeSpanForAccountValidation
                .Setup(method =>
                    method.GetAccountValidationResultWithMeasurements(
                        It.IsAny<string>(), It.IsAny<string>())
                    )
                .Returns(() => (true, false, new Dictionary<string, TimeSpan>()));

            _measureTimeSpanForAccountValidation
                .Setup(method =>
                    method.GetFastestAccountValidation(It.IsAny<Dictionary<string, TimeSpan>>()))
                .Returns(new KeyValuePair<string, TimeSpan>());

            _measureTimeSpanForAccountValidation
                .Setup(method =>
                    method.GetSlowestAccountValidation(It.IsAny<Dictionary<string, TimeSpan>>()))
                .Returns(new KeyValuePair<string, TimeSpan>());

            var fileAccountValidator = new FileAccountMessageValidator(_measureTimeSpanForAccountValidation.Object);

            var returnedErrorMessage = fileAccountValidator.GetErrorMessageIfAccountIsInvalid(bankAccount);

            Assert.Equal("Account name - not valid for", returnedErrorMessage);
        }

        [Fact]
        public void GetErrorMessageIfAccountIsInvalid_AccountNumberIsNotCorrect_ShouldReturnErrorMessage()
        {
            string bankAccount = "Thomas 329999213#";

            _measureTimeSpanForAccountValidation
                .Setup(method =>
                    method.GetAccountValidationResultWithMeasurements(
                        It.IsAny<string>(), It.IsAny<string>())
                    )
                .Returns(() => (false, true, new Dictionary<string, TimeSpan>()));

            _measureTimeSpanForAccountValidation
                .Setup(method =>
                    method.GetFastestAccountValidation(It.IsAny<Dictionary<string, TimeSpan>>()))
                .Returns(new KeyValuePair<string, TimeSpan>());

            _measureTimeSpanForAccountValidation
                .Setup(method =>
                    method.GetSlowestAccountValidation(It.IsAny<Dictionary<string, TimeSpan>>()))
                .Returns(new KeyValuePair<string, TimeSpan>());

            var fileAccountValidator = new FileAccountMessageValidator(_measureTimeSpanForAccountValidation.Object);

            var returnedErrorMessage = fileAccountValidator.GetErrorMessageIfAccountIsInvalid(bankAccount);

            Assert.Equal("Account number - not valid for", returnedErrorMessage);
        }

        [Fact]
        public void GetErrorMessageIfAccountIsInvalid_AccountInitialNumberIsNotCorrect_ShouldReturnErrorMessage()
        {
            string bankAccount = "Thomas 132999921";

            _measureTimeSpanForAccountValidation
                .Setup(method =>
                    method.GetAccountValidationResultWithMeasurements(
                        It.IsAny<string>(), It.IsAny<string>())
                    )
                .Returns(() => (false, true, new Dictionary<string, TimeSpan>()));

            _measureTimeSpanForAccountValidation
                .Setup(method =>
                    method.GetFastestAccountValidation(It.IsAny<Dictionary<string, TimeSpan>>()))
                .Returns(new KeyValuePair<string, TimeSpan>());

            _measureTimeSpanForAccountValidation
                .Setup(method =>
                    method.GetSlowestAccountValidation(It.IsAny<Dictionary<string, TimeSpan>>()))
                .Returns(new KeyValuePair<string, TimeSpan>());

            var fileAccountValidator = new FileAccountMessageValidator(_measureTimeSpanForAccountValidation.Object);

            var returnedErrorMessage = fileAccountValidator.GetErrorMessageIfAccountIsInvalid(bankAccount);

            Assert.Equal("Account number - not valid for", returnedErrorMessage);
        }

        [Fact]
        public void GetErrorMessageIfAccountIsInvalid_AccountNumberAndNameIsNotCorrect_ShouldReturnErrorMessage()
        {
            string bankAccount = "aThomas 232999921";

            _measureTimeSpanForAccountValidation
                .Setup(method =>
                    method.GetAccountValidationResultWithMeasurements(
                        It.IsAny<string>(), It.IsAny<string>())
                    )
                .Returns(() => (true, true, new Dictionary<string, TimeSpan>()));

            _measureTimeSpanForAccountValidation
                .Setup(method =>
                    method.GetFastestAccountValidation(It.IsAny<Dictionary<string, TimeSpan>>()))
                .Returns(new KeyValuePair<string, TimeSpan>());

            _measureTimeSpanForAccountValidation
                .Setup(method =>
                    method.GetSlowestAccountValidation(It.IsAny<Dictionary<string, TimeSpan>>()))
                .Returns(new KeyValuePair<string, TimeSpan>());

            var fileAccountValidator = new FileAccountMessageValidator(_measureTimeSpanForAccountValidation.Object);

            var returnedErrorMessage = fileAccountValidator.GetErrorMessageIfAccountIsInvalid(bankAccount);

            Assert.Equal("Account name,account number - not valid for", returnedErrorMessage);
        }
    }
}
