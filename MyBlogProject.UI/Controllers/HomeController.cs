using MyBlogProject.BLL;
using MyBlogProject.Entity;
using MyBlogProject.Entity.Messages;
using MyBlogProject.Entity.ViewModels;
using MyBlogProject.UI.ViewModels;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MyBlogProject.UI.Controllers
{
    public class HomeController : Controller
    {
        BlogManager bm = new BlogManager();
        CategoryManager cm = new CategoryManager();
        UserManager um = new UserManager();

        // GET: Home
        public ActionResult Index()
        {
            //MyBlogProject.BLL.Test test = new MyBlogProject.BLL.Test();
            //test.InsertTest();
            //test.UpdateTest();
            //test.CommentTest();
            //if (TempData["blogs"] != null)
            //{
            //    return View(TempData["blogs"] as List<Blog>);
            //}


            //return View(bm.GetAllBlogs().OrderByDescending(i => i.ModifiedOn).ToList());

            return View(bm.GetAllBlogsQueryable().Include("Owner").OrderByDescending(i => i.ModifiedOn).ToList());
        }

        public ActionResult ByCategory(int? id)
        {

            // GET: Category
            // PartialCategories içerisindeki adresi değiştirildi ve CategoryController Kapatıldı.
            // HATA !!!: KATEGORİLERE HER TIKLANDIĞINDA SAYFA EN TEPEYE ATIYOR, TEMPDATA YÖNTEMİNDE BU OLMAMIŞTI.

            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }


                Category cat = cm.GetCategoryById(id.Value);

                if (cat == null)
                {
                    return HttpNotFound();
                }


                return View("Index", cat.Blogs.OrderByDescending(i => i.ModifiedOn).ToList());
            }

        }

        public ActionResult MostLiked()
        {
            return View("Index", bm.GetAllBlogsQueryable().OrderByDescending(i => i.LikeCount).ToList());
        }
        public ActionResult About()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                BusinessLayerResult<User> res = um.LoginUser(model);


                if (res.Errors.Count > 0)
                {
                    if (res.Errors.Find(x => x.Code == ErrorMessageCode.UserIsNotActive) != null)
                    {
                        ViewBag.Setlink = "http://localhost:57227/Home/Activate/1234-5678-9999";
                    }
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(model);
                }

                Session["login"] = res.Result;
                return RedirectToAction("Index");
            }

            return View(model);
        }
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {

            if (ModelState.IsValid)
            {
                #region controlmailerror

                //if (model.Username == "emrekocak")
                //{
                //    ModelState.AddModelError("Username", "This username is already taken");
                //}
                //if (model.Email == "emrekocak@gmail.com")
                //{
                //    ModelState.AddModelError("Email", "This email is already taken");
                //}
                //foreach (var item in ModelState)
                //{
                //    if (item.Value.Errors.Count > 0)
                //    {
                //        return View(model);
                //    }
                //}
                #endregion

                #region Control
                //try
                //{
                //    um.RegisterUser(model);
                //}
                //catch (Exception ex)
                //{
                //    ModelState.AddModelError("", ex.Message);
                //}

                #endregion


                BusinessLayerResult<User> res = um.RegisterUser(model);

                if (res.Errors.Count > 0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(model);
                }

                OkViewModel notifyObj = new OkViewModel()
                {
                    Title = "Register is Successful",
                    RedirectingUrl = "/Home/Login",

                };

                notifyObj.Items.Add("Please check your mailbox and click the activation link to complete your account.");

                return View("Ok", notifyObj);

            }




            return View(model);
        }


        public ActionResult UserActivate(Guid id)
        {
            BusinessLayerResult<User> res = um.ActivateUser(id);

            if (res.Errors.Count > 0)
            {
                ErrorViewModel errorObj = new ErrorViewModel()
                {
                    Items = res.Errors
                };

                return View("Error", errorObj);
            }
            OkViewModel notifyObj = new OkViewModel()
            {
                Title = "Account has been activated. ",
                RedirectingUrl = "/Home/Login",

            };

            notifyObj.Items.Add("Blog, Comment, Like... Now Whatever you want you can do it!");

            return View("Ok", notifyObj);
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index");
        }

        public ActionResult ShowProfile()
        {
            if (Session["login"] == null)
            {
                return RedirectToAction("login");
            }
            User currentUser = Session["login"] as User;

            //BusinessLayerResult<User> res = UserManager.GetUserById(currentUser.Id);

            //if (res.Errors.Count > 0)
            //{

            //}

            //return View(res.Result);

            return View(currentUser);
        }

        public ActionResult EditProfile()
        {
            if (Session["login"] == null)
            {
                return RedirectToAction("login");
            }
            User currentUser = Session["login"] as User;

            return View(currentUser);
        }
        [HttpPost]
        public ActionResult EditProfile(User model, HttpPostedFileBase ProfileImage)
        {
            if (ProfileImage != null && (
                ProfileImage.ContentType == "image/jpeg" ||
                ProfileImage.ContentType == "image/jpg" ||
                ProfileImage.ContentType == "image/png"))
            {
                string filename = $"user{model.Id}.{ProfileImage.ContentType.Split('/')[1]}";

                ProfileImage.SaveAs(Server.MapPath($"~/Content/image/{filename}"));
                model.ProfileImageFilename = filename;
            }

            BusinessLayerResult<User> res = um.UpdateProfile(model);

            if (res.Errors.Count > 0)
            {
                ErrorViewModel errorObj = new ErrorViewModel()
                {
                    Items = res.Errors,
                    Title = "Profile could not be updated",
                    RedirectingUrl = "/Home/EditProfile"
                };
                Session["login"] = res.Result;
                return View();
            }

            return View();
        }

        public ActionResult DeleteProfile()
        {
            return View();
        }
    }
}