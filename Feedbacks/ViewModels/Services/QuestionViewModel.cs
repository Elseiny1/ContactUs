using Feedbacks.Entities.Forms;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Feedbacks.ViewModels.Services
{
    public class QuestionViewModel
    {
        public string? Id { get; set; }

        [Required(ErrorMessage = "Question text is requird.")]
        [StringLength(500, MinimumLength = 2,
            ErrorMessage = "Question text must be in range 2 to 500 charachtars")]
        public string QuestionText { get; set; }

        [Required(ErrorMessage = "Question type is requird.")]
        [Range(1, 3, ErrorMessage = "Question type must be in range 1 to 3.")]
        public int QuestionType { get; set; } // Types from 1 to 3.
        public bool IsDeleted { get; set; } = false;

        public string ServiceId { get; set; }
        public string? Massage { get; set; }

        public List<ChoiceViewModel>? Choices { get; set; }
    }
}
