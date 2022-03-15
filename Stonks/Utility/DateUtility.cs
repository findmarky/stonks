namespace Stonks.Utility
{
    public static class DateUtility
    {
        private static readonly DateTime UnixEpoch = new(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static string ConvertUnixMSecTimestampToDate(long unixMillisecondsTimeStamp, string format)
        {
            return DateTimeFromUnixTimestampMillis(unixMillisecondsTimeStamp).ToString(format);
        }

        public static DateTime DateTimeFromUnixTimestampMillis(long millis)
        {
            return UnixEpoch.AddMilliseconds(millis);
        }
    }
}
