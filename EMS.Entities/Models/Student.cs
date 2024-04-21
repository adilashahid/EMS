using System.ComponentModel.DataAnnotations;

namespace EMS.Entities.Models
{
    public class Student
    {

        [Key]
        public int RollNo { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
         public string FatherName { get; set; }
        public string ClassName { get; set; }
        public string? Address { get; set; }
        [MaxLength(15)]
        [MinLength(11)]
        public string MobileNo { get; set; }
        public string? Email
        {
            get; set;
        }
        [Required]
        public DateTime DOB { get; set; }
  

        public decimal Fee { get; set; }


        //[NotMapped]
        //public IFormFile Image { get; set; }
        //public string? ImagePath { get; set; }
        //public Teacher Teacher { get; set; }
    }
}
