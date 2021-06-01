using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using AstroAPI.Extentions;
using AstroAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AstroAPI.Clients
{
    public class DynamoDbClient : IDynamoDbClient, IDisposable
    {

        public string _tableName;
        private readonly IAmazonDynamoDB _dynamoDb;

        public DynamoDbClient(IAmazonDynamoDB dynamoDB)
        {
            _dynamoDb = dynamoDB;
            _tableName = Constants.TableName;
        }


        public async Task<AsteroidsDBRepository> DeleteAsteroid(string id, string name, string magnitude)
        {
            var request = new DeleteItemRequest
            {
                TableName = _tableName,
                Key = new Dictionary<string, AttributeValue>
                    {
                        {"Id", new AttributeValue{S = id} },
                    {"Name", new AttributeValue{S = name} },
                    {"Magnitude", new AttributeValue{S = magnitude} }
                    }
            };

            var response = await _dynamoDb.DeleteItemAsync(request);

            var result = response.Attributes.ToClass<AsteroidsDBRepository>();

            return result;


        }

        public async Task<List<AsteroidsDBRepository>> GetAll()
        {
            var result = new List<AsteroidsDBRepository>();

            var request = new ScanRequest
            {
                TableName = _tableName,
            };

            var response = await _dynamoDb.ScanAsync(request);

            if (response.Items == null || response.Items.Count == 0)
                return null;

            foreach (Dictionary<string, AttributeValue> item in response.Items)
            {
                result.Add(item.ToClass<AsteroidsDBRepository>());

            }
            return result;
        }

        public async Task<AsteroidsDBRepository> GetDataById(string id)
        {
            var item = new GetItemRequest
            {
                TableName = _tableName,
                Key = new Dictionary<string, AttributeValue>
                    {
                        {"Id", new AttributeValue{S = id} }
                    }
            };

            var response = await _dynamoDb.GetItemAsync(item);

            if (response.Item == null || !response.IsItemSet)
                return null;

            var result = response.Item.ToClass<AsteroidsDBRepository>();

            return result;
        }

        public Task UpdateDataIntoDb()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public async Task<EclipseDBRepository> GetEclipseByCity(string city)
        {
            var _tableName2 = "lunar_eclipse";
            var items = new GetItemRequest
            {
                TableName = _tableName2,
                Key = new Dictionary<string, AttributeValue>
                    {
                        {"City", new AttributeValue{S = city} }
                    }
            };

            var response = await _dynamoDb.GetItemAsync(items);

            if (response.Item == null || !response.IsItemSet)
                return null;

            var result = response.Item.ToClass<EclipseDBRepository>();

            return result;
        }

        public async Task<List<MarsExplorationModel>> GetAllMarsInfo()
        {
            var result = new List<MarsExplorationModel>();

            var _tableName = "mars_exploration";

            var request = new ScanRequest
            {
                TableName = _tableName,
            };

            var response = await _dynamoDb.ScanAsync(request);

            if (response.Items == null || response.Items.Count == 0)
                return null;

            foreach (Dictionary<string, AttributeValue> item in response.Items)
            {
                result.Add(item.ToClass<MarsExplorationModel>());

            }
            return result;
        }

        public async Task<List<LunarEcliseTypeModel>> GetAllEclipseTypes()
        {
            var result = new List<LunarEcliseTypeModel>();

            var _tableName = "lunar_eclipse_forms";

            var request = new ScanRequest
            {
                TableName = _tableName,
            };

            var response = await _dynamoDb.ScanAsync(request);

            if (response.Items == null || response.Items.Count == 0)
                return null;

            foreach (Dictionary<string, AttributeValue> item in response.Items)
            {
                result.Add(item.ToClass<LunarEcliseTypeModel>());

            }
            return result;
        }

        public async Task<List<SpaceWeather>> GetAllWeatherTypes()
        {
            var result = new List<SpaceWeather>();

            var _tableName = "space_weather";

            var request = new ScanRequest
            {
                TableName = _tableName,
            };

            var response = await _dynamoDb.ScanAsync(request);

            if (response.Items == null || response.Items.Count == 0)
                return null;

            foreach (Dictionary<string, AttributeValue> item in response.Items)
            {
                result.Add(item.ToClass<SpaceWeather>());

            }
            return result;
        }
        public async Task<SpaceWeather> GetWeatherByType(string name)
        {
            var _tableName2 = "space_weather";
            var items = new GetItemRequest
            {
                TableName = _tableName2,
                Key = new Dictionary<string, AttributeValue>
                    {
                        {"Name", new AttributeValue{S = name} }
                    }
            };

            var response = await _dynamoDb.GetItemAsync(items);

            if (response.Item == null || !response.IsItemSet)
                return null;

            var result = response.Item.ToClass<SpaceWeather>();

            return result;
        }

        public async Task<SpacePhotoModel> GetPhotoByTag(string tag)
        {
            var _tableName = "photo_search";
            var items = new GetItemRequest
            {
                TableName = _tableName,
                Key = new Dictionary<string, AttributeValue>
                    {
                        {"Tag", new AttributeValue{S = tag} }
                    }
            };

            var response = await _dynamoDb.GetItemAsync(items);

            if (response.Item == null || !response.IsItemSet)
                return null;

            var result = response.Item.ToClass<SpacePhotoModel>();

            return result;
        }

        public async Task PostDataIntoDb(AsteroidsDBRepository data)
        {
            var item = new PutItemRequest
            {
                TableName = _tableName,
                Item = new Dictionary<string, AttributeValue>
                    {
                        {"Id", new AttributeValue{S = data.Id} },
                    {"Name", new AttributeValue{S = data.Name} },
                    {"Magnitude", new AttributeValue{S = data.Magnitude} }
                    }
            };

            var response = await _dynamoDb.PutItemAsync(item);
        }
    }
}
