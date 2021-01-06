using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProgrammersBlog.Entities.Concrete;

namespace ProgrammersBlog.Data.Concrete.EntityFramework.Mappings
{
    public class CommentMap: IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("Comments");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();

            builder.Property(c => c.Text)
                .IsRequired()
                .HasMaxLength(1000);
            
            builder.Property(c => c.ModifiedByName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(c => c.CreatedDate)
                .IsRequired();
            
            builder.Property(c => c.ModifiedDate)
                .IsRequired();

            builder.Property(c => c.IsActive)
                .IsRequired();

            builder.Property(c => c.IsDeleted)
                .IsRequired();

            builder.Property(c => c.Note)
                .HasMaxLength(500);

            builder.HasOne<Article>(c => c.Article)
                .WithMany(a => a.Comments)
                .HasForeignKey(c => c.ArticeId);
        }
    }
}