using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using MyNoteSample.BusinessLayer;
using MyNoteSample.Entities;
using MyNoteSample.Web.Filters;
using MyNoteSample.Web.Models;

namespace MyNoteSample.Web.Controllers
{
    [Exc]
    public class NoteController : Controller
    {
        private NoteManager nm = new NoteManager();
        //private CategoryManager cm = new CategoryManager();
        private LikedManager lm = new LikedManager();

        private Note _note = new Note();
        [Auth]
        public ActionResult Index()
        {
            var notes = nm.ListQueryable().
                Include("Category").
                Include("NoteUser").
                Where(z => z.NoteUser.Id == CurrentSession.user.Id).
                OrderByDescending(x => x.ModifiedOn);
            return View(notes.ToList());
        }
        public ActionResult GetNoteDetail(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            _note = nm.Find(x => x.Id == Id);
            if (_note == null)
            {
                return HttpNotFound();
            }
            return PartialView("_PartialNoteDetail", _note);
        }
        [Auth]
        public ActionResult MyLikedNotes()
        {
            var notes = lm.ListQueryable().Include("NoteUser").Include("Note").Where(
                x => x.NoteUser.Id == CurrentSession.user.Id).Select(
                x => x.Note).Include("Category").Include("NoteUser").OrderByDescending(
                x => x.ModifiedOn);
            return View("Index", notes.ToList());
        }
        [Auth]
        public ActionResult Details(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            _note = nm.Find(x => x.Id == Id);
            if (_note == null)
            {
                return HttpNotFound();
            }
            return View(_note);
        }
        [Auth]
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(CacheHelper.GetCategoriesFromCache(), "Id", "Title");
            return View();
        }
        [Auth]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Note note)
        {
            ModelState.Remove("CreatedOn");
            ModelState.Remove("ModifiedOn");
            ModelState.Remove("ModifiedUsername");
            if (ModelState.IsValid)
            {
                note.NoteUser = CurrentSession.user;
                nm.Insert(note);
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(CacheHelper.GetCategoriesFromCache(), "Id", "Title", note.CategoryId);
            return View(note);
        }
        [Auth]
        public ActionResult Edit(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            _note = nm.Find(x => x.Id == Id);
            if (_note == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(CacheHelper.GetCategoriesFromCache(), "Id", "Title", _note.CategoryId);
            return View(_note);
        }
        [Auth]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Note note)
        {
            ModelState.Remove("CreatedOn");
            ModelState.Remove("ModifiedOn");
            ModelState.Remove("ModifiedUsername");
            if (ModelState.IsValid)
            {
                _note = nm.Find(z => z.Id == note.Id);
                _note.IsDraft = note.IsDraft;
                _note.CategoryId = note.CategoryId;
                _note.Text = note.Text;
                _note.Title = note.Title;
                nm.Update(_note);
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(CacheHelper.GetCategoriesFromCache(), "Id", "Title", note.CategoryId);
            return View(note);
        }
        [Auth]
        public ActionResult Delete(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            _note = nm.Find(x => x.Id == Id);
            if (_note == null)
            {
                return HttpNotFound();
            }
            return View(_note);
        }
        [Auth]
        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int Id)
        {
            _note = nm.Find(x => x.Id == Id);
            nm.Delete(_note);

            return RedirectToAction("Index");
        }
    }
}
