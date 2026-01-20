using System.ComponentModel.DataAnnotations;

namespace WorkFlowApp.Models
{
    public class KaplamaVeri
    {
        [Key]
        public int Id { get; set; }
        public DateTime Tarih { get; set; } = DateTime.Now;
        public string Makine { get; set; } = string.Empty;
        public string Vardiya { get; set; } = string.Empty; 
        public string UrunGrubu { get; set; } = string.Empty;
        public double Miktar { get; set; }
        public string Personel { get; set; } = string.Empty; 
    }
}