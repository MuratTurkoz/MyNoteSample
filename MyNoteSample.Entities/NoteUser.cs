using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyNoteSample.Entities
{
    [Table("NoteUsers")]
    public class NoteUser : EntityBase
    {
        [DisplayName("İsim"),
            StringLength(25, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string Name { get; set; }
        [DisplayName("Soyad"),
            StringLength(25,ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string SurName { get; set; }
        [DisplayName("Kullanıcı Adı"),
            Required(ErrorMessage = "{0} alanı gereklidir."),
            StringLength(25, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string UserName { get; set; }
        [DisplayName("E-Posta"),
            Required(ErrorMessage = "{0} alanı gereklidir."),
            StringLength(70, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string Email { get; set; }
        [DisplayName("Şifre"),
            Required(ErrorMessage = "{0} alanı gereklidir."),
            StringLength(25, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır."),
            DataType(DataType.Password)]
        
        public string Password { get; set; }
        [StringLength(30), ScaffoldColumn(false)]
        public string ProfileImageFilename { get; set; }

        [Required, ScaffoldColumn(false)]
        public Guid ActiveGuid { get; set; }
        [DisplayName("Is Active")]
        public bool IsActive { get; set; }
        [DisplayName("Is Admin")]
        public bool IsAdmin { get; set; }
        public virtual List<Note> Notes { get; set; }
        public virtual List<Liked> Likeds { get; set; }
        public virtual List<Comment> Comments { get; set; }

    }
}
