using System.ComponentModel.DataAnnotations;

namespace DemoApp.Models
{
    public class Step1ViewModel
    {
        [Required(ErrorMessage = "Please enter your full name")]
        public string FullName { get; set; }


        public string? Summary { get; set; }

        [Required(ErrorMessage = "Please enter your email")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter your phone number")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Please enter your address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Please enter your date of birth")]
        public DateTime DateOfBirth { get; set; }

    }
}
