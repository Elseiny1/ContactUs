using System.ComponentModel.DataAnnotations;

namespace Feedbacks.ViewModels.Client
{
    public class ClientViewModel
    {
        [Required(ErrorMessage = "Full Name is required.")]
        [StringLength(100, ErrorMessage = "Full name must be at least {2} characters long.", MinimumLength = 2)]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone is required.")]
        [StringLength(15, ErrorMessage = "Phone number must be at least {2} characters long.", MinimumLength = 10)]
        [RegularExpression(@"^(009665|9665|\+9665|05|5)(5|0|3|6|4|9|1|8|7)([0-9]{7})$",
            ErrorMessage = "Phone number must be in the format 05XXXXXXXX or 9665XXXXXXXXX.")]
        [DataType(DataType.PhoneNumber)]
        public int Phone { get; set; }
    }
}
