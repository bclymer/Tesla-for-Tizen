using System.Threading.Tasks;

namespace TeslaTizen.Data
{
    public interface ICache
    {
        void Init();
        Task<T> GetDataAsync<T>(string key);
        Task StoreDataAsync(object data, string key);
        bool IsFreshInstall();
    }
}
