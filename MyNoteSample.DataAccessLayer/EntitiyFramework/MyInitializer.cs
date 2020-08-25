using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using MyNoteSample.Entities;

namespace MyNoteSample.DataAccessLayer.EntityFramework
{
    public class MyInitializer : CreateDatabaseIfNotExists<DatabaseContext>
    {
        protected override void Seed(DatabaseContext context)
        {
            NoteUser admin = new NoteUser()
            {
                ActiveGuid = Guid.NewGuid(),
                IsAdmin = true,
                IsActive = true,
                UserName = "murat",
                Password = "123",
                Email = "murat@gmail.com",
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now.AddMinutes(5),
                ProfileImageFilename = "user_default.png",
                ModifiedUsername = "muratturkoz",
                Name = "murat",
                SurName = "turkoz"

            };
            NoteUser standartUser = new NoteUser()
            {
                ActiveGuid = Guid.NewGuid(),
                IsAdmin = false,
                IsActive = true,
                UserName = "azra",
                Password = "123",
                Email = "azra@gmail.com",
                ProfileImageFilename = "user_default.png",
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now.AddMinutes(5),
                ModifiedUsername = "azraturkoz",
                Name = "azra",
                SurName = "turkoz"
            };
            context.NoteUsers.Add(admin);
            context.NoteUsers.Add(standartUser);
            for (int i = 0; i < 20; i++)
            {
                NoteUser noteUser = new NoteUser()
                {
                    ActiveGuid = Guid.NewGuid(),
                    IsAdmin = false,
                    IsActive = true,
                    UserName = $"user-{i}",
                    Password = "123",
                    Email = FakeData.NetworkData.GetEmail(),
                    CreatedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddDays(-1), DateTime.Now),
                    ModifiedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddDays(-1), DateTime.Now),
                    ProfileImageFilename = "user_default.png",
                    ModifiedUsername = $"user-{i}",
                    Name = FakeData.NameData.GetFirstName(),
                    SurName = FakeData.NameData.GetSurname()


                };
                context.NoteUsers.Add(noteUser);
            }
            context.SaveChanges();
            //Add fakeData Categories..
            List<NoteUser> noteUsers = context.NoteUsers.ToList();
            for (int i = 0; i < 10; i++)
            {
                Category _cat = new Category()
                {
                    Title = FakeData.PlaceData.GetStreetName(),
                    Description = FakeData.PlaceData.GetAddress(),
                    CreatedOn = FakeData.DateTimeData.GetDatetime(),
                    ModifiedOn = FakeData.DateTimeData.GetDatetime(),
                    ModifiedUsername = "muratturkoz"

                };
                context.Categories.Add(_cat);
               
                for (int m = 0; m < FakeData.NumberData.GetNumber(5, 9); m++)
                {
                    NoteUser owner = noteUsers[FakeData.NumberData.GetNumber(0, noteUsers.Count - 1)];
                    Note _note = new Note()
                    {
                        Title = FakeData.TextData.GetAlphabetical(FakeData.NumberData.GetNumber(5, 25)),
                        Text = FakeData.TextData.GetSentences(FakeData.NumberData.GetNumber(1, 3)),
                        Category = _cat,
                        LikeCount = FakeData.NumberData.GetNumber(1, 9),
                        CreatedOn = FakeData.DateTimeData.GetDatetime(),
                        ModifiedOn = FakeData.DateTimeData.GetDatetime(),
                        ModifiedUsername = owner.UserName,
                        NoteUser = owner,
                        //CategoryId = _cat.Id,

                    };
                    _cat.Notes.Add(_note);
                    for (int k = 0; k < FakeData.NumberData.GetNumber(1, 10); k++)
                    {
                        NoteUser comment_owner = noteUsers[FakeData.NumberData.GetNumber(0, noteUsers.Count - 1)];
                        Comment _com = new Comment()
                        {
                            Text = FakeData.TextData.GetSentences(FakeData.NumberData.GetNumber(1, 3)),
                            CreatedOn = FakeData.DateTimeData.GetDatetime(),
                            ModifiedOn = FakeData.DateTimeData.GetDatetime(),
                            ModifiedUsername = comment_owner.UserName,
                            NoteUser = comment_owner,
                            Note = _note,
                        };
                        _note.Comments.Add(_com);
                    }
                    for (int n = 0; n < _note.LikeCount; n++)
                    {
                        Liked _lik = new Liked()
                        {
                            NoteUser = noteUsers[n],


                        };
                        _note.Likes.Add(_lik);
                    }
                }
            }
            context.SaveChanges();
        }
    }
}
