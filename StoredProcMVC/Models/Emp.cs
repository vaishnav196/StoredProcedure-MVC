using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace StoredProcMVC.Models
{
    public class Emp
    {
        [Key]
        [Required(ErrorMessage = "Id is required")]
        [DisplayName("ID")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [DisplayName("Name")]
        // [StringLength(20, MinimumLength = 10, ErrorMessage = "String length should be more than 10 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Salary is required")]
        [DisplayName("Salary")]
        public double Salary { get; set; }
    }
}
