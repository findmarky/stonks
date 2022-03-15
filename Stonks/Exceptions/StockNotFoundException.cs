namespace Stonks.Exceptions
{
    public class StockNotFoundException : ApplicationException
    {
        public StockNotFoundException(string message) 
            : base(message)
        {
        }
    }
}
