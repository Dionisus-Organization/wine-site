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

        public static async Task<IEnumerable<T>> GetAllData<T>(string tableName)
        {
            return await mapper.FetchAsync<T>($"SELECT * FROM {tableName}");
        }


    }
}
