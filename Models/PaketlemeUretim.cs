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
        public int KoliAdedi { get; set; } = 0;
        public int KoliIciAdet { get; set; } = 0;
        public string Operator { get; set; } = string.Empty; 
        public int Adet { get; set; } = 0;
        public string Personel { get; set; } = string.Empty;
        public string Aciklama { get; set; } = string.Empty;
    }
}
