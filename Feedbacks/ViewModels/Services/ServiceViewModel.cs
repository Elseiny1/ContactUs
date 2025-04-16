using System.ComponentModel.DataAnnotations;

namespace Feedbacks.ViewModels.Services
{
    public class ServiceViewModel
    {
        [Required(ErrorMessage = "Service Name is required.")]
        [StringLength(100, ErrorMessage = "Service name must be at least {2} characters long.", MinimumLength = 2)]
        [RegularExpression(@"^[\u0600-\u06FFa-zA-Z\s]+$", ErrorMessage = "Only Arabic and English letters are allowed.")]
        public string ServiceName { get; set; }
    }
}
