using System.ComponentModel.DataAnnotations;

namespace WorkFlowApp.Models
{
    public class FiyatListesi
    {
        [Key]
        public int Id { get; set; }
        public int MusteriId { get; set; } // Hangi müşterinin listesi?
        public string DosyaYolu { get; set; } = string.Empty; // PDF/Excel dosyasının adı
        public string Aciklama { get; set; } = string.Empty; // "Ocak 2025 Fiyatları"
        public DateTime YuklemeTarihi { get; set; } = DateTime.Now;
    }
}