using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using wineApi.Cassandra;

namespace wineApi.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : Controller
    {
        [HttpGet]
        public async Task<IEnumerable<UserModel>> GetAllUsers()
        {
            return await CassandraConnection.GetAllData<UserModel>("user");
        }
    }
}
