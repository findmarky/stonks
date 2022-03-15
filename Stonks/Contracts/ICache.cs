namespace Stonks.Contracts
{
    public interface ICache<T> 
    {
        bool TryGetValue(string key, out T value);

        void AddOrReplace(string key, T value);
    }
}
