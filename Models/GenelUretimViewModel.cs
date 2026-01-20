namespace WorkFlowApp.Models
{
    public class GenelUretimViewModel
    {
        // System.Collections.Generic... diyerek tam adres veriyoruz ki hata şansı kalmasın
        public System.Collections.Generic.List<KaplamaUretim> KaplamaListesi { get; set; } = new System.Collections.Generic.List<KaplamaUretim>();
        public System.Collections.Generic.List<TekstilUretim> TekstilListesi { get; set; } = new System.Collections.Generic.List<TekstilUretim>();
    }
}