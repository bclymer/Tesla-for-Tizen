using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeslaTizen.Models;

namespace TeslaTizen.Services
{
    public interface IProfileService
    {
        IObservable<List<Profile>> GetProfiles();
        IObservable<Profile> GetProfile(string profileId);
        Task DeleteProfileAsync(Profile profile);
        Task UpsertProfileAsync(Profile profile);
    }
}
