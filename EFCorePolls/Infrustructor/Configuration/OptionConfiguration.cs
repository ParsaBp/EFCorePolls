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
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Text).HasMaxLength(300);

            builder.HasMany(p => p.Votes).WithOne(p => p.Option)
                .HasForeignKey(p => p.OptionId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(p => p.Question).WithMany(p => p.Options)
                .HasForeignKey(p => p.QuestionId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
