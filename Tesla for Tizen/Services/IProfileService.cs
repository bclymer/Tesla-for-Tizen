using System.Collections.Generic;
using TeslaTizen.Models;

namespace TeslaTizen.Services
{
    public interface IProfileService
    {
        List<Profile> GetProfiles();
        void DeleteProfile(Profile profile);
    }
}
