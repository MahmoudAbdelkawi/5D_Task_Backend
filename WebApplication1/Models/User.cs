using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is Required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Weight is Required")]
        public string Weight { get; set; }
        public string? Photo { get; set; }
        [Required(ErrorMessage = "DateOfBirth is Required")]
        public DateTime DateOfBirth { get; set;}
    }
}
