using System.ComponentModel.DataAnnotations;

namespace WorkFlowApp.Models
{
    public class Musteri
    {
        [Key]
        public int Id { get; set; }
        public string FirmaAdi { get; set; } = string.Empty;
        public string YetkiliKisi { get; set; } = string.Empty;
        public string Telefon { get; set; } = string.Empty;
    }
}