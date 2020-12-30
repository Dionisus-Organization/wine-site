using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Cassandra.Mapping;
using wineApi.Cassandra;

namespace wineApi.Controllers
{
    public static class WineControllerHelper
    {
        public static Cql GenerateCql(string tableName, string color, string country)
        {
            StringBuilder builder = new ($"select * from {tableName} where color=? and country=? ");
            builder.Append("allow filtering");

            return new Cql(builder.ToString(), color, country);
        }

        private static List<WineModel> CopyElements(this List<WineModel> destination, List<WineModel> source, int number)
        {
            var gordon = new Random();
            
            for (var i = 0; i < number; i++)
            {
                var randIndex = gordon.Next(source.Count);
                destination.Add(source[randIndex]);
            }

            return destination;
        }

        /// <summary>
        /// Build Cql string for request
        /// </summary>
        /// <param name="tableName">Name of Table (wine)</param>
        /// <param name="color">Color of wine</param>
        /// <param name="wine_type">Type of wine</param>
        /// <param name="country">Wine's country</param>
        /// <param name="vintage">Wine's vintage</param>
        /// <returns>String for Cql</returns>
        public static string GenerateFilterCql(string tableName, string color, string wine_type, string country,
            string vintage)
        {
            StringBuilder builder = new($"select * from {tableName} where ");

            if (!string.IsNullOrEmpty(color))
                builder.Append($"{IsContainParams(builder.ToString())}color='{color}' ");

            if (!string.IsNullOrEmpty(wine_type))
                builder.Append($"{IsContainParams(builder.ToString())}wine_type='{wine_type}' ");

            if (!string.IsNullOrEmpty(country))
                builder.Append($"{IsContainParams(builder.ToString())}country='{country}' ");

            if (!string.IsNullOrEmpty(vintage))
                builder.Append($"{IsContainParams(builder.ToString())}vintage='{vintage}' ");

            builder.Append("allow filtering");

            return builder.ToString();
        }

        /// <summary>
        /// Check, does string contain params
        /// and returns 'and' word for string request if it needed 
        /// </summary>
        private static string IsContainParams(string cql)
        {
            return cql.Contains('=') ? "and " : string.Empty;
        }
        
        /// <summary>
        /// Retrieve data for recommendation
        /// </summary>
        /// <param name="cql">Cql request</param>
        /// <returns>List of wines</returns>
        public static async Task<List<WineModel>> GetWineData(Cql cql, int limit)
        {
            var result = await CassandraConnection.GetInstance()
                .GetByRequestData<WineModel>(cql);

            return new List<WineModel>().CopyElements(result, limit);
        }
    }
}