namespace Stonks.Models
{
    // Aggregate bars for a stock over a given date range in custom time window sizes.
    // https://polygon.io/docs/stocks/get_v2_aggs_ticker__stocksticker__range__multiplier___timespan___from___to
    public class AggregateBars
    {
        // The exchange symbol that this item is traded under.
        public string ticker { get; set; }

        // The number of aggregates (minute or day) used to generate the response.
        public int queryCount { get; set; }

        // The total number of results for this request.
        public int resultsCount { get; set; }

        // Whether or not this response was adjusted for splits.
        public bool adjusted { get; set; }

        // The status of this request's response.
        public string status { get; set; }

        public List<AggregateBarResult> results { get; set; } = new();

        public string request_id { get; set; }

        public int count { get; set; }
    }
}
