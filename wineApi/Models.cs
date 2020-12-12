using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wineApi
{
    /// <summary>
    /// Запись, описывающая модель пользователя
    /// </summary>
    public record UserModel (int Id, string Name, string Last_name);

    /// <summary>
    /// Запись, описывающая модель вина
    /// </summary>
    public record WineModel (
        int Id, 
        string Wine, 
        string Wine_Slug, 
        string Appellation,
        string Appellation_Slug,
        string Color,
        string Wine_Type,
        string Region,
        string Country,
        string Classification,
        string Vintage,
        string Date,
        bool Is_Primeurs,
        float Score,
        string Confidence_Index,
        int Journalist_Count,
        string Lwin,
        string Lwin_11
        );

    /// <summary>
    /// 
    /// </summary>
    public record Country(int Id, string CountryName, IEnumerable<Region> Regions);

    /// <summary>
    /// 
    /// </summary>
    public record Region(int Id, string RegionName);
}
