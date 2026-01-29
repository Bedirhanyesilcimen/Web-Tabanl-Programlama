using System.Collections.Generic;

namespace WorkFlowApp.Models
{
    public class DashboardViewModel
    {
        public string? KullaniciRolu { get; set; }
        public int ToplamPersonel { get; set; }
        public int ToplamUrunCesidi { get; set; }
        public int KritikStokSayisi { get; set; } 
        public int GelecekSevkiyatlar { get; set; } 
        public int BekleyenTalepler { get; set; } 
        public int AktifSiparisler { get; set; } 
        public int UretimdekiIsler { get; set; }
    }
}
