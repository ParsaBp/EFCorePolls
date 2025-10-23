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
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Role).HasConversion<string>();

            builder.Property(u => u.UserName)
                   .IsRequired()
                   .HasMaxLength(100);

            // User → Votes (no cascade, handled via VoteConfiguration)
            builder.HasMany(u => u.Votes)
                   .WithOne(v => v.User)
                   .HasForeignKey(v => v.UserId)
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
