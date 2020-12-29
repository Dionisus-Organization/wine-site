using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Cache;
using System.Threading.Tasks;
using System.Xml.Schema;
using Cassandra;
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
        private const string _tableName = "wine";
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
        /// 
        /// </summary>
        /// <param name="wines"></param>
        /// <returns></returns>
        [HttpGet("recommendation")]
        public async Task<IEnumerable<WineModel>> GetRecommendations([FromBody]SelectedWines wines)
        {
            var retrievedData = await Recommenation.GetSelectedData(wines.Wines);
            Recommenation.CalculateWineColor(retrievedData, out double red, out double white, out double pink);
            Recommenation.CalculateWineCountry( retrievedData, out string country );

            Cql redWineCql = new($"select * from {_tableName} where color='Red' and country=? limit ? allow filtering", country, (int)red);
            Cql whiteWineCql = new($"select * from {_tableName} where color='White' and country=? limit ? allow filtering", country, (int)white);
            Cql pinkWineCql = new($"select * from {_tableName} where color='Pink' and country=? limit ? allow filtering", country, (int)pink);

            var redWineResult = await CassandraConnection.GetInstance().GetByRequestData<WineModel>(redWineCql);
            var whiteWineResult = await CassandraConnection.GetInstance().GetByRequestData<WineModel>(whiteWineCql);
            var pinkWineResult = await CassandraConnection.GetInstance().GetByRequestData<WineModel>(pinkWineCql);
            
            redWineResult.AddRange(whiteWineResult);
            redWineResult.AddRange(pinkWineResult);

            return redWineResult;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class SelectedWines
    {
        public int[] Wines { get; set; }
    }
}
