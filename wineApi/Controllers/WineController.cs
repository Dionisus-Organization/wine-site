using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Cache;
using System.Threading.Tasks;
using Cassandra;
using Cassandra.Mapping;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
        public async Task<IEnumerable<WineModel>> GetWines([FromQuery]int page)
        {
            int startIndex = _pageSize * (page - 1);

            Cql cql = new( $"SELECT * FROM {_tableName} where id >= ? and id < ? allow filtering", startIndex, startIndex + _pageSize );

            var temp = await CassandraConnection.GetInstance().GetPagedData<WineModel>( cql );
            List<WineModel> result = temp.ToList().OrderBy( wine => wine.Id ).ToList();

            return result;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WineModel>> GetWine(int id)
        {
            Cql cql = new($"WHERE wine_id=?", id);
            return await CassandraConnection.GetInstance().GetRecord<WineModel>(cql)
                .ConfigureAwait(false);
        }
    }

    public static class ListExtansion
    {
        public static void CopyToList<T>(this List<T> source, List<T> destination, int startIndex, int count)
        {
            for ( int i = startIndex; i < count; i++ )
                destination.Add( source[ i ] );
        }
    }
}
