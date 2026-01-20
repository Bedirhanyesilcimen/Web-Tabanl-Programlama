using System;
using System.ComponentModel.DataAnnotations;

namespace WorkFlowApp.Models
{
    public class PaketlemeUretim
    {
        [Key]
        public int Id { get; set; }

        public DateTime Tarih { get; set; } = DateTime.Now;

        public string UrunAdi { get; set; } = string.Empty;

        // --- PAKETLEME SAYFASININ İSTEDİKLERİ (Hata verenler) ---
        public int KoliAdedi { get; set; } = 0;
        public int KoliIciAdet { get; set; } = 0;
        public string Operator { get; set; } = string.Empty; // Hatada 'Operator' yok diyordu

        // --- ANA SAYFANIN İSTEDİĞİ ---
        // (Bunu da ekliyoruz ki HomeController hata vermesin)
        public int Adet { get; set; } = 0;

        // Yedek olarak kalsın (Bazı yerlerde Personel geçiyor olabilir)
        public string Personel { get; set; } = string.Empty;

        public string Aciklama { get; set; } = string.Empty;
    }
}