using Feedbacks.Entities.Forms;
using System.ComponentModel.DataAnnotations;

namespace Feedbacks.Entities.Clients
{
    public class Client
    {
        [Key]
        public string Id { get; set; }

        [Required(ErrorMessage = "Full Name is required.")]
        [StringLength(200, ErrorMessage = "Full name must be at least {2} characters long.", MinimumLength = 2)]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [StringLength(150, ErrorMessage = "Email must be at least {5} characters long.", MinimumLength = 5)]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone is required.")]
        [StringLength(15, ErrorMessage = "Phone number must be at least {2} characters long.", MinimumLength = 10)]
        [DataType(DataType.PhoneNumber)]
        public int Phone { get; set; }

        #region Relationships
        public ICollection<ClientAnswer> ClientAnswer { get; set; }

        #endregion
    }
}
