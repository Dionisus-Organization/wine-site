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
    [Route("api/user")]
    [ApiController]
    public class UserController : Controller
    {
        const string tableName = "user";

        [HttpGet]
        public string GetAllUsers()
        {
            // return await CassandraConnection.GetAllData<UserModel>(tableName)
            //     .ConfigureAwait(false);
            return "Hello world";
        }

        [HttpGet("{id}")]
        public async Task<UserModel> GetUser(int id)
        {
            return await CassandraConnection.GetRecord<UserModel>(id, tableName)
                .ConfigureAwait(false);
        }

        [HttpPost("create")]
        public async Task AddUser(UserModel user)
        {
            await CassandraConnection.AddRecord(user)
                .ConfigureAwait(false);
        }

        [HttpPost("update/{id}")]
        public async Task UpdateUserInfo(UserModel user)
        {
            await CassandraConnection.UpdateRecord(user);
        }

        [HttpDelete("delete/{id}")]
        public async Task DeleteUser(int id)
        {

        }
    }
}
