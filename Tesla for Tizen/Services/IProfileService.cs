using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeslaTizen.Models;

namespace TeslaTizen.Services
{
    public interface IProfileService
    {
        Task<IObservable<List<Profile>>> GetProfilesAsync();
        Task DeleteProfileAsync(Profile profile);
        Task UpsertProfileAsync(Profile profile);
    }
}
