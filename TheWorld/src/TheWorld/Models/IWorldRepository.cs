using System.Collections.Generic;
using System.Threading.Tasks;
using TheWorld.Models.Models;

namespace TheWorld.Models
{
    public interface IWorldRepository
    {
        IEnumerable<Trip> GetAllTrips();
        Trip GetTripByName(string tripName);

        void AddTrip(Trip trip);

        Task<bool> SaveChangesAsync();
        void AddStop(string tripName, Stop newStop);
    }
}