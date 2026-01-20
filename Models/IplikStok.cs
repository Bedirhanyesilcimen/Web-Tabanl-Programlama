using System;
using System.ComponentModel.DataAnnotations;

namespace WorkFlowApp.Models
{
    public class IplikStok
    {
        [Key]
        public int Id { get; set; }

        public string Firma { get; set; } = string.Empty;     // Hangi firmadan geldi?
        public string UrunAdi { get; set; } = string.Empty;   // İpliğin cinsi (Pamuk 20/1 vb.)
        public string LotNo { get; set; } = string.Empty;     // LOT numarası (Takip için kritik)
        
        public double GelenKg { get; set; } = 0;              // İlk gelen miktar
        public double KalanKg { get; set; } = 0;              // Harcandıkça düşecek
        
        public DateTime GelisTarihi { get; set; } = DateTime.Today;
    }
}