﻿using AzureCosmosDbSqlApiSample.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureCosmosDbSqlApiSample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LocationsController : ControllerBase
    {
        readonly CosmosClient _cosmosClient;
        Container locationContainer;
        public LocationsController(CosmosClient cosmosClient)
        {
            _cosmosClient = cosmosClient;
            locationContainer = cosmosClient.GetContainer("tetriscdb", "tetrisLocations");
        }

        [HttpGet]
        public IActionResult Get(string id)
        {
            return Ok("");
        }

        [HttpPost]
        public async Task<IActionResult> Post(Location location)
        {
            location.Id = Guid.NewGuid().ToString();
            var result = await locationContainer.CreateItemAsync(location, new PartitionKey(location.Country));
            return CreatedAtAction("Get", new { id = result.Resource.Id }, result.Resource);
        }
    }
}