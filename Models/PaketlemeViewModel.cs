namespace WorkFlowApp.Models
{
    public class PaketlemeViewModel
    {
        // Sayfanın altındaki tablo için geçmiş üretimler
        public List<PaketlemeUretim> GecmisUretimler { get; set; } = new List<PaketlemeUretim>();

        // Dropdown (Seçim Kutusu) için Depodaki Ürün İsimleri
        public List<string> DepodakiUrunler { get; set; } = new List<string>();
    }
}