using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyNoteSample.Entities
{
    [Table("Comments")]
    public class Comment : EntityBase
    {
        [Required, StringLength(300)]
        public string Text { get; set; }
        public virtual Note Note { get; set; }
        public virtual NoteUser NoteUser { get; set; }

    }
}
