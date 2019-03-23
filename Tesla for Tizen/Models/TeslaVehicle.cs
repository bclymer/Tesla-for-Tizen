using Tesla.NET.Models;

namespace TeslaTizen.Models
{
    public class TeslaVehicle
    {
        public string Name { get; set; }
        public long Id { get; set; }

        public static TeslaVehicle From(IVehicle vehicle)
        {
            return new TeslaVehicle
            {
                Name = vehicle.DisplayName,
                Id = vehicle.Id,
            };
        }
    }
}
