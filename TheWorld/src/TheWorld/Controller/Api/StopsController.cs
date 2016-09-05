using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheWorld.Models;
using TheWorld.Models.Models;
using TheWorld.Services;
using TheWorld.ViewModels;

namespace TheWorld.Controllers.Api
{
    public class StopsController : Controller
    {
        private GeoCoordService _coordService;
        private ILogger<StopsController> _logger;
        private IWorldRepository _repository;

        public StopsController(IWorldRepository repository, ILogger<StopsController> logger, GeoCoordService coordService)
        {
            _repository = repository;
            _logger = logger;
            _coordService = coordService;
        }

        [HttpGet("/api/trips/{tripName}/stops")]
        public IActionResult Get(string tripName)
        {
            try
            {
                var trip = _repository.GetTripByName(tripName, User.Identity.Name);

                return Ok(Mapper.Map<IEnumerable<StopsViewModel>>(trip.Stops.OrderBy(s => s.Order).ToList()));
            }
            catch(Exception ex)
            {
                _logger.LogError("Failed do get stops: {0}",ex);
            }

            return BadRequest("Failed do get stops: ");

        }
        [HttpPost("/api/trips/{tripName}/stops")]
        public async Task<IActionResult> Post(string tripName , [FromBody]StopsViewModel vm) 
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newStop = Mapper.Map<Stop>(vm);

                    var result = await _coordService.GetCoordAsync(newStop.Name);

                    if (!result.Success)
                    {
                        _logger.LogError(result.Message);
                    }
                    else
                    {
                        newStop.Latitude = result.Latitude;
                        newStop.Longitude = result.Longitude;
                    }

                    _repository.AddStop(tripName, newStop,User.Identity.Name);

                    if(await _repository.SaveChangesAsync())
                    {
                        return Created($"/api/trips/{tripName}/stops/{newStop.Name}", Mapper.Map<StopsViewModel>(newStop));
                    }


                     
                }   
            }
            catch(Exception ex)
            {
                _logger.LogError("Failed to save new Stop {0}", ex);
            }

            return BadRequest("Failed to save new Stop ");

        }

    }
}
