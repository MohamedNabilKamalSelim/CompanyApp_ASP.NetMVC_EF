using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyApp.Models
{
    [Table("Department", Schema = "HR")]
    public class Department
    {
        [Key]
        [Display(Name = "ID")]
        public int? DepartmentId { get; set; }

        [Required]
        [Display(Name = "Name")]
        [Column(TypeName = "varchar(150)")]
        public string DepartmentName { get; set; } = string.Empty;

        [Column(TypeName = "varchar(7)")]
        [Display(Name = "Department Abbreviation")]
        public string? DepartmentAbbreviation { get; set; }
    }
}
