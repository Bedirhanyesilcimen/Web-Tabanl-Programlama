using System.ComponentModel.DataAnnotations;

namespace WorkFlowApp.Models
{
    public class Recete
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Ürün Adı")]
        public string UrunAdi { get; set; } = string.Empty; // Hangi ürünün reçetesi?

        [Display(Name = "Bileşen (Hammadde)")]
        public string BilesenAdi { get; set; } = string.Empty;

        [Display(Name = "Kullanım Miktarı")]
        public decimal KullanimMiktari { get; set; } // Örn: 0.05

        [Display(Name = "Birim")]
        public string Birim { get; set; } = "Kg";
    }
}