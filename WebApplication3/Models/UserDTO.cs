using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models
{
    public class UserDTO
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Name Is Required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email Is Required")]
        [EmailAddress(ErrorMessage ="Invalid Error Format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password Is Required")]
        [StringLength(20,MinimumLength =3,ErrorMessage ="Password is not matching standard")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm your password")]
        [Compare("Password",ErrorMessage = "Password and Confirm Password must match")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Dob Is Required")]
        public DateTime Dob { get; set; }

        
        [Required(ErrorMessage = "Age Is Required")]
        [Range(18, 80, ErrorMessage = "Invalid Age")]
        public int Age { get; set; }
    }
}
