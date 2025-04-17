using Feedbacks.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace Feedbacks.Entities.Forms
{
    [Index(nameof(ServiceId), IsUnique = false)]
    public class Question
    {
        [Key]
        public string Id { get; set; }

        [Required(ErrorMessage = "Question text is requird.")]
        [StringLength(500, MinimumLength = 2,
            ErrorMessage = "Question text must be in range 2 to 500 charachtars")]
        public string QuestionText { get; set; } = "Add your question here."; //default string for placeholder

        [Required(ErrorMessage = "Question type is requird.")]
        [Range(1, 3, ErrorMessage = "Question type must be in range 1 to 3.")]
        public int QuestionType { get; set; } // Types from 1 to 3.
        public bool IsDeleted { get; set; } = false;

        #region Relationships
        [ForeignKey("ServiceId")]
        public Service Service { get; set; }
        public string ServiceId { get; set; }

        public ICollection<Choice> Choices { get; set; }

        #endregion
    }
}
