using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProgrammersBlog.Entities.Concrete;

namespace ProgrammersBlog.Data.Concrete.EntityFramework.Mappings
{
    public class ArticleMap : IEntityTypeConfiguration<Article>
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

            builder.HasData(
                new Article
                {
                    Id = 1,
                    CategoryId = 1,
                    Title = "C# 9.0 ve .NET 5 Yenilikleri",
                    Content =
                        "Lorem Ipsum, dizgi ve baskı endüstrisinde kullanılan mıgır metinlerdir. " +
                        "Lorem Ipsum, adı bilinmeyen bir matbaacının bir hurufat numune kitabı oluşturmak üzere bir yazı galerisini alarak karıştırdığı 1500'lerden beri endüstri standardı sahte metinler olarak kullanılmıştır. " +
                        "Beşyüz yıl boyunca varlığını sürdürmekle kalmamış, aynı zamanda pek değişmeden elektronik dizgiye de sıçramıştır. " +
                        "1960'larda Lorem Ipsum pasajları da içeren Letraset yapraklarının yayınlanması ile ve yakın zamanda Aldus PageMaker gibi Lorem Ipsum sürümleri içeren masaüstü yayıncılık yazılımları ile popüler olmuştur.",
                    Thumbnail = "default.jpg",
                    Date = DateTime.Now,
                    SeoDescription = "C# 9.0 ve .NET 5 Yenilikleri",
                    SeoTags = "C#, .NET, C# 9, .NET Framework, .NET Core",
                    SeoAuthor = "Initial Create",
                    IsActive = true,
                    IsDeleted = false,
                    CreatedByName = "Initial Create",
                    CreatedDate = DateTime.Now,
                    ModifiedByName = "InitialCreate",
                    ModifiedDate = DateTime.Now,
                    Note = "C# Kategorisinde Bir Blog",
                    UserId = 1,
                    ViewsCount = 100,
                    CommentCount = 1
                },
                new Article
                {
                    Id = 2,
                    CategoryId = 2,
                    Title = "C++11 ve C++19 Yenilikleri",
                    Content =
                        "Yinelenen bir sayfa içeriğinin okuyucunun dikkatini dağıttığı bilinen bir gerçektir. " +
                        "Lorem Ipsum kullanmanın amacı, sürekli 'buraya metin gelecek, buraya metin gelecek' yazmaya kıyasla daha dengeli bir harf dağılımı sağlayarak okunurluğu artırmasıdır. ," +
                        "Şu anda birçok masaüstü yayıncılık paketi ve web sayfa düzenleyicisi, varsayılan mıgır metinler olarak Lorem Ipsum kullanmaktadır. " +
                        "Ayrıca arama motorlarında 'lorem ipsum' anahtar sözcükleri ile arama yapıldığında henüz tasarım aşamasında olan çok sayıda site listelenir. " +
                        "Yıllar içinde, bazen kazara, bazen bilinçli olarak (örneğin mizah katılarak), çeşitli sürümleri geliştirilmiştir.",
                    Thumbnail = "default.jpg",
                    Date = DateTime.Now,
                    SeoDescription = "C++11 ve C++19 Yenilikleri",
                    SeoTags = "C++, cplusplus, C++11, C++19",
                    SeoAuthor = "Initial Create",
                    IsActive = true,
                    IsDeleted = false,
                    CreatedByName = "Initial Create",
                    CreatedDate = DateTime.Now,
                    ModifiedByName = "InitialCreate",
                    ModifiedDate = DateTime.Now,
                    Note = "C++11 ve C++19 Yenilikleri",
                    UserId = 1,
                    ViewsCount = 200,
                    CommentCount = 1
                },
                new Article
                {
                    Id = 3,
                    CategoryId = 3,
                    Title = "Angular vs ReactJS vs VueJS",
                    Content =
                        "Lorem Ipsum pasajlarının birçok çeşitlemesi vardır. " +
                        "Ancak bunların büyük bir çoğunluğu mizah katılarak veya rastgele sözcükler eklenerek değiştirilmişlerdir. " +
                        "Eğer bir Lorem Ipsum pasajı kullanacaksanız, metin aralarına utandırıcı sözcükler gizlenmediğinden emin olmanız gerekir. " +
                        "İnternet'teki tüm Lorem Ipsum üreteçleri önceden belirlenmiş metin bloklarını yineler. Bu da, bu üreteci İnternet üzerindeki gerçek Lorem Ipsum üreteci yapar. " +
                        "Bu üreteç, 200'den fazla Latince sözcük ve onlara ait cümle yapılarını içeren bir sözlük kullanır. " +
                        "Bu nedenle, üretilen Lorem Ipsum metinleri yinelemelerden, mizahtan ve karakteristik olmayan sözcüklerden uzaktır.",
                    Thumbnail = "default.jpg",
                    Date = DateTime.Now,
                    SeoDescription = "Angular vs ReactJS vs VueJS",
                    SeoTags = "Angular, ReactJS, VueJS, JavaScript, JS",
                    SeoAuthor = "Initial Create",
                    IsActive = true,
                    IsDeleted = false,
                    CreatedByName = "Initial Create",
                    CreatedDate = DateTime.Now,
                    ModifiedByName = "InitialCreate",
                    ModifiedDate = DateTime.Now,
                    Note = "Angular vs ReactJS vs VueJS",
                    UserId = 1,
                    ViewsCount = 195,
                    CommentCount = 1
                }
            );
        }
    }
}