using Amazon.DynamoDBv2.Model;
using AstroAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AstroAPI.Clients
{
    public interface IDynamoDbClient
    {
        public Task<AsteroidsDBRepository> GetDataById(string id);
        //public Task<bool> PostDataToDb(CityDbRepository data);
        public Task PostDataIntoDb(AsteroidsDBRepository data);
        public Task<AsteroidsDBRepository> DeleteAsteroid(string id, string name, string magnitude);
        public Task<List<AsteroidsDBRepository>> GetAll();
        public Task<EclipseDBRepository> GetEclipseByCity(string city);
        public Task<List<MarsExplorationModel>> GetAllMarsInfo();
        public Task<List<LunarEcliseTypeModel>> GetAllEclipseTypes();
        public Task<List<SpaceWeather>> GetAllWeatherTypes();
        public Task<SpaceWeather> GetWeatherByType(string name);
        public Task<SpacePhotoModel> GetPhotoByTag(string tag);

    }
}
