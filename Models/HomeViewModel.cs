using WorkFlowApp.Models;

namespace WorkFlowApp.ViewModels 
{
    public class HomeViewModel
    {
        public int BugunUretilenKoli { get; set; }
        public int ToplamDepoStogu { get; set; }
        public int BekleyenSiparis { get; set; } 
        public int PersonelSayisi { get; set; }
        public List<MamulStok> KritikStoklar { get; set; } = new List<MamulStok>();
        public List<string> GrafikTarihler { get; set; } = new List<string>();
        public List<int> GrafikVeriler { get; set; } = new List<int>();
    }
}
