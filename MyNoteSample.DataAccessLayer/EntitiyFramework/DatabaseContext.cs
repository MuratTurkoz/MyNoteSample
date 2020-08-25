using MyNoteSample.Entities;
using System.Data.Entity;


namespace MyNoteSample.DataAccessLayer.EntityFramework
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Liked> Likeds { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<NoteUser> NoteUsers { get; set; }
        public DatabaseContext()
        {
            Database.SetInitializer(new MyInitializer());
        }
        //SQL yapılan işi database oluşurken Code kısmında bu şekilde düzenleyebilirizç
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    //FluentAPI
        //    modelBuilder.Entity<Note>()
        //        .HasMany(n => n.Comments)
        //        .WithRequired(c => c.Note).WillCascadeOnDelete(true);
        //    modelBuilder.Entity<Note>()
        //       .HasMany(n => n.Likes)
        //       .WithRequired(c => c.Note).WillCascadeOnDelete(true);

        //}
    }
}
