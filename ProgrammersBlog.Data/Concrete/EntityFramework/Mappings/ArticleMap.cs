using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProgrammersBlog.Entities.Concrete;

namespace ProgrammersBlog.Data.Concrete.EntityFramework.Mappings
{
    public class ArticleMap: IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.ToTable("Articles");
            builder.HasKey(a => a.Id);
            
            builder.Property(a => a.Id)
                .ValueGeneratedOnAdd();
            
            builder.Property(a => a.Title)
                .HasMaxLength(100)
                .IsRequired();
            
            builder.Property(a => a.Content)
                .IsRequired()
                .HasColumnName("NVARCHAR(MAX)");
            
            builder.Property(a => a.Date)
                .IsRequired();
            
            builder.Property(a => a.SeoAuthor)
                .IsRequired()
                .HasMaxLength(50);
            
            builder.Property(a => a.SeoDescription)
                .HasMaxLength(150)
                .IsRequired();
            
            builder.Property(a => a.SeoTags)
                .IsRequired()
                .HasMaxLength(70);
            
            builder.Property(a => a.ViewsCount)
                .IsRequired();
            
            builder.Property(a => a.CommentCount)
                .IsRequired();

            builder.Property(a => a.Thumbnail)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(a => a.CreatedByName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(a => a.ModifiedByName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(a => a.CreatedDate)
                .IsRequired();
            
            builder.Property(a => a.ModifiedDate)
                .IsRequired();

            builder.Property(a => a.IsActive)
                .IsRequired();

            builder.Property(a => a.IsDeleted)
                .IsRequired();

            builder.Property(a => a.Note)
                .HasMaxLength(500);

            builder.HasOne<Category>(a => a.Category)
                .WithMany(c => c.Articles)
                .HasForeignKey(a => a.CategoryId);

            builder.HasOne<User>(a => a.User)
                .WithMany(u => u.Articles)
                .HasForeignKey(a => a.UserId);
        }
    }
}