using System.ComponentModel.DataAnnotations;

namespace EmployeeDetails.Models
{
    public class Employee
    {

        [Key]
        public int EmpId { get; set; }
        [Required(ErrorMessage = "Please enter first name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please enter last name")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Please enter phone number")]
        [MaxLength(10, ErrorMessage = "Please enter valid phone number")]
        [RegularExpression(@"^\+?[0-9]*$", ErrorMessage = "Invalid phone number.")]
        public string Phonenumber { get; set; }
        [Required(ErrorMessage = "Please enter your email address")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter salary")]
        public float Salary { get; set; }
        [DataType(DataType.Date)]
        public DateTime? CreatedOn { get; set; }
        [DataType(DataType.Date)]
        public DateTime? UpdatedOn { get; set; }
    }
}
