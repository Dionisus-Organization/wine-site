using System.ComponentModel.DataAnnotations;

namespace wineApi.Models
{
    /// <summary>
    /// Класс, описывающий модель вина
    /// </summary>
    public class WineModel
    {
        [Key]
        public int Wine_Id { get; set; }
        public string Wine { get; set; }
        public string Wine_Slug { get; set; }
        public string Appellation { get; set; }
        public string Appellation_Slug { get; set; }
        public string Color { get; set; }
        public string Wine_Type { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public string Classification { get; set; }
        public string Vintage { get; set; }
        public string Date { get; set; }
        public bool Is_Primeurs { get; set; }
        public float Score { get; set; }
        public string Confidence_Index { get; set; }
        public int Journalist_Count { get; set; }
        public string Lwin { get; set; }
        public string Lwin_11 { get; set; }
    }
}
