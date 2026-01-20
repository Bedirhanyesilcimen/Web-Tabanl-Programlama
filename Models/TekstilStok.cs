using System;
using System.ComponentModel.DataAnnotations;

namespace WorkFlowApp.Models
{
    public class TekstilStok
    {
        [Key]
        public int Id { get; set; }

        public string Cinsi { get; set; } = string.Empty; 
        
        // YENİ: Çuval Bilgileri
        public int CuvalSayisi { get; set; } = 0;  // Örn: 10 Çuval
        public int CuvalIciAdet { get; set; } = 0; // Örn: Her çuvalda 500 çift var

        public double Miktar { get; set; } = 0;    // Toplam (Otomatik hesaplanacak)
        public string Birim { get; set; } = "Adet"; 
        public string Tur { get; set; } = string.Empty; 
        public string Kategori { get; set; } = string.Empty;
        public string AltKategori { get; set; } = string.Empty;

        public DateTime GirisTarihi { get; set; } = DateTime.Now;
    }
}