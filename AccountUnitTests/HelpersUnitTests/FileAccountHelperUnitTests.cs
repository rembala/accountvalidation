using AccountBusinessLayer.Helpers;
using AccountBusinessLayer.Validations.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Moq;

namespace AccountUnitTests.Helpers
{
    public class FileAccountHelperUnitTests
    {
        private Mock<IFileAccountValidator> _fileAccountHelperMock = new Mock<IFileAccountValidator>(MockBehavior.Strict);

        [Fact]
        public async Task GetInvalidAccountsAsync_FileIsValid_ReturnsInvalidAccount()
        {
            var invalidAccount = "XAEA-12 8293982";

            var fileData = $"XAEA-12 8293982 {Environment.NewLine}Rose 329a982{Environment.NewLine}michael 3113902";

            var invalidAccounts = new List<string> { "XAEA-12 8293982", "Rose 329a982", "michael 3113902" };

            var fileName = "userAccounts.txt";
             
            var stream = new MemoryStream();

            var writer = new StreamWriter(stream);

            writer.Write(fileData);

            writer.Flush();

            stream.Position = 0;

            var file = new FormFile(stream, 0, stream.Length, "id_from_form", fileName);

            _fileAccountHelperMock
                .Setup(method => method
                    .GetInvalidAccounts(It.Is<List<string>>(accountsFromFile =>
                        accountsFromFile.Any(accountFromFile => invalidAccounts.Any(invalidAccount => invalidAccount.Equals(accountFromFile)))))
                    )
                .Returns(new List<string> { invalidAccount });

            var fileAccountHelper = new FileAccountHelper(_fileAccountHelperMock.Object);

            var returnInvalidAccount = await fileAccountHelper.GetInvalidAccountsAsync(file);

            Assert.Contains(invalidAccount, returnInvalidAccount);
        }

        [Fact]
        public async Task GetInvalidAccountsAsync_FileIsNotValid_ThrowsNullReferenceException()
        {
            IFormFile file = null;

            _fileAccountHelperMock.Setup(method => method.GetInvalidAccounts(It.IsAny<List<string>>()));

            var fileAccountHelper = new FileAccountHelper(_fileAccountHelperMock.Object);

            await Assert.ThrowsAsync<ArgumentException>(() => fileAccountHelper.GetInvalidAccountsAsync(file));
        }

        [Fact]
        public async Task GetInvalidAccountsAsync_FileIsValid_With_Empty_Spaces_ReturnsInvalidAccount()
        {
            var invalidAccount = "XAEA-12 8293982";

            var fileData = @$"{invalidAccount}{Environment.NewLine}   {Environment.NewLine}";

            var invalidAccounts = new List<string> { invalidAccount };

            var fileName = "userAccounts.txt";

            var stream = new MemoryStream();

            var writer = new StreamWriter(stream);

            writer.Write(fileData);

            writer.Flush();

            stream.Position = 0;

            var file = new FormFile(stream, 0, stream.Length, "id_from_form", fileName);

            _fileAccountHelperMock
                .Setup(method => method
                    .GetInvalidAccounts(It.Is<List<string>>(accountsFromFile =>
                        accountsFromFile.Any(accountFromFile => invalidAccounts.Any(invalidAccount => invalidAccount.Equals(accountFromFile)))))
                    )
                .Returns(new List<string> { invalidAccount });

            var fileAccountHelper = new FileAccountHelper(_fileAccountHelperMock.Object);

            var returnInvalidAccount = await fileAccountHelper.GetInvalidAccountsAsync(file);

            Assert.Contains(invalidAccount, returnInvalidAccount);
        }
    }
}
