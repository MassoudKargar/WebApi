using System.ComponentModel.DataAnnotations;

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Api.Entities.Users
{
    public class Role : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> b)
        {
            b.HasKey(k => k.Id);
            b.Property(k => k.Id).ValueGeneratedOnAdd();
            b.Property(b => b.Name).IsRequired().HasMaxLength(50);
            b.Property(b => b.Description).IsRequired().HasMaxLength(100);
        }
    }
}
