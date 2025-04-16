using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Feedbacks.Entities.Forms
{
    [Index(nameof(QuestionId), IsUnique = false)]
    public class Choice
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Choice text is requird.")]
        [StringLength(500, MinimumLength = 2,
            ErrorMessage = "Choice text must be in range 2 to 500 charachtars")]
        public string Text { get; set; }


        #region Relationships
        [ForeignKey("QuestionId")]
        public Question Question { get; set; }
        public string QuestionId { get; set; }

        #endregion

    }
}
