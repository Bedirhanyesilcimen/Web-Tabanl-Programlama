using System.ComponentModel.DataAnnotations;

namespace WorkFlowApp.Models
{
    public class IzinHareket
    {
        [Key]
        public int Id { get; set; }

        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }

        [Display(Name = "Başlangıç")]
        public DateTime Baslangic { get; set; }

        [Display(Name = "Bitiş")]
        public DateTime Bitis { get; set; }

        [Display(Name = "Gün Sayısı")]
        public int GunSayisi { get; set; }

        [Display(Name = "Açıklama")]
        public string Aciklama { get; set; } = string.Empty;
    }
}