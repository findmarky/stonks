namespace Stonks.Models
{
    public static class AggregateBarsExtensions
    {
        public static bool HasOkStatus(this AggregateBars aggregateBars)
        {
            return string.Equals(aggregateBars.status, "OK", StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
