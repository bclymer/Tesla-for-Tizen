using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using Tesla.NET;
using TeslaTizen.Models;
using TeslaTizen.Utils;

namespace TeslaTizen.Data
{
    public class TeslaAPIWrapper: ITeslaAPIWrapper
    {
        private readonly HttpClient httpClient;
        private ITeslaClient teslaClient;

        public TeslaAPIWrapper() : this(new HttpClient()) { }

        public TeslaAPIWrapper(HttpClient httpClient)
        {
            this.httpClient = httpClient;
            ProductHeaderValue header = new ProductHeaderValue("Tesla_For_Tizen");
            ProductInfoHeaderValue userAgent = new ProductInfoHeaderValue(header);
            httpClient.DefaultRequestHeaders.UserAgent.Add(userAgent);
        }

        public TeslaAuthentication Login(string email, string password)
        {
            var clientKeys = LocalFileParser.GetTeslaClient();
            var client = new TeslaAuthClient(httpClient);
            var login = client.RequestAccessTokenAsync(
                clientId: clientKeys.ClientId,
                clientSecret: clientKeys.ClientSecret,
                email: email,
                password: password);
            return new TeslaAuthentication(login.Result.Data);
        }

        public void SetBearerToken(string bearerToken)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
            teslaClient = new Tesla.NET.TeslaClient(httpClient);
        }

        public List<TeslaVehicle> GetVehicles()
        {
            var apiVehicles = teslaClient.GetVehiclesAsync().Result;
            var vehilces = apiVehicles?.Data.Response.Where(c => c != null).Select(c => TeslaVehicle.From(c)).ToList();
            return vehilces ?? new List<TeslaVehicle>();
        }

        private void VerifyAuthentication()
        {
            var hasAuth = httpClient.DefaultRequestHeaders.Authorization != null && teslaClient != null;
            if (!hasAuth)
            {
                throw new InvalidOperationException("Attempting network request that requries authentication without setting the bearer token.");
            }
        }
    }
}
