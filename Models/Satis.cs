using System.ComponentModel.DataAnnotations;

namespace WorkFlowApp.Models
{
    public class Satis
    {
        [Key]
        public int Id { get; set; }
        public int SevkEdilenAdet { get; set; } = 0;
        public int KalanAdet => Adet - SevkEdilenAdet;

        public DateTime Tarih { get; set; } = DateTime.Now;

        [Display(Name = "Müşteri Ünvanı")]
        public string MusteriUnvani { get; set; } = string.Empty;

        [Display(Name = "Satılan Ürün")]
        public string UrunAdi { get; set; } = string.Empty;

        [Display(Name = "Adet (Koli)")]
        public int Adet { get; set; }

        [Display(Name = "Toplam Tutar")]
        public decimal ToplamTutar { get; set; } 
    }
}