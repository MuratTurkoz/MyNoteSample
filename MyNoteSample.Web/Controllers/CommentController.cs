using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyNoteSample.BusinessLayer;
using MyNoteSample.Entities;
using MyNoteSample.Web.Filters;
using MyNoteSample.Web.Models;

namespace MyNoteSample.Web.Controllers
{
    public class CommentController : Controller
    {
        // Böylede olabilir
        private CommentManager cm = new CommentManager();
        private NoteManager nm = new NoteManager();
        public ActionResult ShowNoteComments(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Note note = nm.Find(x => x.Id == Id);
            //var comments = cm.ListQueryable().Include("Note").Include("NoteUser").
            //   Where(z => z.Note.Id == Id).
            //   OrderByDescending(x => x.ModifiedOn);
            if (note == null)
            {
                return HttpNotFound();
            }
            return PartialView("_PartialComments",
                note.Comments.OrderByDescending(z => z.ModifiedOn).ToList());
        }
        [Auth]
        [HttpPost]
        public ActionResult Edit(int? Id, string text)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = cm.Find(x => x.Id == Id);
            if (comment == null)
            {
                return new HttpNotFoundResult();
            }
            comment.Text = text;
            int res = cm.Update(comment);
            if (res > 0)
            {
                return Json(new { result = true }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { result = false }, JsonRequestBehavior.AllowGet);

        }
        [Auth]
        public ActionResult Delete(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = cm.Find(x => x.Id == Id);
            if (comment == null)
            {
                return new HttpNotFoundResult();
            }
            int res = cm.Delete(comment);
            if (res > 0)
            {
                return Json(new { result = true }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { result = false }, JsonRequestBehavior.AllowGet);

        }
        [Auth]
        [HttpPost]
        public ActionResult Create(Comment comment, int? noteid)
        {
            ModelState.Remove("CreatedOn");
            ModelState.Remove("ModifiedOn");
            ModelState.Remove("ModifiedUsername");
            if (ModelState.IsValid)
            {
                if (noteid == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                Note note = nm.Find(x => x.Id == noteid);
                if (note == null)
                {
                    return new HttpNotFoundResult();
                }
                comment.Note = note;
                comment.NoteUser = CurrentSession.user;
                int res = cm.Insert(comment);
                if (res > 0)
                {
                    return Json(new { result = true }, JsonRequestBehavior.AllowGet);
                }

            }
            return Json(new { result = false }, JsonRequestBehavior.AllowGet);
        }
    }
}