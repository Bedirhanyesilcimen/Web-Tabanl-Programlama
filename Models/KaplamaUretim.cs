using System;
using System.ComponentModel.DataAnnotations;

namespace WorkFlowApp.Models
{
    public class KaplamaUretim
    {
        [Key]
        public int Id { get; set; }
        public DateTime Tarih { get; set; } = DateTime.Now;
        public string Aciklama { get; set; } = string.Empty;
        public string UrunAdi { get; set; } = string.Empty;
        public string Operator { get; set; } = string.Empty;
        public int Adet { get; set; } = 0;
        public string HammaddeTuru { get; set; } = string.Empty;
        public string UrunModeli { get; set; } = string.Empty;
        public int MiktarCift { get; set; } = 0;
        public string HatNo { get; set; } = string.Empty;
    }
}
