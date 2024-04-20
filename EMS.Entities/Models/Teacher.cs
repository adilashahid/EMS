using System.ComponentModel.DataAnnotations;

namespace EMS.Entities.Models
{
    public class Teacher
    {
        [Key]
        public int TeacherID { get; set; }

        [StringLength(50)]
        public string 
            Name { get; set; }

        [StringLength(50)]
        public string FatherName { get; set; }
        public string? Address { get; set; }
        [MaxLength(15)]
        [MinLength(11)]
        public string MobileNo { get; set; }
        public string? Email { get; set; }
        [Required]
        public int Salary { get; set; }
        public DateTime DOB { get; set; }
        //public List<Student> Student { get; set; }
        //public IFormFile Image { get; set; }
        //public string? ImagePath { get; set; }
    }
}
