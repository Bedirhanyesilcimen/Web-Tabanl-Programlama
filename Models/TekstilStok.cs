using System;
using System.ComponentModel.DataAnnotations;

namespace WorkFlowApp.Models
{
    public class TekstilStok
    {
        [Key]
        public int Id { get; set; }
        public string Cinsi { get; set; } = string.Empty; 
        public int CuvalSayisi { get; set; } = 0; 
        public int CuvalIciAdet { get; set; } = 0; 
        public double Miktar { get; set; } = 0;    
        public string Birim { get; set; } = "Adet"; 
        public string Tur { get; set; } = string.Empty; 
        public string Kategori { get; set; } = string.Empty;
        public string AltKategori { get; set; } = string.Empty;
        public DateTime GirisTarihi { get; set; } = DateTime.Now;
    }
}
