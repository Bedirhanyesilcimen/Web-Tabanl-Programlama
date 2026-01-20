using System;
using System.ComponentModel.DataAnnotations;

namespace WorkFlowApp.Models
{
    public class TekstilUretim
    {
        [Key]
        public int Id { get; set; }
        public DateTime Tarih { get; set; } = DateTime.Now;
        public string Aciklama { get; set; } = string.Empty;
        public string MakineAdi { get; set; } = string.Empty;
        public string Personel { get; set; } = string.Empty; 
        public int Metraj { get; set; } = 0;
        public string MakineNo { get; set; } = string.Empty;
        public string Vardiya { get; set; } = string.Empty;
        public string UrunModeli { get; set; } = string.Empty;
        public int UretilenCift { get; set; } = 0;
        public int FireAdet { get; set; } = 0;
        public int Miktar { get; set; } 
        public string Operator { get; set; } = string.Empty;
    }
}