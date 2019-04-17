using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeslaTizen.Data
{
    public static class CacheFactory
    {
        public static ICache CreateCache()
        {
            return new PreferenceCache();
        }
    }
}
