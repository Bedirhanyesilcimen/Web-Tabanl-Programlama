using System.ComponentModel.DataAnnotations;

namespace WorkFlowApp.Models
{
    public class Makine
    {
        [Key]
        public int Id { get; set; }
        public string MakineKodu { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string Durum { get; set; } = "AKTİF"; 
    }
}