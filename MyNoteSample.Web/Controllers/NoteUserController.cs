using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Web.Mvc;
using MyNoteSample.BusinessLayer;
using MyNoteSample.BusinessLayer.Results;
using MyNoteSample.Entities;
using MyNoteSample.Web.Filters;

namespace MyNoteSample.Web.Controllers
{
    [Auth]
    [AuthAdmin]
    [Exc]
    public class NoteUserController : Controller
    {
        private UserManager um = new UserManager();
        private NoteUser noteUser = new NoteUser();
        public ActionResult Index()
        {
            return View(um.List());
        }
        public ActionResult Details(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            noteUser = um.Find(x => x.Id == Id);
            if (noteUser == null)
            {
                return HttpNotFound();
            }
            return View(noteUser);
        }

        // GET: NoteUser/Create
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NoteUser noteUser)
        {
            ModelState.Remove("CreatedOn");
            ModelState.Remove("ModifiedOn");
            ModelState.Remove("ModifiedUsername");
            if (ModelState.IsValid)
            {
                LayerResult<NoteUser> res = um.Insert(noteUser);
                if (res.Errors.Count > 0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(noteUser);
                }
                return RedirectToAction("Index");
            }

            return View(noteUser);
        }
        public ActionResult Edit(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            noteUser = um.Find(x => x.Id == Id);
            if (noteUser == null)
            {
                return HttpNotFound();
            }
            return View(noteUser);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(NoteUser noteUser)
        {
            ModelState.Remove("CreatedOn");
            ModelState.Remove("ModifiedOn");
            ModelState.Remove("ModifiedUsername");
            if (ModelState.IsValid)
            {
                LayerResult<NoteUser> res = um.Update(noteUser);
                if (res.Errors.Count > 0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(noteUser);
                }
                return RedirectToAction("Index");
            }
            return View(noteUser);
        }
        public ActionResult Delete(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            noteUser = um.Find(x => x.Id == Id);
            if (noteUser == null)
            {
                return HttpNotFound();
            }
            return View(noteUser);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int Id)
        {
            noteUser = um.Find(x => x.Id == Id);
            um.Delete(noteUser);
            return RedirectToAction("Index");
        }
    }
}
