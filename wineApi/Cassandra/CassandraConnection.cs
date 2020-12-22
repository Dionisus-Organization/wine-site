using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Cassandra;
using Cassandra.Mapping;

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
            //// Create a cluster instance
            //cluster = Cluster.Builder().AddContactPoint( Environment.GetEnvironmentVariable( "CASSANDRA_ADDRESS" ) ).Build();
            //MappingConfiguration.Global.Define<AllMappings>();

            ////Create connections to the nodes using a keyspace
            //session = cluster.Connect( Environment.GetEnvironmentVariable( "KEYSPACE_NAME" ) );

            //mapper = new Mapper( session );
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
        public async Task<IEnumerable<T>> GetAllData<T>(string tableName)
        {
            Cql cql = new($"SELECT * FROM {tableName}");
            IEnumerable<T> result = await mapper.FetchAsync<T>(cql)
                .ConfigureAwait(false);

            return result;
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
    }
}
