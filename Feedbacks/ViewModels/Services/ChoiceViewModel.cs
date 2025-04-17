using Feedbacks.Entities.Forms;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Feedbacks.ViewModels.Services
{
    public class ChoiceViewModel
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "Choice text is requird.")]
        [StringLength(500, MinimumLength = 2,
            ErrorMessage = "Choice text must be in range 2 to 500 charachtars")]
        [RegularExpression(@"^[\u0600-\u06FFa-zA-Z\s]+$", ErrorMessage = "Only Arabic and English letters are allowed.")]
        public string Text { get; set; }

        public string? QuestionId { get; set; }
    }
}
