using EFCorePolls.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCorePolls.Infrustructor.Configuration
{
    public class VoteConfiguration : IEntityTypeConfiguration<Vote>
    {
        public void Configure(EntityTypeBuilder<Vote> builder)
        {
            builder.HasKey(v => v.Id);

            builder.Property(v => v.VotedAt)
                   .IsRequired();

            builder.Property(v => v.UserName)
                   .IsRequired();

            // Vote → Option (cascade delete)
            builder.HasOne(v => v.Option)
                   .WithMany(o => o.Votes)
                   .HasForeignKey(v => v.OptionId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Vote → Question (restrict to avoid multiple cascade paths)
            builder.HasOne(v => v.Question)
                   .WithMany(q => q.Votes)
                   .HasForeignKey(v => v.QuestionId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Vote → Poll (restrict to avoid multiple cascade paths)
            builder.HasOne(v => v.Poll)
                   .WithMany(p => p.Votes)
                   .HasForeignKey(v => v.PollId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Vote → User (set null on delete)
            builder.HasOne(v => v.User)
                   .WithMany(u => u.Votes)
                   .HasForeignKey(v => v.UserId)
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
