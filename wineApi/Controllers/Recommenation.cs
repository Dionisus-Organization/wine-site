using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cassandra.Mapping;
using wineApi.Cassandra;

namespace wineApi.Controllers
{
    public static class Recommenation
    {
        /// <summary>
        /// Retrieve selected wines from DB by their IDs
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public static async Task<List<WineModel>> GetSelectedData(int[] ids)
        {
            List<WineModel> selectedWines = new List<WineModel>();
            
            foreach (int id in ids)
            {
                Cql cql = new("WHERE id=? allow filtering", id);
                selectedWines.Add(await CassandraConnection.GetInstance().GetRecord<WineModel>(cql));
            }

            return selectedWines;
        }

        /// <summary>
        /// Calculating of wines color proportions
        /// </summary>
        /// <param name="wines">Array of selected wines</param>
        /// <param name="redProportion">Out parameter of red wine</param>
        /// <param name="whiteProportion">Out parameter of white wine</param>
        /// <param name="pinkProportion">Out parameter of poink wine</param>
        public static void CalculateWineColor(
            List<WineModel> wines, 
            out double redProportion,
            out double whiteProportion, 
            out double pinkProportion)
        {
            var redWines = 0.0;
            var whiteWines = 0.0;
            var pinkWines = 0.0;
            
            foreach (var wine in wines)
            {
                switch (wine.Color)
                {
                    case "Red":
                        redWines++;
                        break;
                    case "White":
                        whiteWines++;
                        break;
                    case "Pink":
                        pinkWines++;
                        break;
                }
            }

            double sum = redWines + whiteWines + pinkWines;

            redProportion = Math.Round(redWines / sum * 10);
            whiteProportion =  Math.Round(whiteWines / sum * 10);
            pinkProportion = Math.Round(pinkWines / sum * 10);
        }

        /// <summary>
        /// Calculating of most selected country
        /// </summary>
        /// <param name="wines"></param>
        /// <param name="countryProportion"></param>
        public static void CalculateWineCountry(
            List<WineModel> wines,
            out string selectedCountry)
        {
            var counter = new Dictionary<string, int>();

            foreach ( var wine in wines )
            {
                if (counter.ContainsKey(wine.Country) )
                    counter[ wine.Country ]++;
                else
                    counter.Add( wine.Country, 1 );
            }

            int i = 0;
            foreach ( var country in counter )
                i = country.Value > i ? country.Value : i;

            selectedCountry = counter.FirstOrDefault( x => x.Value == i ).Key;
        }
    }
}