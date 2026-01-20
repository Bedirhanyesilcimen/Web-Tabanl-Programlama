namespace WorkFlowApp.Models
{
    public class TekstilViewModel
    {
        public List<Makine> Makineler { get; set; } = new List<Makine>();
        public List<TekstilUretim> Uretimler { get; set; } = new List<TekstilUretim>();
    }
}