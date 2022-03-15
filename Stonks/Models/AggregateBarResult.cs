namespace Stonks.Models;

public class AggregateBarResult
{
    // The trading volume of the symbol in the given time period.
    public double v { get; set; }

    // The volume weighted average price.
    public double vw { get; set; }

    // The open price for the symbol in the given time period.
    public double o { get; set; }

    // The close price for the symbol in the given time period.
    public double c { get; set; }

    // The highest price for the symbol in the given time period.
    public double h { get; set; }

    // The lowest price for the symbol in the given time period.
    public double l { get; set; }

    // The Unix Msec timestamp for the start of the aggregate window.
    public long t { get; set; }

    // The number of transactions in the aggregate window.
    public int n { get; set; }

    // DateTime
    public string d { get; set; }
}