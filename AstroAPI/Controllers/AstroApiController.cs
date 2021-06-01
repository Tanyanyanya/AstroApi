using Amazon.DynamoDBv2;
using AstroAPI.Clients;
using AstroAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AstroAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AstroApiController : ControllerBase
    {
        private readonly ILogger<AstroApiController> _logger;
        private readonly PicOfDayClient _picOfDayClient;
        private readonly IDynamoDbClient _dynamoDbClient;
        private readonly AsteroidsApiClient _weatherNotifClient;




        public AstroApiController(ILogger<AstroApiController> logger,  PicOfDayClient spaceWeatherClient, IDynamoDbClient dynamoDbClient, AsteroidsApiClient weatherNotifClient )
        {
            _logger = logger;
            _picOfDayClient = spaceWeatherClient;
            _dynamoDbClient = dynamoDbClient;
            _weatherNotifClient = weatherNotifClient;


        }


        [HttpGet("PictureOfDay")]
        public async Task<PicOfDay> GetSpaceWeather()
        {

            var weather = await _picOfDayClient.GetSpaceWeather();
            return weather;
            //_spaceWeatherClient.GetSpaceWeatherInfo();

        }

        //[HttpGet("AsteroidApi")]
        //public async Task<AsteroidsResponse> GetAsteroidsApiResponse([FromQuery] AsteroidId asteroidId)
        //{

        //    var asteroid = await _weatherNotifClient.GetAsteroidsApi(asteroidId.id);
        //    var result = new AsteroidsResponse
        //    {
        //        Id = asteroid.Id,
        //        Name = asteroid.Name_limited,
        //        Magnitude = asteroid.Absolute_magnitude_h.ToString()
        //    };
        //    return result;
        //    //_spaceWeatherClient.GetSpaceWeatherInfo();

        //}

        //[HttpPost("AsteroidsApiPost")]
        //public async Task<IActionResult> AddToAsteroidsList([FromBody] AsteroidsResponse asteroidResponse)
        //{
        //    var data = new AsteroidsDBRepository
        //    {
        //        Id = asteroidResponse.Id,
        //        Name = asteroidResponse.Name,
        //        Magnitude = asteroidResponse.Magnitude
        //    };
 
        //    await _dynamoDbClient.PostDataIntoDb(data);

        //    return Ok();

        //}


        [HttpGet("asteroids")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _dynamoDbClient.GetAll();

            if (response == null)
                return NotFound("There are no records in db");

            var result = response;

            return Ok(result);


        }


        [HttpGet("asteroids/{id}")]
        public async Task<ActionResult<AsteroidsDBRepository>> Get(string id)
        {
            var response = await _dynamoDbClient.GetDataById(id);

            if (response == null)
                return NotFound("There are no records in db");

            var result = response;

            return Ok(result);

        }



        [HttpGet("eclipse/lunar_eclipse_types")]
        public async Task<IActionResult> GetAllLunarEclipseTypes()
        {
            var response = await _dynamoDbClient.GetAllEclipseTypes();

            if (response == null)
                return NotFound("There are no records in db");

            var result = response;

            return Ok(result);


        }


        [HttpGet("eclipse/{city}")]
        public async Task<ActionResult<EclipseDBRepository>> GetEclipse(string city)
        {
            var response = await _dynamoDbClient.GetEclipseByCity(city);

            if (response == null)
                return NotFound("There are no records in db");

            var result = response;

            return Ok(result);

        }



        [HttpGet("weather/types")]
        public async Task<IActionResult> GetAllSpaceWeatherTypes()
        {
            var response = await _dynamoDbClient.GetAllWeatherTypes();

            if (response == null)
                return NotFound("There are no records in db");

            var result = response;

            return Ok(result);


        }

        [HttpGet("weather/{type}")]
        public async Task<ActionResult<SpaceWeather>> GetSpaceWeatherByType(string type)
        {
            var response = await _dynamoDbClient.GetWeatherByType(type);

            if (response == null)
                return NotFound("There are no records in db");

            var result = response;

            return Ok(result);

        }

        [HttpGet("search_photo/{tag}")]
        public async Task<ActionResult<SpacePhotoModel>> GetSpaceSpacePhotoByType(string tag)
        {
            var response = await _dynamoDbClient.GetPhotoByTag(tag);

            if (response == null)
                return NotFound("There are no records in db");

            var result = response;

            return Ok(result);

        }



        [HttpGet("Mars_exploration")]
        public async Task<IActionResult> GetAllMarsExploration()
        {
            var response = await _dynamoDbClient.GetAllMarsInfo();

            if (response == null)
                return NotFound("There are no records in db");

            var result = response;

            return Ok(result);


        }


    }
}
