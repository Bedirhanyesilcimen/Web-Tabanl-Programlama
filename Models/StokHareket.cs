using System.ComponentModel.DataAnnotations;

namespace WorkFlowApp.Models
{
    public class StokHareket
    {
        [Key]
        public int Id { get; set; }
        public DateTime Tarih { get; set; } = DateTime.Now;
        public string UrunAdi { get; set; } = string.Empty;
        public int Miktar { get; set; } = 0;
        public string IslemTuru { get; set; } = "İşlem Yok"; 
        public string Kullanici { get; set; } = "Sistem";
    }
}
