using AccountBusinessLayer.Validations;
using AccountBusinessLayer.Validations.Interfaces;
using Moq;

namespace AccountUnitTests.ValidationUnitTests
{
    public class FileAccountValidatorUnitTests
    {
        private Mock<IFileAccountMessageValidator> _fileAccountMessageValidatorMock = new Mock<IFileAccountMessageValidator>(MockBehavior.Strict);

        [Fact]
        public void GetInvalidAccounts_AccountErrorMessageIsNotEmoty_ReturnsPopulatedErrorMessages() {
            var bankAccountInformations = new List<string> {
                "Thomas 32999921",
                "Richard 3293982",
                "XAEA-12 8293982",
                "Rose 329a982"
            };

            var errorMessage = "account is invalid";

            _fileAccountMessageValidatorMock
                .Setup(method => 
                    method.GetErrorMessageIfAccountIsInvalid(It.Is<string>(item => bankAccountInformations.Any(account => account.Equals(item)))))
                .Returns(() => errorMessage);

            var fileAccountValidator = new FileAccountValidator(_fileAccountMessageValidatorMock.Object);

            var result = fileAccountValidator.GetInvalidAccounts(bankAccountInformations);

            for (int i = 0; i < bankAccountInformations.Count; i++)
            {
                Assert.Equal($"{errorMessage} {i + 1} line `{bankAccountInformations[i]}`", result[i]);
            }
        }

        [Fact]
        public void GetInvalidAccounts_AccountErrorMessageIsEmoty_ReturnsNothing()
        {
            var bankAccountInformations = new List<string> {
                "Thomas 32999921",
                "Richard 3293982",
            };

            _fileAccountMessageValidatorMock
                .Setup(method =>
                    method.GetErrorMessageIfAccountIsInvalid(It.Is<string>(item => bankAccountInformations.Any(account => account.Equals(item)))))
                .Returns(() => string.Empty);

            var fileAccountValidator = new FileAccountValidator(_fileAccountMessageValidatorMock.Object);

            var result = fileAccountValidator.GetInvalidAccounts(bankAccountInformations);

            Assert.Empty(result);
        }
    }
}
