using System.ComponentModel.DataAnnotations;

namespace blzrwasm_d.Shared
{
    public enum Gender
    {
        Male,
        Female,
        Other
    }
    public class Employee
    {
        public int EmployeeId { get; set; }
        [Required]
        [MinLength(2, ErrorMessage = "FirstName must contains at least 2 charcters")]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public int DepartmentId { get; set; }
        public string PhotoPath { get; set; } = string.Empty;
        public Department? Department { get; set; }
    }
}
