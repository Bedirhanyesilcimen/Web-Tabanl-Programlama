using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkFlowApp.Models
{
    public class TaskItem
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Görev Başlığı")]
        public string Title { get; set; } = string.Empty;
        [Display(Name = "Açıklama")]
        public string? Description { get; set; }
        [Display(Name = "Durum")]
        public bool IsCompleted { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Son Tarih")]
        public DateTime Deadline { get; set; }
        public int? EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public Employee? Employee { get; set; }
    }
}
