using MyNoteSample.BusinessLayer.Abstract;
using MyNoteSample.BusinessLayer.Results;
using MyNoteSample.Common.Helpers;
using MyNoteSample.DataAccessLayer.EntityFramework;
using MyNoteSample.Entities;
using MyNoteSample.Entities.Messages;
using MyNoteSample.Entities.ValueObject;
using System;


namespace MyNoteSample.BusinessLayer
{
    public class UserManager : ManagerBase<NoteUser>
    {

        public LayerResult<NoteUser> RegisterUser(RegisterModel data)
        {
            NoteUser noteUser = Find(x => x.UserName == data.Username || x.Email == data.Email);
            LayerResult<NoteUser> layerResult = new LayerResult<NoteUser>();
            if (noteUser != null)
            {
                if (noteUser.UserName == data.Username)
                {
                    layerResult.AddError(ErrorMessageCode.UsernameAlreadyExists, "Kullanıcı Adı Kayıtlı.");
                }
                if (noteUser.Email == data.Email)
                {
                    layerResult.AddError(ErrorMessageCode.EmailAlreadyExists, "Kullanıcı E-mail kayıtlı");
                }

            }
            else
            {
                int result = base.Insert(new NoteUser()
                {
                    UserName = data.Username,
                    Email = data.Email,
                    ProfileImageFilename = "user_default.png",
                    Password = data.Password,
                    ActiveGuid = Guid.NewGuid(),
                    IsActive = false,
                    IsAdmin = false,
                });
                if (result > 0)
                {
                    layerResult.Result = Find(x => x.Email == data.Email && x.UserName == data.Username);
                    string siteUrl = ConfigHelper.Get<string>("SiteRootUrl");
                    string activateUrl = $"{siteUrl}/Home/UserActivate/{layerResult.Result.ActiveGuid}";
                    string body = $"Merhaba {layerResult.Result.UserName} ;<br><br>Hesabınızı aktifleştirmek için <a href='{activateUrl}' target='_blank'>tıklayınız</a>";
                    MailHelper.SenMail(body, layerResult.Result.Email, "MyNoteSample Aktifleştirme");
                    //TODO:Aktivasyon Email'ı Alınacak
                    //layerResult.Result.ActiveGuid
                }
            }
            return layerResult;
        }
        public LayerResult<NoteUser> GetUseryById(int Id)
        {
            LayerResult<NoteUser> res = new LayerResult<NoteUser>();
            res.Result = Find(x => x.Id == Id);
            if (res.Result == null)
            {
                res.AddError(ErrorMessageCode.UserNotFound, "Kullanıcı Bulunamadı.");

            }
            return res;
        }
        public LayerResult<NoteUser> LoginUser(LoginViewModel data)
        {
            LayerResult<NoteUser> res = new LayerResult<NoteUser>();
            res.Result = Find(x => x.UserName == data.UserName && x.Password == data.Password);


            if (res.Result != null)
            {
                if (!res.Result.IsActive)
                {
                    res.AddError(ErrorMessageCode.UserIsNotActive, "Kullanıcı Aktifleştirlmemiştir");
                    res.AddError(ErrorMessageCode.CheckYourEmail, "Lütfen E -posta adresinizi kontrol edniniz.");
                }

            }
            else
            {
                res.AddError(ErrorMessageCode.UsernameOrPassWrong, "Kullanıcı adı veya şifre uyuşmuyor");
            }
            return res;
        }
        public LayerResult<NoteUser> ActivateUser(Guid activatedId)
        {
            LayerResult<NoteUser> res = new LayerResult<NoteUser>();
            res.Result = Find(x => x.ActiveGuid == activatedId);
            if (res.Result != null)
            {
                if (res.Result.IsActive)
                {
                    res.AddError(ErrorMessageCode.UserAlreadyActivate, "Kullanıcı zaten aktif edilmiştir.");
                    return res;
                }
                res.Result.IsActive = true;
                Update(res.Result);
            }
            else
            {
                res.AddError(ErrorMessageCode.ActivateIdDoesNotExists, "Aktifleştirecek kullanıcı bulunamadı.");
            }
            return res;
        }
        public LayerResult<NoteUser> UpdateProfile(NoteUser data)
        {
            NoteUser db_user = Find(x => x.UserName == data.UserName || x.Email == data.Email);
            LayerResult<NoteUser> res = new LayerResult<NoteUser>();
            if (db_user != null && db_user.Id != data.Id)
            {
                if (db_user.UserName == data.UserName)
                {
                    res.AddError(ErrorMessageCode.UsernameAlreadyExists, "Kullanıcı Adı Kayıtlı.");
                }
                if (db_user.Email == data.Email)
                {
                    res.AddError(ErrorMessageCode.EmailAlreadyExists, "Kullanıcı E-mail kayıtlı");
                }
                return res;

            }
            res.Result = Find(x => x.Id == data.Id);
            res.Result.Email = data.Email;
            res.Result.Name = data.Name;
            res.Result.SurName = data.SurName;
            res.Result.UserName = data.UserName;
            res.Result.Password = data.Password;
            if (string.IsNullOrEmpty(data.ProfileImageFilename) == false)
            {
                res.Result.ProfileImageFilename = data.ProfileImageFilename;
            }
            if (base.Update(res.Result) == 0)
            {
                res.AddError(ErrorMessageCode.ProfileCouldNotUpdated, "Profil Güncellenmedi.");
            }
            return res;

        }
        public LayerResult<NoteUser> RemoveUserById(int Id)
        {
            LayerResult<NoteUser> res = new LayerResult<NoteUser>();
            NoteUser db_user = Find(x => x.Id == Id);

            if (db_user != null)
            {
                if (Delete(db_user) == 0)
                {
                    res.AddError(ErrorMessageCode.UserCouldNotRemove, "Kullanıcı silinemedi");
                    return res;
                }
            }
            else
            {
                res.AddError(ErrorMessageCode.UserCouldNotFind, "Kullanıcı Bulunamadı");
            }
            return res;
        }
        //Method hiding
        public new LayerResult<NoteUser> Insert(NoteUser data)
        {
            NoteUser noteUser = Find(x => x.UserName == data.UserName || x.Email == data.Email);
            LayerResult<NoteUser> res = new LayerResult<NoteUser>();
            res.Result = data;
            if (noteUser != null)
            {
                if (noteUser.UserName == data.UserName)
                {
                    res.AddError(ErrorMessageCode.UsernameAlreadyExists, "Kullanıcı Adı Kayıtlı.");
                }
                if (noteUser.Email == data.Email)
                {
                    res.AddError(ErrorMessageCode.EmailAlreadyExists, "Kullanıcı E-mail kayıtlı");
                }

            }
            else
            {
                res.Result.ProfileImageFilename = "user_default.png";
                res.Result.ActiveGuid = Guid.NewGuid();
                if (base.Insert(res.Result) == 0)

                {
                    res.AddError(ErrorMessageCode.UserCouldNotInserted, "Kullanıcı kayıt edilemdi.");
                }

            }
            return res;

        }
        public new LayerResult<NoteUser> Update(NoteUser data)
        {
            NoteUser db_user = Find(x => x.UserName == data.UserName || x.Email == data.Email);
            LayerResult<NoteUser> res = new LayerResult<NoteUser>();
            res.Result = data;
            if (db_user != null && db_user.Id != data.Id)
            {
                if (db_user.UserName == data.UserName)
                {
                    res.AddError(ErrorMessageCode.UsernameAlreadyExists, "Kullanıcı Adı Kayıtlı.");
                }
                if (db_user.Email == data.Email)
                {
                    res.AddError(ErrorMessageCode.EmailAlreadyExists, "Kullanıcı E-mail kayıtlı");
                }
                return res;

            }
            res.Result = Find(x => x.Id == data.Id);
            res.Result.Email = data.Email;
            res.Result.Name = data.Name;
            res.Result.SurName = data.SurName;
            res.Result.UserName = data.UserName;
            res.Result.Password = data.Password;
            res.Result.IsActive = data.IsActive;
            res.Result.IsAdmin = data.IsAdmin;

            if (base.Update(res.Result) == 0)
            {
                res.AddError(ErrorMessageCode.UserCouldNotUpdated, "Kullanıcı Güncellenmedi.");
            }
            return res;

        }
    }
}
