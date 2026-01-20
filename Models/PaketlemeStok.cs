using System;
using System.ComponentModel.DataAnnotations;

namespace WorkFlowApp.Models
{
    public class PaketlemeStok
    {
        [Key]
        public int Id { get; set; }

        public string UrunAdi { get; set; } = string.Empty;
        public string Tur { get; set; } = "Pamuklu"; 

        public DateTime Tarih { get; set; } = DateTime.Today;

        public int Devir { get; set; } = 0; 
        public int Giris { get; set; } = 0; 
        public int Iade { get; set; } = 0;  
        public int Cikis { get; set; } = 0; 
        public string? DosyaYolu { get; set; }
        
        public int Kalan { get; set; } = 0; 
    }
}