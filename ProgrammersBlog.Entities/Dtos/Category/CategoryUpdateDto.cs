﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProgrammersBlog.Entities.Dtos.Category
{
    public class CategoryUpdateDto
    {
        [Required]
        public int Id { get; set; }
        
        [DisplayName("kategori Adı:")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz!")]
        [MaxLength(70, ErrorMessage = "{0} alanı, maksimum {1} karakter uzunluğunda olabilir!")]
        [MinLength(2, ErrorMessage = "{0} alanı, minimum {1} karakter olmalıdır!")]
        public string Name { get; set; }
        
        [DisplayName("Kategori Açıklaması:")]
        [MaxLength(500, ErrorMessage = "{0} alanı, maksimum {1} karakter uzunluğunda olabilir!")]
        public string Description { get; set; }
        
        [DisplayName("Kategori Özel Not Alanı: ")]
        [MaxLength(500, ErrorMessage = "{0} alanı, maksimum {1} karakter uzunluğunda olabilir!")]
        public string Note { get; set; }
        
        [DisplayName("Durumu:")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz")]
        public bool IsActive { get; set; }
        
        [DisplayName("Silindi mi?")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz")]
        public bool IsDeleted { get; set; }
    }
}