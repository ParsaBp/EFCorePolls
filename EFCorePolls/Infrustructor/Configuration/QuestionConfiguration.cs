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
                   .HasMaxLength(500);

            // Question → Poll (many-to-one)
            builder.HasOne(q => q.Poll)
                   .WithMany(p => p.Questions)
                   .HasForeignKey(q => q.PollId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Question → Options (one-to-many)
            builder.HasMany(q => q.Options)
                   .WithOne(o => o.Question)
                   .HasForeignKey(o => o.QuestionId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Remove Question → Votes (votes tracked via Options)
        }
    }
}
