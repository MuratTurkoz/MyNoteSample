using System.Net;
using System.Web.Mvc;
using MyNoteSample.BusinessLayer;
using MyNoteSample.Entities;
using MyNoteSample.Web.Filters;
using MyNoteSample.Web.Models;

namespace MyNoteSample.Web.Controllers
{
    [Auth]
    [AuthAdmin]
    [Exc]
    public class CategoryController : Controller
    {
        private CategoryManager cm = new CategoryManager();
        public ActionResult Index()
        {
            return View(cm.List());
        }
        public ActionResult Details(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = cm.Find(x => x.Id == Id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category category)
        {
            ModelState.Remove("CreatedOn");
            ModelState.Remove("ModifiedOn");
            ModelState.Remove("ModifiedUsername");
            if (ModelState.IsValid)
            {
                int res = cm.Insert(category);
                CacheHelper.RemoveCategoriesFromCache();
                if (res > 0)
                {
                    return RedirectToAction("Index");
                }
                return View(category);
            }

            return View(category);
        }
        public ActionResult Edit(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = cm.Find(x => x.Id == Id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category category)
        {
            ModelState.Remove("CreatedOn");
            ModelState.Remove("ModifiedOn");
            ModelState.Remove("ModifiedUsername");
            if (ModelState.IsValid)
            {
                Category cat = cm.Find(x => x.Id == category.Id);
                cat.Title = category.Title;
                cat.Description = category.Description;
                //TODO:incele
                cm.Update(category);
                CacheHelper.RemoveCategoriesFromCache();
                return RedirectToAction("Index");
            }
            return View(category);
        }
        public ActionResult Delete(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = cm.Find(x => x.Id == Id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int Id)
        {
            Category category = cm.Find(x => x.Id == Id);
            cm.Delete(category);
            CacheHelper.RemoveCategoriesFromCache();
            return RedirectToAction("Index");
        }
    }
}
