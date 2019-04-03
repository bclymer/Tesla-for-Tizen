using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeslaTizen.Utils;
using Xamarin.Forms;

namespace TeslaTizen.Data
{
    public class ApplicationCache : ICache
    {
        public void Init()
        {
            // no-op
        }

        public Task StoreDataAsync(object data, string name)
        {
            try
            {
                var json = JsonConvert.SerializeObject(data);
                Application.Current.Properties[name] = json;
                Application.Current.SavePropertiesAsync();
                LogUtil.Debug($"Storing cached data for {name}");
            }
            catch (Exception ex)
            {
                LogUtil.Error(ex.Message);
            }
            return Task.CompletedTask;
        }

        public Task<T> GetDataAsync<T>(string name)
        {
            try
            {
                if (Application.Current.Properties.ContainsKey(name))
                {
                    var json = Application.Current.Properties[name] as string;
                    var data = JsonConvert.DeserializeObject<T>(json);
                    LogUtil.Debug($"Found cached data for {name}");
                    return Task.FromResult(data);
                }
                else
                {
                    LogUtil.Debug($"Missed cached data for {name}");
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
            var exists = Application.Current.Properties.ContainsKey("IsFreshInstall");
            Application.Current.Properties["IsFreshInstall"] = true;
            return !exists;
        }
    }
}
