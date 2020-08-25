using MyNoteSample.BusinessLayer;
using MyNoteSample.BusinessLayer.Results;
using MyNoteSample.Entities;
using MyNoteSample.Entities.Messages;
using MyNoteSample.Entities.ValueObject;
using MyNoteSample.Web.Filters;
using MyNoteSample.Web.Models;
using MyNoteSample.Web.Models.ViewModels.NotifyViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace MyNoteSample.Web.Controllers
{
    [Exc]
    public class HomeController : Controller
    {

        // GET: Home
        private UserManager um = new UserManager();
        private NoteManager nm = new NoteManager();
        private CategoryManager cm = new CategoryManager();
        public ActionResult Index()
        {
            return View(nm.ListQueryable().Where(x => x.IsDraft == false).OrderByDescending(x => x.ModifiedOn).ToList());
        }
        public ActionResult About()
        {
            return View();
        }

        public ActionResult ByCategory(int? Id)
        {
            //Boyle yapılabilir...
            //if (Id == null)
            //{
            //    return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            //}
            //Category cat = cm.Find(x => x.Id == Id);
            ////List<Note> note = cat.Notes;
            //if (cat == null)
            //{
            //    return HttpNotFound();
            //}
            //return View("Index", cat.Notes.Where(z => z.IsDraft == false).OrderByDescending(x => x.ModifiedOn).ToList());
            List<Note> note = nm.ListQueryable().Where(
                x => x.IsDraft == false && x.CategoryId == Id).OrderByDescending(
                x => x.ModifiedOn).ToList();
            return View("Index", note);
        }

        public PartialViewResult GetCategoriesPartial()
        {
            return PartialView("_PartialCategories", CacheHelper.GetCategoriesFromCache());
        }

        public ActionResult LastNotes()
        {
            return View("Index", nm.List().OrderByDescending(x => x.ModifiedOn).ToList());
        }
        public ActionResult MostLiked()
        {
            return View("Index", nm.ListQueryable().OrderByDescending(x => x.LikeCount).ToList());
            //return PartialView("_PartialNote", nm.GetIQueryableNotes().OrderByDescending(x => x.LikeCount).ToList());
        }
        [Auth]
        public ActionResult ShowProfile()
        {

            LayerResult<NoteUser> res = um.GetUseryById(CurrentSession.user.Id);
            if (res.Errors.Count > 0)
            {
                //TODO: Kullanıcıyı bir hata ekranınıa yönlendirmeliyiz.
                if (res.Errors.Count > 0)
                {
                    ErrorViewModel evm = new ErrorViewModel()
                    {
                        Title = "Geçersiz İşlem",
                        RedirectingTimeOut = 3000,
                        Items = res.Errors,

                    };
                    return View("Error", evm);
                }
            }
            return View(res.Result);
        }
        [Auth]
        public ActionResult EditProfile()
        {
            LayerResult<NoteUser> res = um.GetUseryById(CurrentSession.user.Id);
            if (res.Errors.Count > 0)
            {
                //TODO: Kullanıcıyı bir hata ekranınıa yönlendirmeliyiz.
                if (res.Errors.Count > 0)
                {
                    ErrorViewModel evm = new ErrorViewModel()
                    {
                        Title = "Geçersiz İşlem",
                        RedirectingTimeOut = 3000,
                        Items = res.Errors,
                    };
                    return View("Error", evm);
                }
            }
            return View(res.Result);
        }
        [Auth]
        [HttpPost]
        public ActionResult EditProfile(NoteUser model, HttpPostedFileBase ProfileImage)
        {
            ModelState.Remove("ModifiedUsername");//istemediğimiz özelliği kaldırabiliriz.
            if (ModelState.IsValid)
            {
                if (ProfileImage != null &&
                (ProfileImage.ContentType == "image/jpeg" ||
                ProfileImage.ContentType == "image/jpg" ||
                ProfileImage.ContentType == "image/png"))
                {
                    string fileName = $"user_{model.Id}.{ProfileImage.ContentType.Split('/')[1]}";
                    ProfileImage.SaveAs(Server.MapPath($"~/img/{fileName}"));
                    model.ProfileImageFilename = fileName;
                }
                LayerResult<NoteUser> res = um.UpdateProfile(model);
                if (res.Errors.Count > 0)
                {
                    ErrorViewModel evm = new ErrorViewModel()
                    {
                        Title = "Hata oluştu..",
                        Items = res.Errors,
                        RedirectingUrl = "/Home/EditProfile"
                    };
                    return View("Error", evm);
                }
                CurrentSession.Set<NoteUser>("login", res.Result);

                return RedirectToAction("ShowProfile");
            }
            return View(model);
        }
        [Auth]
        public ActionResult RemoveProfile()
        {

            LayerResult<NoteUser> res = um.RemoveUserById(CurrentSession.user.Id);
            if (res.Errors.Count > 0)
            {
                ErrorViewModel evm = new ErrorViewModel()
                {
                    Title = "Profil Silenemedi..",
                    Items = res.Errors,
                    RedirectingUrl = "/Home/ShowProfile"
                };
                return View("Error", evm);
            }
            CurrentSession.Clear();

            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                LayerResult<NoteUser> res = um.LoginUser(model);
                if (res.Errors.Count > 0)
                {
                    if (res.Errors.Find(x => x.Code == ErrorMessageCode.UserIsNotActive) != null)
                    {
                        ViewBag.SetLink = "http://Home/Activate/1234-4567-78945";
                    }
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(model);
                }
                CurrentSession.Set<NoteUser>("login", res.Result);
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                LayerResult<NoteUser> res = um.RegisterUser(model);
                if (res.Errors.Count > 0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(model);
                }
                OkViewModel ovm = new OkViewModel()
                {
                    Title = "Kayıt Başarılı",
                    RedirectingUrl = "/Home/Login",
                    RedirectingTimeOut = 3000
                };
                ovm.Items.Add("Lütfen e-posta adresinize gönderdiğimiz aktivasyon linkine tıklayarak hesabınızı aktive ediniz.Hesabınızı aktive etmeden giriş yapamazsınız.");
                return View("Ok", ovm);

            }
            return View(model);
        }
        public ActionResult UserActivate(Guid Id)
        {
            LayerResult<NoteUser> res = um.ActivateUser(Id);
            if (res.Errors.Count > 0)
            {
                ErrorViewModel evm = new ErrorViewModel()
                {
                    Title = "Geçersiz İşlem",
                    RedirectingTimeOut = 3000,
                    Items = res.Errors,

                };
                return View("Error", evm);
                //return RedirectToView("~/Views/Shared/Error", evm);
            }
            OkViewModel ovm = new OkViewModel()
            {
                Title = "Hesap Aktifleştirildi...",
                RedirectingUrl = "/Home/Login",
                RedirectingTimeOut = 3000,
            };
            ovm.Items.Add("Hesabnız aktifleştriildi.Artık paylaşabilirisiniz.");
            return View("Ok", ovm);
        }
        public ActionResult Logout()
        {
            CurrentSession.Clear();


            return RedirectToAction("Index", "Home");
        }
        public ActionResult AccessDenied()
        {

            return View();
        }
        public ActionResult HasError()
        {

            return View();
        }
    }
}