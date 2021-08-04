
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Entities.Users
{
    public class Role : IdentityRole<int>, IEntity
    {
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
