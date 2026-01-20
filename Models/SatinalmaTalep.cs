using System.ComponentModel.DataAnnotations;

namespace WorkFlowApp.Models
{
    public class SatinalmaTalep
    {
        [Key]
        public int Id { get; set; }
        
        public string UrunAdi { get; set; } = string.Empty;
        public int Miktar { get; set; }
        public string Birim { get; set; } = string.Empty;
        public string TalepEden { get; set; } = string.Empty;
        
        public DateTime TalepTarihi { get; set; } = DateTime.Now;
        public DateTime Tarih { get; set; } = DateTime.Now; 

        public string Durum { get; set; } = "Genel Müdür Onayında"; 
        
        public string Aciklama { get; set; } = "-"; 
    }
}