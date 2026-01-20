using System.ComponentModel.DataAnnotations;

namespace WorkFlowApp.Models
{
    public class PazarlamaSiparis
    {
        [Key]
        public int Id { get; set; }
        public string MusteriAdi { get; set; } = string.Empty;
        public string UrunAdi { get; set; } = string.Empty;
        public int Adet { get; set; } 
        public decimal BirimFiyat { get; set; }
        public bool Tamamlandi { get; set; } = false;

        public int SiparisMiktari { get; set; }
        public int GonderilenMiktar { get; set; } = 0;
        public int KalanMiktar { get; set; }
        public DateTime SiparisTarihi { get; set; } = DateTime.Now;
    }
}