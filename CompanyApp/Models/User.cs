using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyApp.Models
{
    public class User
    {
        [Key]
        [Display(Name = "ID")]
        public int? UserId { get; set; }

        [Required]
        [Column(TypeName = "varchar(200)")]
        [Display(Name = "User Name")]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "varchar(200)")]
        [DataType(DataType.Password)]
        [Display(Name = "User Password")]
        public string UserPassword { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "varchar(200)")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "User Email")]
        public string UserEmail { get; set; } = string.Empty;


    }
}
