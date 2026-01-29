using System.ComponentModel.DataAnnotations;

namespace WorkFlowApp.Models
{
    public class FiyatListesi
    {
        [Key]
        public int Id { get; set; }
        public int MusteriId { get; set; } 
        public string DosyaYolu { get; set; } = string.Empty; 
        public string Aciklama { get; set; } = string.Empty; 
        public DateTime YuklemeTarihi { get; set; } = DateTime.Now;
    }
}
