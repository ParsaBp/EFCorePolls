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
            builder.HasKey(q => q.Id);
            builder.Property(q => q.Text)
                   .IsRequired()
                   .HasMaxLength(300);

            // Question → Options (cascade delete)
            builder.HasMany(q => q.Options)
                   .WithOne(o => o.Question)
                   .HasForeignKey(o => o.QuestionId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Question → Votes (restrict to avoid multiple cascade paths)
            builder.HasMany(q => q.Votes)
                   .WithOne(v => v.Question)
                   .HasForeignKey(v => v.QuestionId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Question → Poll (cascade delete)
            builder.HasOne(q => q.Poll)
                   .WithMany(p => p.Questions)
                   .HasForeignKey(q => q.PollId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
