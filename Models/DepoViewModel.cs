using System.Collections.Generic;

namespace WorkFlowApp.Models
{
    public class DepoViewModel
    {
        public List<MamulStok> MamulStoklari { get; set; } = new List<MamulStok>();
        public List<HammaddeStok> HammaddeStoklari { get; set; } = new List<HammaddeStok>();
        public List<TekstilStok> TekstilStoklari { get; set; } = new List<TekstilStok>();
        public List<StokLog> SonHareketler { get; set; } = new List<StokLog>();
    }
}
