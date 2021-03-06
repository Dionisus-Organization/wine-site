﻿using System;
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
        public async Task GetAllUsers ()
        {
            //return await CassandraConnection.GetInstance().GetAllData<UserModel>( tableName )
            //    .ConfigureAwait( false );

        }

        [HttpGet("{id}")]
        public async Task<UserModel> GetUser ( int id )
        {
            Cql cql = new( $"SELECT * FROM {tableName} WHERE userid=?", id );
            return await CassandraConnection.GetInstance().GetRecord<UserModel>( cql )
                .ConfigureAwait( false );
        }

        [HttpPost( "create" )]
        public async Task AddUser ( UserModel user )
        {
            await CassandraConnection.GetInstance().AddRecord( user )
                .ConfigureAwait( false );
        }

        [HttpPost( "update/{id}" )]
        public async Task UpdateUserInfo ( int id, string name, string email )
        {
            Cql cql = new( "WHERE userid=?", id );

            var user = await CassandraConnection.GetInstance().GetRecord<UserModel>( cql )
                .ConfigureAwait( false );

            UserModel newUser = new UserModel {
                Email = email,
                Name = name,
                UserId = user.UserId,
                Password = user.Password
            };

            await CassandraConnection.GetInstance().UpdateRecord( newUser )
                .ConfigureAwait( false );
        }

        [HttpDelete( "delete/{id}" )]
        public async Task DeleteUser ( int id )
        {
            Cql cql = new( "WHERE userid=?", id );
            await CassandraConnection.GetInstance().DeleteRecord<UserModel>( cql )
                .ConfigureAwait( false );
        }
    }
}
