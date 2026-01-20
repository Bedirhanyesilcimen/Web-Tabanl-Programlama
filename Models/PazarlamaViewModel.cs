using System.Collections.Generic;

namespace WorkFlowApp.Models
{
    public class PazarlamaViewModel
    {
      public List<PazarlamaSiparis> Siparisler { get; set; } = new List<PazarlamaSiparis>();
    public decimal ToplamCiro { get; set; }
    }
}