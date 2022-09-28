using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyApp.Models
{
    [Table("Employees", Schema = "HR")]
    public class Employee
    {
        [Key]
        [Display(Name = "ID")]
        public int? EmployeeId { get; set; }

        [Required]
        [Display(Name = "Name")]
        [Column(TypeName = "varchar(200)")]
        public string EmployeeName { get; set; } = string.Empty;

        [Display(Name = "Employee Image")]
        [Column(TypeName = "varchar(250)")]
        public string? EmployeeImageUrl { get; set; }

        [Display(Name = "Date Of Birth")]
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }

        [Display(Name = "Salary")]
        [Column(TypeName = "decimal(12, 2)")]
        public decimal Salary { get; set; }

        [Display(Name = "Hiring Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MMMM-yyyy}")]
        public DateTime HiringDate { get; set; }

        [Required]
        [StringLength(14, MinimumLength = 14)]
        [Column(TypeName = "varchar(14)")]
        [Display(Name = "National ID")]
        public string NationalId { get; set; } = string.Empty;

        [Required]
        public int DepartmentId { get; set; }

        [ForeignKey("DepartmentId")]
        public Department? Department { get; set; }
    }
}
