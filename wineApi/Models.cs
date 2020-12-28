using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wineApi
{
    /// <summary>
    /// Запись, описывающая модель пользователя
    /// </summary>
    public class UserModel
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Password { get; set; }
    }

    /// <summary>
    /// Запись, описывающая модель вина
    /// </summary>
    public class WineModel
    {
        public int Id { get; set; }
        public int Wine_Id { get; set; }
        public string Wine { get; set; }
        public string WineSlug { get; set; }
        public string Appellation { get; set; }
        public string AppellationSlug { get; set; }
        public string Color { get; set; }
        public string WineType { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public string Classification { get; set; }
        public string Vintage { get; set; }
        public string Date { get; set; }
        public bool Is_Primeurs { get; set; }
        public float Score { get; set; }
        public string ConfidenceIndex { get; set; }
        public int JournalistCount { get; set; }
        public string Lwin { get; set; }
        public string Lwin11 { get; set; }
    }
}
