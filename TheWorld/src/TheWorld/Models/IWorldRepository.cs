using System.Collections.Generic;
using TheWorld.Models.Models;

namespace TheWorld.Models
{
    public interface IWorldRepository
    {
        IEnumerable<Trip> GetAllTrips();
    }
}