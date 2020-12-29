using System.Text;
using Cassandra.Mapping;

namespace wineApi.Controllers
{
    public static class WineControllerHelper
    {
        public static Cql GenerateCql(string tableName, string color, string country, int limit = 0)
        {
            if (limit == 0)
                return null;
            
            StringBuilder builder = new ($"select * from {tableName} where color=? and country=? ");
            builder.Append($"limit {limit} ");
            builder.Append("allow filtering");

            return new Cql(builder.ToString(), color, country);
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
    }
}