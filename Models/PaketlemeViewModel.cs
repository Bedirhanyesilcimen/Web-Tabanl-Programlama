namespace WorkFlowApp.Models
{
    public class PaketlemeViewModel
    {
        public List<PaketlemeUretim> GecmisUretimler { get; set; } = new List<PaketlemeUretim>();
        public List<string> DepodakiUrunler { get; set; } = new List<string>();
    }
}
