using System.Collections.Generic;

namespace WorkFlowApp.Models
{
    public class DepoViewModel
    {
        // 1. Bitmiş Ürünler (Eldiven)
        public List<MamulStok> MamulStoklari { get; set; } = new List<MamulStok>();

        // 2. Hammaddeler (İplik - Lotlu)
        public List<HammaddeStok> HammaddeStoklari { get; set; } = new List<HammaddeStok>();

        // 3. Tekstil (Astar - Yarı Mamul) -- YENİ EKLENEN
        public List<TekstilStok> TekstilStoklari { get; set; } = new List<TekstilStok>();
        
        // Loglar
        public List<StokLog> SonHareketler { get; set; } = new List<StokLog>();
    }
}