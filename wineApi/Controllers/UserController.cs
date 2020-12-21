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
    [Route( "user" )]
    [ApiController]
    public class UserController : Controller
    {
        const string tableName = "user";

        [HttpGet]
        public async Task<IEnumerable<UserModel>> GetAllUsers ()
        {
            return await CassandraConnection.GetAllData<UserModel>( tableName )
                .ConfigureAwait( false );
        }

        [HttpGet("{id}")]
        public async Task<UserModel> GetUser ( int id )
        {
            Cql cql = new( $"SELECT * FROM user WHERE userid=?", id );
            return await CassandraConnection.GetRecord<UserModel>( cql )
                .ConfigureAwait( false );
        }

        [HttpPost( "create" )]
        public async Task AddUser ( UserModel user )
        {
            await CassandraConnection.AddRecord( user )
                .ConfigureAwait( false );
        }

        [HttpPost( "update/{id}" )]
        public async Task UpdateUserInfo ( int id, string name, string email )
        {
            Cql cql = new( "WHERE userid=?", id );

            var user = await CassandraConnection.GetRecord<UserModel>( cql )
                .ConfigureAwait( false );

            UserModel newUser = user with { Name = name, Email = email };

            await CassandraConnection.UpdateRecord( newUser )
                .ConfigureAwait( false );
        }

        [HttpDelete( "delete/{id}" )]
        public async Task DeleteUser ( int id )
        {
            Cql cql = new( "WHERE userid=?", id );
            await CassandraConnection.DeleteRecord<UserModel>( cql )
                .ConfigureAwait( false );
        }
    }
}
