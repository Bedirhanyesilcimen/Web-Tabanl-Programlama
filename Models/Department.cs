using System.ComponentModel.DataAnnotations;

namespace WorkFlowApp.Models
{
    public class Department
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Departman adı zorunludur.")]
        [Display(Name = "Departman Adı")]
        public string Name { get; set; } = string.Empty; 
        public ICollection<Employee>? Employees { get; set; }
    }
}
