using System;
using System.ComponentModel.DataAnnotations;

namespace WorkFlowApp.Models
{
    public class HammaddeStok
    {
        [Key]
        public int Id { get; set; }

        public string HammaddeAdi { get; set; } = string.Empty; // Örn: Polyester İplik
        public string LotNo { get; set; } = string.Empty;       // Lot Takibi
        public string Tedarikci { get; set; } = string.Empty;   
        
        public double MiktarKg { get; set; } = 0;               
        public DateTime GirisTarihi { get; set; } = DateTime.Now;
    }
}