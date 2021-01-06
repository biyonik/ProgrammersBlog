using System;
using System.Text;
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

            builder.HasData(new User
            {
                Id = 1,
                RoleId = 1,
                FirstName = "Ahmet",
                LastName = "Altun",
                Email = "ahmet.altun60@gmail.com",
                UserName = "admin",
                IsActive = true,
                IsDeleted = false,
                CreatedByName = "Initial Create",
                CreatedDate = DateTime.Now,
                ModifiedByName = "InitialCreate",
                ModifiedDate = DateTime.Now,
                PasswordHash = Encoding.ASCII.GetBytes("0192023a7bbd73250516f069df18b500"),
                Picture = "https://encrypted-tbn0.gstatic.com/images?q=tbn%3AANd9GcSX4wVGjMQ37PaO4PdUVEAliSLi8-c2gJ1zvQ&usqp=CAU"
            });
        }
    }
}