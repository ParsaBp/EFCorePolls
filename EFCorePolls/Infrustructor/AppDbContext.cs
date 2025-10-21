using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFCorePolls.Entity;
using EFCorePolls.Infrustructor.Configuration;

namespace EFCorePolls.Infrustructor
{
   public class AppDbContext : DbContext
    {

        public DbSet<User> Users { get; set; }
        public DbSet<Poll> Polls { get; set; }
        public DbSet<Vote> Votes { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<Question> Questions { get; set; }

        protected override void OnModelCreating(ModelBuilder model)
        {
            model.ApplyConfiguration(new UserConfiguration());
            model.ApplyConfiguration(new PollConfiguration());
            model.ApplyConfiguration(new QuestionConfiguration());
            model.ApplyConfiguration(new VoteConfiguration());
            model.ApplyConfiguration(new OptionConfiguration());
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Data Source=LAPTOP-H4J70AS8\\SQLEXPRESS;Initial Catalog=Polls - DB;Integrated Security=True;Trust Server Certificate=True");
            base.OnConfiguring(optionsBuilder);
        }

    }
}
