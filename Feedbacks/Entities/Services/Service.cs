using Feedbacks.Entities.Clients;
using System.ComponentModel.DataAnnotations;

namespace Feedbacks.Entities.Forms
{
    public class Service
    {
        [Key]
        public string Id { get; set; }

        [Required(ErrorMessage = "Service name is requird.")]
        [StringLength(500, MinimumLength = 2,
            ErrorMessage = "Service name must be in range 2 to 500 charachtars")]
        public string ServiceName { get; set; }

        public bool IsDeleted { get; set; } = false;

        #region Relationships
        public ICollection<Question> Questions { get; set; }

        public ICollection<ClientAnswer> ClientAnswers { get; set; }

        #endregion
    }
}
