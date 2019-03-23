using System.Collections.Generic;
using TeslaTizen.Models;

namespace TeslaTizen.Services
{
    public class ProfilesService: IProfileService
    {
        public List<Profile> GetProfiles()
        {
            return new List<Profile>
            {
                new Profile
                {
                    Name = "Test Profile 1",
                },
                new Profile
                {
                    Name = "Test Profile 2",
                },
                new Profile
                {
                    Name = "Test Profile 3",
                },
            };
        }
    }
}
