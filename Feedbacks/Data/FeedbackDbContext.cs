using Feedbacks.Entities;
using Feedbacks.Entities.Forms;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Feedbacks.Data
{
    public class FeedbackDbContext: IdentityDbContext<AdminIdentity> 
    {
        public FeedbackDbContext(DbContextOptions<FeedbackDbContext> options) : base(options)
        {
        }
        public DbSet<Service> Services { get; set; }
    }
}
