using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFCorePolls.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCorePolls.Infrustructor.Configuration
{
    public class QuestionConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Text).IsRequired().HasMaxLength(300);
            builder.HasMany(p => p.Options).WithOne(p => p.Question)
                .HasForeignKey(p => p.QuestionId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(p => p.Poll).WithMany(p => p.Questions)
                .HasForeignKey(p => p.PollId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(p => p.Votes).WithOne(p => p.Question)
                .HasForeignKey(p => p.QuestionId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
