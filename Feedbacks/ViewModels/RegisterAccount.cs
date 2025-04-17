using System.ComponentModel.DataAnnotations;

namespace Feedbacks.ViewModels
{
    public class RegisterAccount
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
            ErrorMessage = "Please Enter Valid Email.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(200, ErrorMessage = "Password must be at least {6} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(200, ErrorMessage = "Password must be at least {6} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        public string? massage { get; set; }
    }
}
