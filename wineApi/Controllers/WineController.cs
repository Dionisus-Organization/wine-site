using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Cassandra.Mapping;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using wineApi.Cassandra;

namespace wineApi.Controllers
{
    [Route("wine")]
    [ApiController]
    public class WineController : ControllerBase
    {
        const string tableName = "wine";

        [HttpGet]
        public async Task<IEnumerable<WineModel>> GetWines()
        {
            return await CassandraConnection.GetInstance().GetAllData<WineModel>( tableName )
                .ConfigureAwait(false);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WineModel>> GetWine( int id )
        {
            Cql cql = new( $"WHERE wine_id=?", id );
            return await CassandraConnection.GetInstance().GetRecord<WineModel>( cql )
                .ConfigureAwait( false );
        }
    }
}
