
namespace AccountBusinessLayer.Common.Interfaces
{
    public interface IMeasureTimeSpanForAccountValidation
    {
        Dictionary<string, TimeSpan> GetMeasureTimeSpanForAccountValidation(
            string accountName, string accountNumber, out bool nameAccountHasErrors, out bool accountNumberIsCorrect, out bool accountInitialNumberIsCorrect);

        KeyValuePair<string, TimeSpan> GetFastestAccountValidation(Dictionary<string, TimeSpan> measuredValidatationTimeSpan);

        KeyValuePair<string, TimeSpan> GetSlowestAccountValidation(Dictionary<string, TimeSpan> measuredValidatationTimeSpan);
    }
}
