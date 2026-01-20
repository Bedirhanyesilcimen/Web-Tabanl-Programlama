namespace WorkFlowApp.Models
{
    public class IKViewModel
    {
        public List<Employee> Personeller { get; set; } = new List<Employee>();
        public List<IzinHareket> IzinGecmisi { get; set; } = new List<IzinHareket>();
    }
}