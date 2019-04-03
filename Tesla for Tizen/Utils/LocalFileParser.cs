using System.IO;
using System.Reflection;
using Newtonsoft.Json;
using TeslaTizen.Models;

namespace TeslaTizen.Utils
{
    public static class LocalFileParser
    {
        private static TeslaClient cachedClient;
        private static Developer cachedDeveloper;

        public static TeslaClient GetTeslaClient()
        {
            if (cachedClient != null)
            {
                return cachedClient;
            }
            var client = Load<TeslaClient>("TeslaTizen.res.teslaApiClient.json");
            cachedClient = client;
            return client;
        }

        public static Developer GetDeveloper()
        {
            if (cachedDeveloper != null)
            {
                return cachedDeveloper;
            }
            var developer = Load<Developer>("TeslaTizen.res.developer.json");
            cachedDeveloper = developer;
            return developer;
        }

        private static T Load<T>(string fileName)
        {
            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(App)).Assembly;
            Stream stream = assembly.GetManifestResourceStream(fileName);
            string json = "";
            using (var reader = new StreamReader(stream))
            {
                json = reader.ReadToEnd();
            }
            var data = JsonConvert.DeserializeObject<T>(json);
            return data;
        }
    }
}
