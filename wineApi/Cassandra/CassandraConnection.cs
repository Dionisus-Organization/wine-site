using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Cassandra;

namespace wineApi.Cassandra
{
    public class CassandraConnection
    {
        private static CassandraConnection instance = null;
        private static Cluster cluster;
        private static ISession session;

        static CassandraConnection() 
        {
            // Create a cluster instance
            cluster = Cluster.Builder().AddContactPoint("").Build();
            //Create connections to the nodes using a keyspace
            session = cluster.Connect("");
        }

        public static CassandraConnection GetInstance()
        {
            if (instance == null)
                instance = new CassandraConnection();

            return instance;
        }
    }
}
