using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProgrammersBlog.Entities.Concrete;

namespace ProgrammersBlog.Data.Concrete.EntityFramework.Mappings
{
    public class UserMap: IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id).ValueGeneratedOnAdd();

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(50);
            
            builder.HasIndex(u => u.Email)
                .IsUnique();

            builder.Property(u => u.UserName)
                .IsRequired()
                .HasMaxLength(20);

            builder.HasIndex(u => u.UserName)
                .IsUnique();

            builder.Property(u => u.PasswordHash)
                .IsRequired()
                .HasColumnType("VARBINARY(500)");

            builder.Property(u => u.Description)
                .HasMaxLength(500);

            builder.Property(u => u.FirstName)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(u => u.LastName)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(u => u.Picture)
                .IsRequired()
                .HasMaxLength(250);
            
            builder.Property(u => u.ModifiedByName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(u => u.CreatedDate)
                .IsRequired();
            
            builder.Property(u => u.ModifiedDate)
                .IsRequired();

            builder.Property(u => u.IsActive)
                .IsRequired();

            builder.Property(u => u.IsDeleted)
                .IsRequired();

            builder.Property(u => u.Note)
                .HasMaxLength(500);

            builder.HasOne<Role>(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId);
        }
    }
}