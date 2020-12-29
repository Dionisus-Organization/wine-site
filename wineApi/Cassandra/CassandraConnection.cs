using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.XPath;
using Cassandra;
using Cassandra.Mapping;
using Microsoft.AspNetCore.Http;
using Microsoft.CSharp.RuntimeBinder;
using ISession = Cassandra.ISession;

namespace wineApi.Cassandra
{
    public class CassandraConnection
    {
        private static CassandraConnection instance = null;
        private static Cluster cluster;
        private static ISession session;
        private static Mapper mapper;

        static CassandraConnection() 
        {
            InitConnection();
        }

        private static void InitConnection()
        {
            // Create a cluster instance
            cluster = Cluster.Builder().AddContactPoint( Environment.GetEnvironmentVariable( "CASSANDRA_ADDRESS" ) ).Build();

            //Create connections to the nodes using a keyspace
            session = cluster.Connect( Environment.GetEnvironmentVariable( "KEYSPACE_NAME" ) );

            MappingConfiguration.Global.Define<AllMappings>();

            mapper = new Mapper( session );
        }

        public static CassandraConnection GetInstance()
        {
            if ( instance == null )
                instance = new CassandraConnection();
            return instance;
        }

        /// <summary>
        /// Get all data from specified table
        /// </summary>
        /// <typeparam name="T">Return data type</typeparam>
        /// <param name="tableName">Name of table</param>
        /// <returns></returns>
        public async Task<List<T>> GetByRequestData<T>(Cql cql)
        {
            var result = await mapper.FetchAsync<T>(cql)
                .ConfigureAwait(false);
            return result.ToList();
        }

        /// <summary>
        /// Get record from DB by cql
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public async Task<T> GetRecord<T> ( Cql cql )
        {
            return await mapper.SingleAsync<T>( cql )
                .ConfigureAwait( false );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task AddRecord<T>(T obj)
        {
            await mapper.InsertAsync(obj)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// /
        /// </summary>
        /// <param name="cql"></param>
        /// <returns></returns>
        public async Task UpdateRecord<T>(T obj)
        {
            await mapper.UpdateAsync(obj)
                .ConfigureAwait(false);
        }

        public async Task DeleteRecord<T>(Cql cql)
        {
            await mapper.DeleteAsync<T>(cql);
        }

        public async Task<int> GetNumberOfRecords<T>(Cql cql)
        {
            var result = await mapper.FetchAsync<T>(cql);
            return result.Count();
        }
    }
}
