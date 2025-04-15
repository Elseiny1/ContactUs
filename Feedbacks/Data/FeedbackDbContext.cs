﻿using Feedbacks.Entities;

namespace Feedbacks.Data
{
    public class FeedbackDbContext: DbContext
    {
        public FeedbackDbContext(DbContextOptions<FeedbackDbContext> options) : base(options)
        {
        }
        public DbSet<UserAccount> UserAccounts { get; set; }

    }
}
