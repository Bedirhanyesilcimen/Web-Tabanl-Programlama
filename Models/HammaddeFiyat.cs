using System.ComponentModel.DataAnnotations;

namespace WorkFlowApp.Models
{
    public class HammaddeFiyat
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Hammadde Adı")]
        public string HammaddeAdi { get; set; } = string.Empty;

        [Display(Name = "Birim Fiyat")]
        public decimal BirimFiyat { get; set; }

        [Display(Name = "Para Birimi")]
        public string ParaBirimi { get; set; } = "USD";
        
        public DateTime GuncellemeTarihi { get; set; } = DateTime.Now;
    }
}
