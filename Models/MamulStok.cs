using System;
using System.ComponentModel.DataAnnotations;

namespace WorkFlowApp.Models
{
    public class MamulStok
    {
        [Key]
        public int Id { get; set; }

        public string UrunAdi { get; set; } = string.Empty;
        public DateTime Tarih { get; set; } = DateTime.Today;
        public int Devir { get; set; } = 0;   
        public int Uretim { get; set; } = 0;  
        public int Sevk { get; set; } = 0;  
        public int Kalan { get; set; } = 0;   
    }
}