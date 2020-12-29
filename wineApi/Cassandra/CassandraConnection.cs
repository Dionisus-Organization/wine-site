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
    /// <summary>
    /// Class that hold connection with cassandra
    /// </summary>
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

        /// <summary>
        /// Returns cassandra`s object with initialized connection with cassandra.
        /// Represent a singleton pattern
        /// </summary>
        /// <returns>Instance of CassandraConnection</returns>
        public static CassandraConnection GetInstance()
        {
            return instance ?? (instance = new CassandraConnection());
        }

        /// <summary>
        /// Get all data from specified table
        /// </summary>
        /// <typeparam name="T">Return data type</typeparam>
        /// <param name="cql">Cql statement</param>
        /// <returns>List of records</returns>
        public async Task<List<T>> GetByRequestData<T>(Cql cql)
        {
            var result = await mapper.FetchAsync<T>(cql)
                .ConfigureAwait(false);
            return result.ToList();
        }

        /// <summary>
        /// Return a record from table
        /// </summary>
        /// <typeparam name="T">Type of model</typeparam>
        /// <param name="cql">CQL statement for request</param>
        /// <returns>A record of specified type</returns>
        public async Task<T> GetRecord<T> ( Cql cql )
        {
            return await mapper.SingleAsync<T>( cql )
                .ConfigureAwait( false );
        }

        /// <summary>
        /// Add record to DB
        /// </summary>
        /// <typeparam name="T">Type of model</typeparam>
        /// <param name="obj">Instance of model</param>
        public async Task AddRecord<T>(T obj)
        {
            await mapper.InsertAsync(obj)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Update record in table
        /// </summary>
        /// <param name="obj">Instance of model</param>
        public async Task UpdateRecord<T>(T obj)
        {
            await mapper.UpdateAsync(obj)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Delete record from table
        /// </summary>
        /// <param name="cql"></param>
        /// <typeparam name="T"></typeparam>
        public async Task DeleteRecord<T>(Cql cql)
        {
            await mapper.DeleteAsync<T>(cql);
        }

        /// <summary>
        /// Return a number of record in table
        /// </summary>
        /// <param name="cql"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<int> GetNumberOfRecords<T>(Cql cql)
        {
            var result = await mapper.FetchAsync<T>(cql);
            return result.Count();
        }
    }
}
