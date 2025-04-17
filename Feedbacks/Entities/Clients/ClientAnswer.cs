using Feedbacks.Entities.Forms;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Feedbacks.Entities.Clients
{
    [Index(nameof(ClientId), IsUnique = false)]
    [Index(nameof(ChoiceId), IsUnique = false)]
    public class ClientAnswer
    {
        [Key]
        public string Id { get; set; }

        [StringLength(500,MinimumLength = 2, 
            ErrorMessage = "Text must in range 2 to 500 charachtars")]
        public string? AnswerText { get; set; }//this field will contain the text and the true or false ansers

        [Range(1,20, ErrorMessage = "Choice Id must be in range of 1 to 20.")]
        public int? ChoiceId { get; set; }

        #region Relationships
        [ForeignKey("ClientId")]
        public Client Client { get; set; }
        public string ClientId { get; set; }

        public ICollection<Service> Services { get; set; }

        #endregion

    }
}
