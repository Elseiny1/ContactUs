using Feedbacks.Entities;
using Feedbacks.Entities.Clients;
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
        public DbSet<Question> Questions { get; set; }
        public DbSet<ClientAnswer> ClientAnswers { get; set; }
        public DbSet<Choice> Choices { get; set; }
    }
}
