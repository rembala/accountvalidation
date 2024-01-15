using AccountBusinessLayer.Common.Constants;
using AccountBusinessLayer.Common.Interfaces;
using AccountBusinessLayer.Validations.Interfaces;
using System.Diagnostics;

namespace AccountBusinessLayer.Common
{
    public class MeasureTimeSpanForAccountValidation : IMeasureTimeSpanForAccountValidation
    {
        private readonly IFileAccountNumberValidator _fileAccountNumberValidator;
        private readonly IFileAccountNameValidator _fileAccountNameValidator;

        public MeasureTimeSpanForAccountValidation(IFileAccountNumberValidator fileAccountNumberValidator,
                                                   IFileAccountNameValidator fileAccountNameValidator)
        {
            _fileAccountNumberValidator = fileAccountNumberValidator;
            _fileAccountNameValidator = fileAccountNameValidator;
        }

        public (bool accountNameHasErrors, bool accountNumberHasErrors, Dictionary<string, TimeSpan> timeSpanByValidation)
            GetAccountValidationResultWithMeasurements(string accountName, string accountNumber)
        {
            var timeSpanByValidation = new Dictionary<string, TimeSpan>();

            bool accountNameContainsAlphabeticCharacters;

            var accountNameAlphabeticCharactersStopWatch = Stopwatch.StartNew();

            try
            {
                accountNameContainsAlphabeticCharacters = _fileAccountNameValidator.NameContainsOnlyAlphabeticCharacters(accountName);
            }
            finally
            {
                accountNameAlphabeticCharactersStopWatch.Stop();
            }

            timeSpanByValidation.Add(AccountValidatorConstants.NameContainsOnlyAlphabeticalCharacters, accountNameAlphabeticCharactersStopWatch.Elapsed);

            bool accountNameIsUpperCase;

            var accountNameIsUppercaseTimeSpan = Stopwatch.StartNew();

            try
            {
                accountNameIsUpperCase = _fileAccountNameValidator.FirstNameIsUppercase(accountName);
            }
            finally
            {
                accountNameIsUppercaseTimeSpan.Stop();
            }

            timeSpanByValidation.Add(AccountValidatorConstants.NameIsUpperCase, accountNameIsUppercaseTimeSpan.Elapsed);

            var nameAccountHasErrors = !accountNameContainsAlphabeticCharacters || !accountNameIsUpperCase;

            bool accountNumberIsCorrect;

            var accountNumberIsCorrectTimeSpan = Stopwatch.StartNew();

            try
            {
                accountNumberIsCorrect = _fileAccountNumberValidator.IsAccountNumberCountIsCorrect(accountNumber);
            }
            finally
            {
                accountNumberIsCorrectTimeSpan.Stop();
            }

            timeSpanByValidation.Add(AccountValidatorConstants.NumberMustBeSevenOrEight, accountNumberIsCorrectTimeSpan.Elapsed);

            bool accountInitialNumberIsCorrect;

            var accountInitialNumberTimeSpan = Stopwatch.StartNew();

            try
            {
                accountInitialNumberIsCorrect = _fileAccountNumberValidator.AccountInitialNumberIsValid(accountNumber);
            }
            finally
            {
                accountInitialNumberTimeSpan.Stop();
            }

            timeSpanByValidation.Add(AccountValidatorConstants.NumberMustStartWithThreeOrFour, accountInitialNumberTimeSpan.Elapsed);

            var accountNumberHasErrors = !accountNumberIsCorrect || !accountInitialNumberIsCorrect;

            return (nameAccountHasErrors, accountNumberHasErrors, timeSpanByValidation);
        }

        public KeyValuePair<string, TimeSpan> GetSlowestAccountValidation(Dictionary<string, TimeSpan> measuredValidatationTimeSpan)
            => measuredValidatationTimeSpan.MaxBy(account => account.Value);

        public KeyValuePair<string, TimeSpan> GetFastestAccountValidation(Dictionary<string, TimeSpan> measuredValidatationTimeSpan)
            => measuredValidatationTimeSpan.MinBy(account => account.Value);
    }
}
