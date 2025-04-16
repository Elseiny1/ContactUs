using Feedbacks.Entities;
using Feedbacks.Entities.Forms;

namespace Feedbacks.Data
{
    public class FeedbackDbContext: DbContext
    {
        public FeedbackDbContext(DbContextOptions<FeedbackDbContext> options) : base(options)
        {
        }
        public DbSet<UserAccount> UserAccounts { get; set; }

        public DbSet<Service> Services { get; set; }
    }
}
