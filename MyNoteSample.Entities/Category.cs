using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyNoteSample.Entities
{
    [Table("Categories")]
    public class Category : EntityBase
    {
        [DisplayName("Başlık"),
            Required(ErrorMessage = "{0} alanı gereklidir."),
            StringLength(50, ErrorMessage = "{0} alanı max. {1} olmalıdır.")]
        public string Title { get; set; }
        [DisplayName("Açıklama"),
            StringLength(200, ErrorMessage = "{0} alanı max. {1} olmalıdır.")]
        public string Description { get; set; }
        public virtual List<Note> Notes { get; set; }
        public Category()
        {
            Notes = new List<Note>();
        }

    }
}
