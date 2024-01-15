
namespace AccountBusinessLayer.Common.Interfaces
{
    public interface IMeasureTimeSpanForAccountValidation
    {
        (bool accountNameHasErrors, bool accountNumberHasErrors, Dictionary<string, TimeSpan> timeSpanByValidation)
            GetAccountValidationResultWithMeasurements(string accountName, string accountNumber);

        KeyValuePair<string, TimeSpan> GetFastestAccountValidation(Dictionary<string, TimeSpan> measuredValidatationTimeSpan);

        KeyValuePair<string, TimeSpan> GetSlowestAccountValidation(Dictionary<string, TimeSpan> measuredValidatationTimeSpan);
    }
}
