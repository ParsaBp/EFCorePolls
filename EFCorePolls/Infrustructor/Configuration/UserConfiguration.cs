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
            builder.HasKey(p => p.Id);
            builder.Property(p => p.UserName).HasMaxLength(100).IsRequired();
            builder.Property(p => p.Password).HasMaxLength(100).IsRequired();
            builder.HasIndex(p => p.UserName).IsUnique();
            builder.HasMany(p => p.Votes).WithOne(p => p.User)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.Property(p => p.Role).HasConversion<string>();


        }
    }
}
