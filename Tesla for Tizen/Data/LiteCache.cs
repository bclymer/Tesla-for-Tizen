using LiteCache.Tizen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tizen.Applications;

namespace TeslaTizen.Data
{
    public class LiteCache : ICache
    {
        public void Init()
        {
            Cotton.CacheDirectory = Application.Current.DirectoryInfo.Data;
        }

        public Task<T> GetDataAsync<T>(string key)
        {
            return Task.FromResult(Cotton.Current.Read<T>(key));
        }

        public bool IsFreshInstall()
        {
            var exists = Cotton.Current.IsExists("IsFreshInstall");
            Cotton.Current.Create("IsFreshInstall", true, TimeSpan.MaxValue);
            return !exists;
        }

        public Task StoreDataAsync(object data, string key)
        {
            Cotton.Current.Create(key, data, TimeSpan.MaxValue);
            return Task.CompletedTask;
        }
    }
}
