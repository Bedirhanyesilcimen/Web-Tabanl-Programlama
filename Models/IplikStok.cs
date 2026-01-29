using System;
using System.ComponentModel.DataAnnotations;

namespace WorkFlowApp.Models
{
    public class IplikStok
    {
        [Key]
        public int Id { get; set; }

        public string Firma { get; set; } = string.Empty;   
        public string UrunAdi { get; set; } = string.Empty; 
        public string LotNo { get; set; } = string.Empty;     
        public double GelenKg { get; set; } = 0;             
        public double KalanKg { get; set; } = 0;           
        public DateTime GelisTarihi { get; set; } = DateTime.Today;
    }
}
