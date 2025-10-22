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
            builder.Property(p => p.Title).IsRequired().HasMaxLength(300);
            builder.HasMany(p => p.Questions).WithOne(p => p.Poll)
                .HasForeignKey(p => p.PollId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(p => p.Votes).WithOne(p => p.Poll)
                .HasForeignKey(p => p.PollId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(p => p.Users).WithMany(u => u.Polls);
        }
    }
}


