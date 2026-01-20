using System;
using System.ComponentModel.DataAnnotations;

namespace WorkFlowApp.Models
{
    public class StokLog
    {
        [Key]
        public int Id { get; set; }
        public string UrunAdi { get; set; } = string.Empty;
        public string IslemTuru { get; set; } = string.Empty; // Giris veya Cikis
        public int Miktar { get; set; } = 0;
        public string Aciklama { get; set; } = string.Empty;
        public DateTime Tarih { get; set; } = DateTime.Now;
        public string IslemiYapan { get; set; } = string.Empty;
    }
}