using MyBlogProject.BLL.Abstract;
using MyBlogProject.BLL.Results;
using MyBlogProject.Common.Helpers;
using MyBlogProject.DAL.EF;
using MyBlogProject.Entity;
using MyBlogProject.Entity.Messages;
using MyBlogProject.Entity.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlogProject.BLL
{
    public class UserManager:ManagerBase<User>
    {
       
        public BusinessLayerResult<User> RegisterUser(RegisterViewModel model)
        {

            BusinessLayerResult<User> res = new BusinessLayerResult<User>();

            string message = UserManager.EmailControl(model.Email);
            if (message != null)
            {
                res.AddError(ErrorMessageCode.UnavailableEmail, "Email Syntax Error");
            }

            User user = Find(i => i.Username == model.Username || i.Email == model.Email);

            if (user != null)
            {
                if (user.Username == model.Username)
                {
                    //res.Errors.Add("This username is already taken");
                    res.AddError(ErrorMessageCode.UsernameAlreadyExists, "This Username is Already Taken");
                }

                if (user.Email == model.Email)
                {
                    //res.Errors.Add("This email is already taken");
                    res.AddError(ErrorMessageCode.EmailAlreadyExists, "This Email is Already Taken");
                }

            }
            else
            {
                int dbResult = Insert(new User()
                {
                    Username = model.Username,
                    Email = model.Email,
                    Password = model.Password,
                    ActivateGuid = Guid.NewGuid(),
                    IsActive = false,
                    IsAdmin = false,

                });

                if (dbResult > 0)
                {
                    res.Result = Find(i => i.Email == model.Email && i.Username == model.Username);

                    string siteUrl = ConfigHelper.Get<string>("SiteUrl");
                    string activateUrl = $"{siteUrl}/Home/UserActivate/{res.Result.ActivateGuid}";
                    string body = $"Hello! {res.Result.Username};<br><br>Please, click the link which below to activate your account <a href='{activateUrl}' target='_blank'>here</a>";

                    MailHelper.SendMail(body, res.Result.Email, "MyBlogProject Account Activation");

                    //TODO: aktivasyon maili atılacak 
                    //res.Result.ActivateGuid
                }
            }

            return res;
        }

        public BusinessLayerResult<User> LoginUser(LoginViewModel model)
        {
            BusinessLayerResult<User> res = new BusinessLayerResult<User>();
            res.Result = Find(x => x.Username == model.Username && x.Password == model.Password);

            if (res.Result != null)
            {
                if (!res.Result.IsActive)
                {
                    //res.Errors.Add("User is not activated. Please check your mailbox.");
                    res.AddError(ErrorMessageCode.UserIsNotActive, "User is not Activated. Please check your mailbox.");
                }
            }
            else
            {
                //res.Errors.Add("Username or Email Error !!!");
                res.AddError(ErrorMessageCode.UsernameOrPasswordWrong, "Username or Email Error !!!");
            }

            return res;
        }

        public BusinessLayerResult<User> ActivateUser(Guid id)
        {
            BusinessLayerResult<User> res = new BusinessLayerResult<User>();
            res.Result = Find(x => x.ActivateGuid == id);
            if (res.Result != null)
            {
                if (res.Result.IsActive)
                {
                    res.AddError(ErrorMessageCode.UserAlreadyActive, "This account has already activated.");
                    return res;
                }
                res.Result.IsActive = true;
                Update(res.Result);
            }
            else
            {
                res.AddError(ErrorMessageCode.ActivationIdDoesNotExist, "There is no account for activation. ");

            }
            return res;
        }
        private static string EmailControl(string email)
        {
            // e - mail hesabı oluşturma


            #region "@" kontrolü
            bool varmi = false;
            int sayac = 0;
            for (int i = 0; i < email.Length; i++)
            {
                if (email.Substring(i, 1) == "@")
                {
                    varmi = true;
                    sayac++;
                }
            }
            if (varmi == false || sayac != 1)
            {
                return "emailde bir tane'@' olmalıdır!";
            }
            #endregion

            #region "." kontrolü
            bool varmi2 = false;
            for (int i = 0; i < email.Length; i++)
            {
                if (email.Substring(i, 1) == ".")
                {
                    varmi2 = true;
                }
            }
            if (varmi2 == false)
            {
                return "emailde bir tane'.' olmalıdır!";
            }
            #endregion

            string[] dizi = email.Split('@');

            #region karakter kontrolü
            if (dizi[0] == "")
            {
                return "'@'den önce en az bir karakter olmalidir";
            }
            #endregion

            #region @ sonrası kontrol
            bool varmi3 = false;
            for (int i = 0; i < dizi[1].Length; i++)
            {
                if (dizi[1].Substring(i, 1) == ".")
                {
                    varmi3 = true;
                }
            }
            if (varmi3 == false)
            {
                return "emailde '@' den sonra bir tane'.' olmalıdır!";
            }
            #endregion

            string[] dizi2 = dizi[1].Split('.');

            #region @ sonrası '.' kontrolü
            bool varmi4 = false;
            for (int i = 0; i < dizi2[dizi2.Length - 1].Length; i++)
            {
                if (dizi2[dizi2.Length - 1].Substring(i, 1) != "")
                {
                    varmi4 = true;
                }
            }
            if (varmi4 == false)
            {
                return "emailde '@' den sonraki kısımda '.' dan sonra en az bir tane karakter olmalıdır!";
            }

            #endregion

            #region ' ' kontrolü
            bool varmi5 = true;
            char[] dizi3 = email.ToCharArray();

            for (int i = 0; i < dizi3.Length; i++)
            {
                if (dizi3[i] == ' ')
                {
                    varmi5 = false;
                }
            }
            if (varmi5 == false)
            {
                return "emailde ' ' karakteri kullanılamaz";
            }
            else
            {
                email = string.Concat(dizi3);
                return null;
            }
            #endregion


        }

        public BusinessLayerResult<User> GetUserById(int id)
        {
            BusinessLayerResult<User> res = new BusinessLayerResult<User>();

            res.Result = Find(x => x.Id == id);

            if (res.Result == null)
            {
                res.AddError(ErrorMessageCode.UserIsNotFound, "User is not found.");
            }
            return res;

        }

        public BusinessLayerResult<User> UpdateProfile(User model)
        {
            User db_user = Find(x => x.Id != model.Id && (x.Username == model.Username || x.Email == model.Email));

            BusinessLayerResult<User> res = new BusinessLayerResult<User>();

            if (db_user != null && db_user.Id != model.Id)
            {
                if (db_user.Email == model.Email)
                {
                    res.AddError(ErrorMessageCode.EmailAlreadyExists, "Email adresi kayıtlı");
                }
                if (db_user.Username == model.Username)
                {
                    res.AddError(ErrorMessageCode.UsernameAlreadyExists, "Kullanıcı adı kayıtlı");
                }
                return res;
            }

            res.Result = Find(x => x.Id == model.Id);
            res.Result.Email = model.Email;
            res.Result.Username = model.Username;
            res.Result.Name = model.Name;
            res.Result.Surname = model.Surname;
            res.Result.Password = model.Password;

            if (string.IsNullOrEmpty(model.ProfileImageFilename) == false)
            {
                res.Result.ProfileImageFilename = model.ProfileImageFilename;
            }

            if (Update(res.Result) == 0)
            {
                res.AddError(ErrorMessageCode.ProfileCouldNotUpdated, "Profil Güncellenemedi");
            }

            return res;

        }

        public BusinessLayerResult<User> RemoveUserById(int id)
        {
            BusinessLayerResult<User> res = new BusinessLayerResult<User>();
            User user = Find(x => x.Id == id);

            if (user != null)
            {
                if (Delete(user) == 0)
                {
                    res.AddError(ErrorMessageCode.UserCouldNotRemove, "Kullanıcı silinemedi");
                    return res;
                }
            }
            else
            {
                res.AddError(ErrorMessageCode.UserCouldNotFind, "Kullanıcı bulunamadı.");
            }

            return res;
        }
    }
}
