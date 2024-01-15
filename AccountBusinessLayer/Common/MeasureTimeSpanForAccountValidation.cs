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

        public Dictionary<string, TimeSpan> GetMeasureTimeSpanForAccountValidation(string accountName, string accountNumber, out bool nameAccountHasErrors, out bool accountNumberIsCorrect, out bool accountInitialNumberIsCorrect)
        {
            var timeSpanByValidation = new Dictionary<string, TimeSpan>();

            bool firstNameContainsAlphabeticCharacters;

            var firstNameAlphabeticCharactersStopWatch = Stopwatch.StartNew();

            try
            {
                firstNameContainsAlphabeticCharacters = _fileAccountNameValidator.NameContainsOnlyAlphabeticCharacters(accountName);
            }
            finally
            {
                firstNameAlphabeticCharactersStopWatch.Stop();
            }

            timeSpanByValidation.Add(AccountValidatorConstants.NameContainsOnlyAlphabeticalCharacters, firstNameAlphabeticCharactersStopWatch.Elapsed);

            bool firstNameIsUpperCase;

            var firstNameIsUppercaseTimeSpan = Stopwatch.StartNew();

            try
            {
                firstNameIsUpperCase = _fileAccountNameValidator.FirstNameIsUppercase(accountName);
            }
            finally
            {
                firstNameIsUppercaseTimeSpan.Stop();
            }

            timeSpanByValidation.Add(AccountValidatorConstants.NameIsUpperCase, firstNameIsUppercaseTimeSpan.Elapsed);

            nameAccountHasErrors = !firstNameContainsAlphabeticCharacters || !firstNameIsUpperCase;
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

            return timeSpanByValidation;
        }

        public KeyValuePair<string, TimeSpan> GetSlowestAccountValidation(Dictionary<string, TimeSpan> measuredValidatationTimeSpan)
            => measuredValidatationTimeSpan.MaxBy(account => account.Value);

        public KeyValuePair<string, TimeSpan> GetFastestAccountValidation(Dictionary<string, TimeSpan> measuredValidatationTimeSpan)
            => measuredValidatationTimeSpan.MinBy(account => account.Value);
    }
}
