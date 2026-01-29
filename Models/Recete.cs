using System.ComponentModel.DataAnnotations;

namespace WorkFlowApp.Models
{
    public class Recete
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Ürün Adı")]
        public string UrunAdi { get; set; } = string.Empty;
        [Display(Name = "Bileşen (Hammadde)")]
        public string BilesenAdi { get; set; } = string.Empty;
        [Display(Name = "Kullanım Miktarı")]
        public decimal KullanimMiktari { get; set; } 
        [Display(Name = "Birim")]
        public string Birim { get; set; } = "Kg";
    }
}
