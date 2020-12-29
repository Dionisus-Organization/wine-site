using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Cache;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using Cassandra;
using Cassandra.DataStax.Graph;
using Cassandra.Mapping;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Configuration.UserSecrets;
using wineApi.Cassandra;

namespace wineApi.Controllers
{
    [Route("wine")]
    [ApiController]
    public class WineController : ControllerBase
    {
        /// <summary>
        /// Name of table in wine_keypspace
        /// </summary>
        private const string _tableName = "wine";
        /// <summary>
        /// Number of elements to display on a page
        /// </summary>
        private const int _pageSize = 20;

        /// <summary>
        /// Handle get request with paginating
        /// </summary>
        /// <param name="page">Selected page</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<WineModel>> GetWines([FromQuery]int page)
        {
            int startIndex = _pageSize * (page - 1);

            Cql cql = new( $"SELECT * FROM {_tableName} where id >= ? and id < ? allow filtering", startIndex, startIndex + _pageSize );

            var temp = await CassandraConnection.GetInstance().GetByRequestData<WineModel>( cql );
            List<WineModel> result = temp.ToList().OrderBy( wine => wine.Id ).ToList();

            return result;
        }

        /// <summary>
        /// Get specifed wine
        /// </summary>
        /// <param name="id">Id of wine</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<WineModel>> GetWine(int id)
        {
            Cql cql = new($"WHERE id=? allow filtering", id);
            return await CassandraConnection.GetInstance().GetRecord<WineModel>(cql)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Get number of records in wine table
        /// </summary>
        /// <returns></returns>
        [HttpGet("number-of-records")]
        public async Task<int> GetNumberOfWines()
        {
            Cql cql = new($"select id from {_tableName}");
            return await CassandraConnection.GetInstance().GetNumberOfRecords<WineModel>(cql);
        }

        /// <summary>
        /// Get wines recommendation for user by selected items
        /// </summary>
        /// <param name="wines"></param>
        /// <returns></returns>
        [HttpGet("recommendation")]
        public async Task<IEnumerable<WineModel>> GetRecommendations([FromBody]SelectedWines wines)
        {
            var retrievedData = await Recommenation.GetSelectedData(wines.Wines);
            Recommenation.CalculateWineColor(retrievedData, out double red, out double white, out double pink);
            Recommenation.CalculateWineCountry( retrievedData, out string country );

            Cql redWineCql = GenerateCql("Red", country, (int) red);
            Cql whiteWineCql = GenerateCql("White", country, (int) white);
            Cql pinkWineCql = GenerateCql("Pink", country, (int) pink);

            var redWineResult = redWineCql is not null ? await GetWineData(redWineCql) : new List<WineModel>();
            var whiteWineResult = whiteWineCql is not null ? await GetWineData(whiteWineCql) : new List<WineModel>();
            var pinkWineResult = pinkWineCql is not null ? await GetWineData(pinkWineCql) : new List<WineModel>();
            
            redWineResult.AddRange(whiteWineResult);
            redWineResult.AddRange(pinkWineResult);

            return redWineResult;
        }

        private Cql GenerateCql(string color, string country, int limit = 0)
        {
            if (limit == 0)
                return null;
            
            StringBuilder builder = new ($"select * from {_tableName} where color=? and country=? ");
            builder.Append($"limit {limit} ");
            builder.Append("allow filtering");

            return new Cql(builder.ToString(), color, country);
        }

        private async Task<List<WineModel>> GetWineData(Cql cql)
        {
            return await CassandraConnection.GetInstance()
                .GetByRequestData<WineModel>(cql);
        }
    }

    /// <summary>
    /// Represent an object, that retrieve from request body
    /// </summary>
    public class SelectedWines
    {
        public int[] Wines { get; set; }
    }
}
