using MyNoteSample.DataAccessLayer;
using MyNoteSample.DataAccessLayer.EntityFramework;
using MyNoteSample.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MyNoteSample.BusinessLayer
{
    public class Test
    {
        private Repository<Category> cat = new Repository<Category>();
        private Repository<NoteUser> repo_user = new Repository<NoteUser>();
        private Repository<Note> repo_note = new Repository<Note>();
        private Repository<Comment> repo_comment = new Repository<Comment>();
        public Test()
        {
            List<Category> categories = cat.List();
            List<Category> cat_filter = cat.List(x => x.Id > 5);

        }
        public void InsertTest()
        {

            int result = repo_user.Insert(new NoteUser()
            {
                ActiveGuid = Guid.NewGuid(),
                IsAdmin = false,
                IsActive = true,
                UserName = "Burçin",
                Password = "123",
                Email = "burçin@gmail.com",
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now.AddMinutes(5),
                ModifiedUsername = "burcinturkoz",
                Name = "burcin",
                SurName = "turkoz"

            });
        }
        public void UpdateTest()
        {
            NoteUser noteUser = repo_user.Find(x => x.UserName == "Burçin");
            if (noteUser != null)
            {
                noteUser.UserName = "Burçinxx";
                //repo_user.Update();
            }
        }
        public void DeleteTest(NoteUser noteUser)
        {
            repo_user.Delete(noteUser);
        }
        public void DeleteTest()
        {
            NoteUser noteUser = repo_user.Find(x => x.UserName == "Burçin");
            if (noteUser != null)
            {
                repo_user.Delete(noteUser);
            }
        }
        public void CommentTest()
        {
            NoteUser noteUser = repo_user.Find(x => x.Id == 2);
            Note note = repo_note.Find(x => x.Id == 2);
            Comment comment = new Comment()
            {
                Text = "Bu bir testtir.",
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now,
                ModifiedUsername = "muratturkoz",
                Note = note,
                NoteUser = noteUser
            };
            repo_comment.Insert(comment);
        }

    }
}
