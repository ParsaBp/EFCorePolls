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

            // Option → Question (many-to-one)
            builder.HasOne(o => o.Question)
                   .WithMany(q => q.Options)
                   .HasForeignKey(o => o.QuestionId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Option → Votes (one-to-many)
            builder.HasMany(o => o.Votes)
                   .WithOne(v => v.Option)
                   .HasForeignKey(v => v.OptionId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Remove Option → Poll (no longer exists)
        }
    }
}
