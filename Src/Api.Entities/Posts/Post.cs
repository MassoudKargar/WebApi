using System;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Entities.Posts
{
    public class Post : BaseEntity<Guid>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int AuthorId { get; set; }
        public User Author { get; set; }
    }
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> b)
        {
            b.HasKey(k => k.Id);
            b.Property(k => k.Id).ValueGeneratedOnAdd();
            b.Property(b => b.Title).IsRequired().HasMaxLength(200);
            b.Property(b => b.Description).IsRequired();
            b.HasOne(p => p.Category).WithMany(p => p.Posts).HasForeignKey(k => k.CategoryId);
            b.HasOne(p => p.Author).WithMany(p => p.Posts).HasForeignKey(k => k.AuthorId);

        }
    }
}
