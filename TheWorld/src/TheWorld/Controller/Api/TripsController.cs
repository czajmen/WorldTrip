using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheWorld.Models;
using TheWorld.Models.Models;
using TheWorld.ViewModels;

namespace TheWorld.Controllers.Api
{

   // [Route("api/trips")]  Główna scieżka 
    public class TripsController : Controller
    {
        private IWorldRepository _repository;

        public TripsController(IWorldRepository repositpory)
        {
            _repository = repositpory;

        }
        [HttpGet("api/trips")]
        public IActionResult Get()
        {
            try
            {
                var results = _repository.GetAllTrips();

                return Ok(Mapper.Map<IEnumerable<TripViewModel>>(results));
            }
            catch(Exception ex)
            {
                return BadRequest("Error");
            }

   
        }

        [HttpPost("api/trips")]
        public IActionResult Post([FromBody]TripViewModel theTrip)  // theTrip ma mapować to co przyjdzie w poście na Trip
        {
            if (ModelState.IsValid)
            {
                var newTrip = Mapper.Map<Trip>(theTrip);

                return Created($"api/trips/{theTrip.Name}", Mapper.Map<TripViewModel>(newTrip));
            }
            else
            {
                return BadRequest(ModelState);
            }
            
        }

    }
}
