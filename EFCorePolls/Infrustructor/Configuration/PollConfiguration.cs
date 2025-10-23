using EFCorePolls.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCorePolls.Infrustructor.Configuration
{
    public class PollConfiguration : IEntityTypeConfiguration<Poll>
    {

        public void Configure(EntityTypeBuilder<Poll> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Title)
                   .IsRequired()
                   .HasMaxLength(300);

            // Poll → Questions (cascade delete)
            builder.HasMany(p => p.Questions)
                   .WithOne(q => q.Poll)
                   .HasForeignKey(q => q.PollId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Poll → Votes (restrict to avoid multiple cascade paths)
            builder.HasMany(p => p.Votes)
                   .WithOne(v => v.Poll)
                   .HasForeignKey(v => v.PollId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Poll → Options (restrict to avoid multiple cascade paths)
            builder.HasMany(p => p.Options)
                   .WithOne(o => o.Poll)
                   .HasForeignKey(o => o.PollId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}


