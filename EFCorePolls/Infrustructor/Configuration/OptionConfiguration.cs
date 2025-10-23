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
    public class OptionConfiguration : IEntityTypeConfiguration<Option>
    {
        public void Configure(EntityTypeBuilder<Option> builder)
        {
            builder.HasKey(o => o.Id);

            builder.Property(o => o.Text)
                   .IsRequired()
                   .HasMaxLength(300);

            // Option → Question (cascade delete)
            builder.HasOne(o => o.Question)
                   .WithMany(q => q.Options)
                   .HasForeignKey(o => o.QuestionId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Option → Poll (restrict to avoid multiple cascade paths)
            builder.HasOne(o => o.Poll)
                   .WithMany(p => p.Options)
                   .HasForeignKey(o => o.PollId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Option → Votes (cascade delete)
            builder.HasMany(o => o.Votes)
                   .WithOne(v => v.Option)
                   .HasForeignKey(v => v.OptionId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
