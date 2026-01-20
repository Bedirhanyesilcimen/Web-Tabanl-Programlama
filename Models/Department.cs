using System.ComponentModel.DataAnnotations;

namespace WorkFlowApp.Models
{
    public class Department
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Departman adı zorunludur.")]
        [Display(Name = "Departman Adı")]
        public string Name { get; set; } = string.Empty; // Sarı uyarı gitmesi için boş değer atadık

        // İlişki
        public ICollection<Employee>? Employees { get; set; } // ? koyduk, liste boş olabilir dedik
    }
}