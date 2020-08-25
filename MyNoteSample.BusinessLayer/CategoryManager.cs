using MyNoteSample.BusinessLayer.Abstract;
using MyNoteSample.Entities;
using System.Linq;

namespace MyNoteSample.BusinessLayer
{
    public class CategoryManager : ManagerBase<Category>
    {
        //Bu Yöntem ile de yapılabilirdi ancak Sql ile yapmak daha iyi
        //public override int Delete(Category obj)
        //{
        //    NoteManager nm = new NoteManager();
        //    CommentManager coom = new CommentManager();
        //    LikedManager lm = new LikedManager();
        //    //ilişki notlar silinmeli
        //    //yorumlar silinmeli
        //    //Like silinmeli
        //    foreach (Note item in obj.Notes.ToList())
        //    {
        //        foreach (Liked like in item.Likes.ToList())
        //        {
        //            lm.Delete(like);
        //        }
        //        foreach (Comment com in item.Comments.ToList())
        //        {
        //            coom.Delete(com);
        //        }
        //        nm.Delete(item);
        //    }
        //    return base.Delete(obj);
        //}
    }
}
