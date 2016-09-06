using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private ILogger<TripsController> _logger;
        private IWorldRepository _repository;

        public TripsController(IWorldRepository repositpory, ILogger<TripsController> logger)
        {
            _repository = repositpory;
            _logger = logger;

        }
        [HttpGet("api/trips")]
        [Authorize]
        public IActionResult Get()
        {
            try
            {
                var results = _repository.GetUserAllTrips(User.Identity.Name);

                return Ok(Mapper.Map<IEnumerable<TripViewModel>>(results));
            }
            catch(Exception ex)
            {
                _logger.LogError("");
                return BadRequest("Error");
            }

   
        }

        [HttpPost("api/trips")]
        public async Task<IActionResult> Post([FromBody]TripViewModel theTrip)  // theTrip ma mapować to co przyjdzie w poście na Trip
        {
            if (ModelState.IsValid)
            {
                var newTrip = Mapper.Map<Trip>(theTrip);

                newTrip.UserName = User.Identity.Name;

                _repository.AddTrip(newTrip);

                if( await _repository.SaveChangesAsync())
                {
                    return Created($"api/trips/{theTrip.Name}", Mapper.Map<TripViewModel>(newTrip));
                }
                else
                {
                    return BadRequest("Faied to save changes to DB" +ModelState.ErrorCount + User.Identity.Name);
                }

          
            }
            else
            {
                return BadRequest(ModelState);
            }
            
        }

        

    }
}
