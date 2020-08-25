using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyNoteSample.Entities
{
    [Table("Notes")]
    public class Note : EntityBase
    {

        [DisplayName("Not Başlığı"),
            Required(ErrorMessage = "{0} boş geçilemez"),
            StringLength(60, ErrorMessage = "{0} max. {1} kadar olmaldır.")]
        public string Title { get; set; }
        [DisplayName("Not Metni"),
            Required(ErrorMessage = "{0} boş geçilemez"),
            StringLength(2000, ErrorMessage = "{0} max. {1} kadar olmaldır.")]
        public string Text { get; set; }
        [DisplayName("Taslak")]
        public bool IsDraft { get; set; }
        [DisplayName("Beğenilme")]
        public int LikeCount { get; set; }
        public int CategoryId { get; set; }

        public virtual NoteUser NoteUser { get; set; }
        public virtual List<Comment> Comments { get; set; }
        public virtual List<Liked> Likes { get; set; }
        public virtual Category Category { get; set; }

        public Note()
        {
            Comments = new List<Comment>();
            Likes = new List<Liked>();
        }
    }
}
