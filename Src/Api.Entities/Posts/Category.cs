using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Api.Entities.Posts
{
    public class Category : BaseEntity
    {
        public Category()
        {
            ChildCategories = new HashSet<Category>();
            Posts = new HashSet<Post>();
        }

        public string Name { get; set; }

        [ForeignKey(nameof(ParentCategory))]
        public int? ParentCategoryId { get; set; }
        public Category ParentCategory { get; set; }

        public ICollection<Category> ChildCategories { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> b)
        {
            b.HasKey(k => k.Id);
            b.Property(k => k.Id).ValueGeneratedOnAdd();
            b.Property(b => b.Name).IsRequired().HasMaxLength(50);
            b.HasOne(p => p.ParentCategory).WithMany(p => p.ChildCategories).HasForeignKey(k => k.ParentCategoryId);

        }
    }
}
