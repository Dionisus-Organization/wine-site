using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wineApi
{
    /// <summary>
    /// Запись, описывающая модель пользователя
    /// </summary>
    public record UserModel (int Id, string Name, string LastName);

    /// <summary>
    /// Запись, описывающая модель вина
    /// </summary>
    public record WineModel (
        int Id, 
        string Wine, 
        string WineSlug, 
        string Appellation,
        string AppellationSlug,
        string Color,
        string WineType,
        string Region,
        string Country,
        string Classification,
        string Vintage,
        string Date,
        bool IsPrimeurs,
        float Score,
        string ConfidenceIndex,
        int JournalistCount,
        string Lwin,
        string Lwin11
        );

    /// <summary>
    /// 
    /// </summary>
    public record CountryModel(int Id, string CountryName, IEnumerable<RegionModel> Regions);

    /// <summary>
    /// 
    /// </summary>
    public record RegionModel(int Id, string RegionName);
}
