namespace WorkFlowApp.Models
{
    public class RaporTalebi
    {
        public int Id { get; set; }
        public string Baslik { get; set; } = string.Empty;
        public string HedefRol { get; set; } = string.Empty;
        public DateTime SonTarih { get; set; }
        public string? Icerik { get; set; }
        public DateTime OlusturulmaTarihi { get; set; } = DateTime.Now;
        public string Durum { get; set; } = "Bekliyor";
        public string? TalepEden { get; set; }
    }
}