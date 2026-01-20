using WorkFlowApp.Models;

namespace WorkFlowApp.ViewModels // ViewModels klasörü yoksa Models içine de koyabilirsin, sorun değil.
{
    public class HomeViewModel
    {
        // 1. ÜSTTEKİ KUTUCUKLAR (SAYAÇLAR)
        public int BugunUretilenKoli { get; set; }
        public int ToplamDepoStogu { get; set; }
        public int BekleyenSiparis { get; set; } // Şimdilik 0 gelecek, ileride bağlarız
        public int PersonelSayisi { get; set; }

        // 2. KRİTİK STOK LİSTESİ (Tablo)
        public List<MamulStok> KritikStoklar { get; set; } = new List<MamulStok>();

        // 3. GRAFİK VERİLERİ (Son 7 Gün)
        public List<string> GrafikTarihler { get; set; } = new List<string>();
        public List<int> GrafikVeriler { get; set; } = new List<int>();
    }
}