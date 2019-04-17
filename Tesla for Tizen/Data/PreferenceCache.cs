using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeslaTizen.Utils;
using Tizen.Applications;

namespace TeslaTizen.Data
{
    public class PreferenceCache : ICache
    {
        public void Init()
        {
            // no-op
        }

        public Task<T> GetDataAsync<T>(string key)
        {
            try
            {
                if (Preference.Contains(key))
                {
                    var json = Preference.Get<string>(key);
                    var data = JsonConvert.DeserializeObject<T>(json);
                    LogUtil.Debug($"Found cached data for {key}");
                    return Task.FromResult(data);
                }
                else
                {
                    LogUtil.Debug($"Missed cached data for {key}");
                    return Task.FromResult(default(T));
                }
            }
            catch (Exception)
            {
                return Task.FromResult(default(T));
            }
        }

        public bool IsFreshInstall()
        {
            var exists = Preference.Contains("IsFreshInstall");
            Preference.Set("IsFreshInstall", true);
            return !exists;
        }

        public Task StoreDataAsync(object data, string key)
        {
            try
            {
                var json = JsonConvert.SerializeObject(data);
                Preference.Set(key, json);
            }
            catch (Exception ex)
            {
                LogUtil.Error(ex.Message);
            }
            return Task.CompletedTask;
        }
    }
}
