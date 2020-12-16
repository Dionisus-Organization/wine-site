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
            // Create a cluster instance
            cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();
            //Create connections to the nodes using a keyspace
            session = cluster.Connect("test_keyspace");
            mapper = new Mapper(session);
        }

        public static CassandraConnection GetInstance()
        {
            return instance ??= new CassandraConnection();
        }

        /// <summary>
        /// Get all data from specified table
        /// </summary>
        /// <typeparam name="T">Return data type</typeparam>
        /// <param name="tableName">Name of table</param>
        /// <returns></returns>
        public static async Task<IEnumerable<T>> GetAllData<T>(string tableName)
        {
            Cql cql = new($"SELECT * FROM {tableName}");
            return await mapper.FetchAsync<T>(cql)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static async Task<T> GetRecord<T>(int id, string tableName)
        {
            Cql cql = new($"SELECT * FROM {tableName} WHERE id=?", id);
            return await mapper.SingleAsync<T>(cql)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static async Task AddRecord<T>(T obj)
        {
            await mapper.InsertAsync(obj)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// /
        /// </summary>
        /// <param name="cql"></param>
        /// <returns></returns>
        public static async Task UpdateRecord<T>(T obj)
        {
            await mapper.UpdateAsync(obj).ConfigureAwait(false);
        }

        public static async Task DeleteRecord(int id, string tableName)
        {
            await mapper.DeleteAsync($"DE");
        }
    }
}
