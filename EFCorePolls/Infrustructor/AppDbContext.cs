using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFCorePolls.Entity;
using EFCorePolls.Infrustructor.Configuration;
using EFCorePolls.Enums;

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

            model.Entity<User>().HasData(
                new User { Id = 1, Password = "1234", Role = UserEnum.NormalUser, UserName = "user1" },
                new User { Id = 2, Password = "1234", Role = UserEnum.NormalUser, UserName = "user2" },
                new User { Id = 3, Password = "1234", Role = UserEnum.Admin, UserName = "admin1" }
                );

            model.Entity<Poll>().HasData(
                new Poll { Id = 1, Title = "question1" }
                );

            model.Entity<Vote>().HasData(
                new Vote { Id = 1, OptionId = 1, UserId = 1, UserName = "user1" },
                new Vote { Id = 2, OptionId = 2, UserId = 2, UserName = "user2" }
                );
            model.Entity<Question>().HasData(
                new Question { Id=1 , PollId=1, Text="Is Desktop computer better than Laptop?"}
                );
            model.Entity<Option>().HasData(
                new Option { Id=1,QuestionId=1, Text="1.yes" },
                new Option { Id = 2, QuestionId = 1, Text = "2.no" },
                new Option { Id = 3, QuestionId = 1, Text = "3.maybe" },
                new Option { Id = 4, QuestionId = 1, Text = "4.I don't know" }
                );
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Hw16;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
            base.OnConfiguring(optionsBuilder);
        }

    }
}
