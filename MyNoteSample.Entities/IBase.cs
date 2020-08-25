using System;

namespace MyNoteSample.Entities
{
    public interface IBase
    {
        int Id { get; set; }
        DateTime CreatedOn { get; set; }
        DateTime ModifiedOn { get; set; }
        string ModifiedUsername { get; set; }
    }
}
