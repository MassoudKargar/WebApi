using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Api.Entities.Posts;

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Api.Entities
{
    public enum GenderType
    {
        [Display(Name = "مرد")]
        Male = 1,

        [Display(Name = "زن")]
        Female = 2
    }
    public class User : BaseEntity
    {
        public User()
        {
            IsActive = true;
            Posts = new HashSet<Post>();
        }

        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        [Required]
        [StringLength(100)]
        public string FullName { get; set; }
        public int Age { get; set; }
        public GenderType Gender { get; set; }
        public bool IsActive { get; set; }
        public DateTimeOffset LastLoginDate { get; set; }

        public ICollection<Post> Posts { get; set; }

    }
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> b)
        {
            b.HasKey(k => k.Id);
            b.Property(k => k.Id).ValueGeneratedOnAdd();
            b.Property(b => b.UserName).IsRequired().HasMaxLength(100);
            b.Property(b => b.FullName).IsRequired().HasMaxLength(100);
            b.Property(b => b.PasswordHash).IsRequired().HasMaxLength(500);
        }
    }
}
