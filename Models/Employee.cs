using System.ComponentModel.DataAnnotations;

namespace WorkFlowApp.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Ad Soyad")]
        public string FullName { get; set; } = string.Empty;

        [Display(Name = "Departman")]
        public int? DepartmentId { get; set; }
        public Department? Department { get; set; }

        [Display(Name = "Telefon")]
        public string Phone { get; set; } = string.Empty;

        [Display(Name = "Maaş")]
        public decimal Salary { get; set; }

    
        
        [Display(Name = "İşe Giriş Tarihi")]
        public DateTime IseGirisTarihi { get; set; } = DateTime.Now;

        [Display(Name = "Yıllık İzin Bakiyesi")]
        public int IzinBakiyesi { get; set; } = 14; 
        [Display(Name = "Durum")]
        public string CalismaDurumu { get; set; } = "Çalışıyor"; 
    }
}