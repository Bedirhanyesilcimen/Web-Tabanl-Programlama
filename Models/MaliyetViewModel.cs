namespace WorkFlowApp.Models
{
    public class MaliyetViewModel
    {
        public List<HammaddeFiyat> Fiyatlar { get; set; } = new List<HammaddeFiyat>();
        public List<Recete> Receteler { get; set; } = new List<Recete>();
    }
}