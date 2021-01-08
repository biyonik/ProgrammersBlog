using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProgrammersBlog.Entities.Dtos.Article
{
    public class ArticleUpdateDto
    {
        [DisplayName("Başlık")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz!")]
        [MaxLength(100, ErrorMessage = "{0 alanı maksimum {1} karakter olabilir!}")]
        [MinLength(5, ErrorMessage = "{0} alanı minimum {1} karakter olmalıdır")]
        public string Title { get; set; }
        
        [DisplayName("İçerik")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz!")]
        [MinLength(5, ErrorMessage = "{0} alanı minimum {1} karakter olabilir!")]
        public string Content { get; set; }
        
        [DisplayName("Thumbnail")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz!")]
        [MaxLength(250, ErrorMessage = "{0 alanı maksimum {1} karakter olabilir!}")]
        [MinLength(5, ErrorMessage = "{0} alanı minimum {1} karakter olmalıdır")]
        public string Thumbnail { get; set; }
        
        [DisplayName("Tarih")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz!")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Date { get; set; }
        
        [DisplayName("SEO Yazarı")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz!")]
        [MaxLength(50, ErrorMessage = "{0 alanı maksimum {1} karakter olabilir!}")]
        [MinLength(5, ErrorMessage = "{0} alanı minimum {1} karakter olmalıdır")]
        public string SeoAuthor { get; set; }
        
        [DisplayName("SEO Açıklaması")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz!")]
        [MaxLength(150, ErrorMessage = "{0 alanı maksimum {1} karakter olabilir!}")]
        [MinLength(5, ErrorMessage = "{0} alanı minimum {1} karakter olmalıdır")]
        public string SeoDescription { get; set; }
        
        [DisplayName("SEO Etiketleri")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz!")]
        [MaxLength(70, ErrorMessage = "{0 alanı maksimum {1} karakter olabilir!}")]
        [MinLength(5, ErrorMessage = "{0} alanı minimum {1} karakter olmalıdır")]
        public string SeoTags { get; set; }
        
        [DisplayName("Kategori")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz!")]
        public int CategoryId { get; set; }
        
        public virtual Concrete.Category Category { get; set; }
    }
}