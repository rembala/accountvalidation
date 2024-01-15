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
        public void GetErrorMessageIfAccountIsInvalid_NameAccountHasError_ReturnsErrorMessage()
        {
            var bankAccountInformations = new List<string> {
                "Thomas 32999921",
                "Richard 3293982",
                "XAEA-12 8293982",
                "Rose 329a982"
            };

            string bankAccount = "Thomas 32999921";

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
        public void GetErrorMessageIfAccountIsInvalid_AccountNumberIsNotCorrect_ReturnsErrorMessage()
        {
            var bankAccountInformations = new List<string> {
                "Thomas 32999921",
                "Richard 3293982",
                "XAEA-12 8293982",
                "Rose 329a982"
            };

            string bankAccount = "Thomas 32999921";

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
        public void GetErrorMessageIfAccountIsInvalid_AccountInitialNumberIsNotCorrect_ReturnsErrorMessage()
        {
            var bankAccountInformations = new List<string> {
                "Thomas 32999921",
                "Richard 3293982",
                "XAEA-12 8293982",
                "Rose 329a982"
            };

            string bankAccount = "Thomas 32999921";

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
        public void GetErrorMessageIfAccountIsInvalid_AccountNumberAndNameIsNotCorrect_ReturnsErrorMessage()
        {
            var bankAccountInformations = new List<string> {
                "Thomas 32999921",
                "Richard 3293982",
                "XAEA-12 8293982",
                "Rose 329a982"
            };

            string bankAccount = "Thomas 32999921";

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
