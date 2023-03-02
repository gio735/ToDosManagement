using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDosManagement.Domain.Models;

namespace ToDosManagement.Persistence.Config
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Username).IsUnicode(false).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Password).IsUnicode(false).IsRequired().HasMaxLength(256);
            builder.HasMany(x => x.ToDos).WithOne(x => x.Owner).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
