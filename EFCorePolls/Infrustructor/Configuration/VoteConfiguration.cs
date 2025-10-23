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

            // Vote → Option (many-to-one)
            builder.HasOne(v => v.Option)
                   .WithMany(o => o.Votes)
                   .HasForeignKey(v => v.OptionId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Remove Vote → Poll and Vote → Question (no longer needed)

            //// User relationship (optional)
            //builder.HasOne(v => v.User)
            //       .WithMany(u => u.Votes)
            //       .HasForeignKey(v => v.UserId)
            //       .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
