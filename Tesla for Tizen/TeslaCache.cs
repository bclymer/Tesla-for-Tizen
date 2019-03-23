using System;
using System.Collections.Generic;
using System.Diagnostics;
using Newtonsoft.Json;
using Tesla_for_Tizen.Models;
using TeslaTizen.Models;
using TeslaTizen.Utils;
using Tizen.Security.SecureRepository;
using Xamarin.Forms;

namespace Tesla_for_Tizen
{
    public class TeslaCache : ITeslaCache
    {
        private static readonly string AuthKey = "authKey";
        private static readonly string VehiclesKey = "vehiclesKey";

        public void StoreAuthentication(TeslaAuthentication auth)
        {
            StoreData(auth, AuthKey);
        }

        public TeslaAuthentication GetAuthentication()
        {
            return FakeAuth();
            //return GetData<TeslaAuthentication>(AuthKey);
        }

        public void StoreVehicles(List<TeslaVehicle> vehicles)
        {
            StoreData(vehicles, VehiclesKey);
        }

        public List<TeslaVehicle> GetVehicles()
        {
            return new List<TeslaVehicle>
            {
                new TeslaVehicle
                {
                    Name = "Sonic",
                    Id = 12345,
                },
                new TeslaVehicle
                {
                    Name = "Jenny",
                    Id = 54321,
                }
            };
            //return GetData<List<TeslaVehicle>>(VehiclesKey);
        }

        private void StoreData(object data, string name)
        {
            try
            {
                var json = JsonConvert.SerializeObject(data);
                Application.Current.Properties[name] = json;
                Debug.WriteLine($"Storing cached data for {name}");
                //var alias = GetAlias(name);
                //DataManager.Save(alias, Encoding.UTF8.GetBytes(json), new Policy(name, true));
            }
            catch (Exception ex)
            {
                // TODO errors
                Console.WriteLine("RIP - " + ex.Message);
            }
        }

        private T GetData<T>(string name)
        {
            try
            {
                if (Application.Current.Properties.ContainsKey(name))
                {
                    var json = Application.Current.Properties[name] as string;
                    var data = JsonConvert.DeserializeObject<T>(json);
                    Debug.WriteLine($"Found cached data for {name}");
                    return data;
                }
                else
                {
                    Debug.WriteLine($"Missed cached data for {name}");
                    return default(T);
                }
                //var alias = GetAlias(name);
                //var jsonBinary = DataManager.Get(alias, name);
                //var json = Encoding.UTF8.GetString(jsonBinary);
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }

        private string GetAlias(string name)
        {
            return Manager.CreateFullAlias("com.bclymer.Tesla_for_Tizen", name);
        }

        private TeslaAuthentication FakeAuth()
        {
            var developer = LocalFileParser.GetDeveloper();
            return new TeslaAuthentication(
                accessToken: developer.AccessToken,
                tokenType: developer.TokenType,
                expiresIn: developer.ExpiresIn,
                createdAt: developer.CreatedAt,
                refreshToken: developer.RefreshToken
            );
        } 
    }
}
