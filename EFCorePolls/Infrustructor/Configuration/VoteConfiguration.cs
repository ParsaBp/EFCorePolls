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

            builder.HasOne(v => v.Option)
                   .WithMany(o => o.Votes)
                   .HasForeignKey(v => v.OptionId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
